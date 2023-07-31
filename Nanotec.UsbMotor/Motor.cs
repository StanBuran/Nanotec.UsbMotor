#nullable disable
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Nlc;

namespace Nanotec.UsbMotor;
public class Motor
{
	public EventHandler<(string Message, EventLogEntryType LogType)> Log;
    public int PollingFrequency { get; set; } = 60;
	private NanoLibAccessor Accessor {get;set;}
	private DeviceHandle DevHandle { get; set; }
    private System.Timers.Timer _tm;
    private bool _stopped;
	public bool[] StatusWord { get; private set; } = new bool[16];
	public string SerialNo { get; private set; }
    public async Task<bool> ConnectAsync(string sn)
    {
        using var task = Task.Factory.StartNew(() => Connect(sn));
		return await task.ConfigureAwait(false);
    }
    private bool Connect(string sn)
    {
        SerialNo = sn;

        //UsbBusVcp (Virtual Com-Port)
        //UsbBusMsc (Mass Storage Class)
		const string hrdwSpecifier = "UsbBusVcp";
        var allok = false;

		Accessor = Nanolib.getNanoLibAccessor();
		if (Accessor == null)
			return false;


		using var lstHrdw = Accessor.listAvailableBusHardware().getResult();
		
		foreach(var hrdw in lstHrdw)
		{									
			// USB BUS
			var name = hrdw.getName();			
			var hrdwBus = hrdw.getBusHardware();			
			var hrdwEx = hrdw.getExtraHardwareSpecifier();
						
			if(!name.Equals("USB Bus", StringComparison.InvariantCultureIgnoreCase) || !hrdwBus.Equals("USB BUS", StringComparison.InvariantCultureIgnoreCase) || !hrdwEx.Equals(hrdwSpecifier, StringComparison.InvariantCultureIgnoreCase))
				continue;
			
			//Open protocol:
			var result = Accessor.openBusHardwareWithProtocol(hrdw, new BusHardwareOptions());
			if (result.hasError())
				continue;
			
			//Check is bus open
			if (!Accessor.isBusHardwareOpen(hrdw))				
				continue;
			
			//Scan devices
			var scanCallback = new ScanBusCallback();
			var devResult = Accessor.scanDevices(hrdw, scanCallback);
			if (devResult.hasError())
				continue;
			
			using var devices = devResult.getResult();
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach(var dev in devices)
			{
                var devHandle = Accessor.addDevice(dev).getResult();
				result = Accessor.connectDevice(devHandle);
				if (!result.hasError())
				{
					var serial = Accessor.readString(devHandle, new OdIndex(0x4040, 0)).getResult().Split(' ').FirstOrDefault()??"N/A";
                    if (serial.Equals(sn, StringComparison.InvariantCultureIgnoreCase))
					{
						DevHandle = devHandle;
						allok = true;						
						break;
					}
                    WriteLog($"Serial {serial} skipped.", EventLogEntryType.FailureAudit);
					Accessor.removeDevice(devHandle);
				}
            }

            if (allok)
                break;
        }
		
        if(allok)				
        {
            _tm = new System.Timers.Timer(PollingFrequency) { AutoReset = true };
            _tm.Elapsed += Timer_Elapsed;
            _tm.Start();
            WriteLog("Device successfully connected");
        }
		else
		    Disconnect(false);

        return allok;
	}

    private bool _tmBusy;
    private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (_tmBusy)
            return;

        if (!await IsConnectedAsync())
        {
            StatusWord = new bool[16];
            return;
        }
        
        _tmBusy = true;

        StatusWord = ReadDataNum(0x6041).ToBinaryString(16)
                                        .Reverse()
                                        .Select(t=>t.Equals('1'))
                                        .ToArray();
        _tmBusy = false;
    }
    public async Task<bool> IsConnectedAsync()
    {
        using var task = Task.Factory.StartNew(IsConnected);
		return await task.ConfigureAwait(false);
    }
	private bool IsConnected()
    {
        var allok = false;

        try
        {
            allok = Accessor?.getConnectionState(DevHandle).getResult() == DeviceConnectionStateInfo.Connected;
        }
        catch (Exception e)
        {
            WriteLog(e);
        }

        return allok;
    }
    public void Disconnect(bool log)
	{
        if (_tm != null)
        {
			_tm.Stop();
            _tm.Dispose();
            _tm = null;
        }

		if (Accessor == null)
			return;

		try
		{
			Accessor.disconnectDevice(DevHandle);
			Accessor.removeDevice(DevHandle);
		}
		finally
		{
			Accessor.Dispose();
			Accessor = null;
		}

		if(log)
		    WriteLog("Device disconnected.");
	}
	public async Task<string> ReadDataStrAsync(ushort index, byte subIndex = 0)
    {
        using var task = Task.Factory.StartNew(() => ReadDataStr(index, subIndex));
		return await task.ConfigureAwait(false);
    }
    private string ReadDataStr(ushort index, byte subIndex = 0)
	{
		var result = Accessor.readString(DevHandle, new OdIndex(index, subIndex));
		if (result.hasError())
		{
			WriteLog($"#O1Z3SA75S. Register: {index:X}:{subIndex:00} - {result.getErrorCode()}, {result.getError()}", EventLogEntryType.Error);
			return default;
		}
		return result.getResult();
	}
	public async Task<long> ReadDataNumAsync(ushort index, byte subIndex = 0)
    {
        using var task = Task.Factory.StartNew(() => ReadDataNum(index, subIndex));
		return await task.ConfigureAwait(false);
    }
    private long ReadDataNum(ushort index, byte subIndex = 0)
	{	
		var result = Accessor.readNumber(DevHandle, new OdIndex(index, subIndex));		
		if(result.hasError())
		{
			WriteLog($"#K7D4M661DA Register: {index:X}:{subIndex:00} - {result.getErrorCode()}, {result.getError()}", EventLogEntryType.Error);
			return default;
		}		
		var resVal = result.getResult();
		switch (GetRegLength(index, subIndex))
		{
			case 8:
				if (resVal > byte.MaxValue)
					resVal = unchecked((byte)resVal);
				break;
			case 16:
				if (resVal > short.MaxValue)
					resVal = unchecked((short)resVal);
				break;
			case 32:
				if (resVal > int.MaxValue)
					resVal = unchecked((int)resVal);
				break;
		}
		return resVal;
	}
    public async Task<long[]> ReadDataNumArrayAsync(ushort index)
    {
        using var task = Task.Factory.StartNew(() => ReadDataNumArray(index));
        return await task.ConfigureAwait(false);
    }
    private long[] ReadDataNumArray(ushort index)
	{
		var result = Accessor.readNumberArray(DevHandle, index);
		if (result.hasError())
		{
			WriteLog($"Error #P2X4F1D78 Register: {index:X} - {result.getErrorCode()}, {result.getError()}", EventLogEntryType.Error);
			return default;
		}
		return result.getResult().ToArray();
	}
    public async Task<byte[]> ReadDataByteArrayAsync(ushort index, byte subIndex = 0)
    {
        using var task = Task.Factory.StartNew(() => ReadDataByteArray(index, subIndex));
        return await task.ConfigureAwait(false);
    }
    private byte[] ReadDataByteArray(ushort index, byte subIndex = 0)
	{
		var result = Accessor.readBytes(DevHandle, new OdIndex(index, subIndex));
		if (result.hasError())
		{
			WriteLog($"Error #I2DQ19J6TY Register: {index:X}:{subIndex:00} - {result.getErrorCode()}, {result.getError()}", EventLogEntryType.Error);
			return default;
		}
		return result.getResult().ToArray();
	}

    public async Task<string> GetDriveSerialNumberAsync()
    {
		using var task = Task.Factory.StartNew(GetDriveSerialNumber);
		return await task.ConfigureAwait(false);
    }
    private string GetDriveSerialNumber() => ReadDataStr(0x4040).Split(' ').FirstOrDefault();	
	public async Task<long> GetCurrentPositionAsync()
    {
		using var task = Task.Factory.StartNew(GetCurrentPosition);
		return await task.ConfigureAwait(false);
    }
    private long GetCurrentPosition() => ReadDataNum(0x6064);
	public async Task<bool> TestAsync(bool positive)
    {
        var allok = await IsConnectedAsync();
        if (!allok)
        {
			WriteLog("#O1AQ1RV3033H Unable to perform homing - the motor not connected.", EventLogEntryType.Warning);
            return false;
        }

        WriteLog($"Run test on device {SerialNo}");
		
        #pragma warning disable CS4014
        Task.Run(() => MoveRelative(positive ? int.MaxValue : int.MinValue));
        #pragma warning restore CS4014
	    await Task.Delay(500);
	    
        await WaitForHardStopAsync();

        return true;
    }
	public async Task WaitForHardStopAsync()
	{
		var path = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), ".csv");
		WriteLog($"Writing data to log file: {path}");
        using var task = Task.Factory.StartNew(() =>
        {
            using var stream = File.AppendText(path);
            stream.WriteLine("DateTimeStamp,Torque");
            var lst = new List<long>();
            for (;;)
            {
                var torque = ReadDataNum(0x6077);
				lst.Add(torque);
				if (lst.Count >= 30)
				{
					stream.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss ffff},{lst.Average()}");
					lst.Clear();
				}

				if (_stopped)
                    break;
            }
			stream.Flush();
        });
		await task.ConfigureAwait(false);
		WriteLog("Data writing completed.");
    }
	public async Task StopAsync()
    {
        using var task = Task.Factory.StartNew(Stop);
        await task.ConfigureAwait(false);
    }
	private void Stop()
    {
        WriteData(11, 0x6040);
        _stopped = true;
        WriteLog("Stop motor.");
    }
    private async Task<bool> WaitForTargetReached(double timeoutSec = 30.0)
    {
        StatusWord[10] = false;
        var timeout = timeoutSec > 0 ? DateTime.Now.AddSeconds(timeoutSec) : DateTime.MaxValue;
        await Task.Delay(PollingFrequency);
        using var task = Task.Factory.StartNew(() =>
        {
            while (_tm.Enabled && !StatusWord[10])
            {
                if (DateTime.Now > timeout)
                {
					WriteLog("#I0ES1A42 WaitForTargetReached: timeout.", EventLogEntryType.Error);
                    return false;
                }
                Thread.Sleep(PollingFrequency);
            }
            return true;
        });
        return await task.ConfigureAwait(false);
    }
    public async Task<bool> MoveAbsoluteAsync(long target, double waitForTargetReachedSec = 30.0d)
    {
        using var task = Task.Factory.StartNew(() => MoveAbsolute(target));
        var allok = await task.ConfigureAwait(false);
        if (allok && waitForTargetReachedSec > 0)
            allok = await WaitForTargetReached(waitForTargetReachedSec);
        return allok;
    }
    private bool MoveAbsolute(long target)
	{
		WriteLog($"Move abs: {target}", EventLogEntryType.SuccessAudit);
        Reload();	
		
		var allok = WriteData(target, 0x607A);
        if (!allok)
            return false;

		allok = WriteData(143,0x6040);
        if (!allok)
            return false;

		allok = WriteData(31,0x6040);
        if (!allok)
            return false;

        _stopped = false;
        return true;
    }

    public void SetPositionZero()
    {
	    Reload(6);
	    WriteData(35, 0x6098);
	    WriteData(111, 0x6040);
	    WriteData(127, 0x6040);
	    //0x6063, 0
    }
    public async Task<bool> MoveRelativeAsync(long target, double waitForTargetReachedSec = 30.0d)
    {
        using var task = Task.Factory.StartNew(() => MoveRelative(target));
        var allok = await task.ConfigureAwait(false);
        if (allok && waitForTargetReachedSec > 0)
            allok = await WaitForTargetReached(waitForTargetReachedSec);
        return allok;
    }
    private bool MoveRelative(long target)
	{
        WriteLog($"Move rel: {target}", EventLogEntryType.SuccessAudit);
		Reload();		
		
		var allok = WriteData(target, 0x607A);
		if(!allok) 
            return false;
		allok = WriteData(143, 0x6040);
        if(!allok) 
            return false;
		allok = WriteData(639, 0x6040);
        if(!allok) 
            return false;

        _stopped = false;
		return true;
	}
    public async Task<long> GetCurrentVelocityAsync()
    {
        using var task = Task.Factory.StartNew(GetCurrentVelocity);
		return await task.ConfigureAwait(false);
    }
    private long GetCurrentVelocity() => ReadDataNum(0x606C); //Get current velocity
	public async Task SetProfileVelocityAsync(long value)
    {
        using var task = Task.Factory.StartNew(() => SetProfileVelocity(value));
        await task.ConfigureAwait(false);
    }
    private void SetProfileVelocity(long value)
    {
	    if (!IsConnected())
		    return;

		WriteLog($"Set velocity: {value}", EventLogEntryType.SuccessAudit);
        _ = WriteData(value, 0x6081); //Profile velocity
    }
    public async Task<long> GetProfileVelocityAsync()
    {
	    using var task = Task.Factory.StartNew(GetProfileVelocity);
	    return await task.ConfigureAwait(false);
    }
	private long GetProfileVelocity() => ReadDataNum(0x6081);
    private void Reload(int mode = 1)
	{
		WriteData(128,0x6040);
		WriteData(0, 0x6040);
		WriteData(6, 0x6040);
		WriteData(7, 0x6040);
		WriteData(15, 0x6040);
		WriteData(mode, 0x6060); //1:Set Profile Position, 6:Homing mode
        _stopped = true;
	}

    private void WriteLog(Exception x, [CallerMemberName] object callerMember = null)
    {
        if (x == null)
            return;

		var message = x.InnerException?.Message??x.Message;
		WriteLog(message, EventLogEntryType.Error, callerMember);
    }
	private void WriteLog(string message, EventLogEntryType logType = EventLogEntryType.Information, [CallerMemberName] object callerMember = null)
	{
		if(!string.IsNullOrWhiteSpace(message))
			Log?.Invoke(callerMember, (message, logType));
	}
	private bool WriteData(long value, ushort index, byte subIndex = 0)
	{
		var length = GetRegLength(index, subIndex);

        ResultVoid result;
        try
        {
		    result = Accessor.writeNumber(DevHandle, value, new OdIndex(index,subIndex), length);
        }
        catch (Exception e)
        {
            WriteLog(e);
            return false;
        }

		if (result.hasError())
		{
			WriteLog($"Error #P24V1S70L Value: {value}, Register: {index:X}:{subIndex:00} - {result.getErrorCode()}, {result.getError()}", EventLogEntryType.Error);
			return false;
		}
		return true;
	}
	private bool WriteData(byte[] value, ushort index, byte subIndex = 0)
	{	
		var result = Accessor.writeBytes(DevHandle, new ByteVector(value), new OdIndex(index, subIndex));
		if (result.hasError())
		{
			Console.WriteLine($"Error #REI304 Value: {value.ToSimpleString()}, Register: {index:X}:{subIndex:00} - {result.getErrorCode()}, {result.getError()}");
			return false;
		}
		return true;
	}
	
	#region [Load Registers]
	private readonly List<NanoReg> NanoRegisters = new();
	private enum AccessEnum {ReadOnly,WriteOnly,ReadWrite};
	private readonly record struct NanoReg
	{
		public int Id { get; init; }
		public ushort Index { get; init; }
		public byte SubIndex { get; init; }
		public AccessEnum Access { get; init; }
		public string DataType {get; init; }
		public uint Length {get; init;}
	};
	private uint GetRegLength(ushort index, byte subIndex)
	{
		LoadRegisters();
		var reg = NanoRegisters.FirstOrDefault(t => t.Index == index && t.SubIndex == subIndex);
		return reg.Length;
	}
	public void LoadRegisters()
	{
		if(NanoRegisters.Any())
			return;
		
		var lines = File.ReadAllLines("NanotecRegisters.csv");

        if (lines == null)
			throw new Exception("Missing NanotecRegisters.csv");

		foreach (var line in lines.Skip(1))
		{
			var arr = line.Split(',');

			var reg = new NanoReg
			{
				Id = Convert.ToInt32(arr[0]),
				Index = ushort.Parse(arr[1], System.Globalization.NumberStyles.AllowHexSpecifier),
				SubIndex = byte.Parse(arr[2], System.Globalization.NumberStyles.AllowHexSpecifier),
				Access = arr[3] switch
				{
					"Read/Write" => AccessEnum.ReadWrite,
					"Read" => AccessEnum.ReadOnly,
					"Write" => AccessEnum.WriteOnly,
					_ => throw new KeyNotFoundException()
				},
				DataType = arr[4],
				Length = uint.Parse(arr[5])
			};	
			NanoRegisters.Add(reg);
		}
	}
	#endregion
	private class ScanBusCallback : NlcScanBusCallback // override
	{
		public override ResultVoid callback(BusScanInfo info, DeviceIdVector devicesFound, int data)
		{
			/*
			switch (info)
			{
				case Nlc.BusScanInfo.Start:
					Console.Write("Scan started .");
					break;
				case Nlc.BusScanInfo.Progress:
					if ((data & 1) == 0) // data holds scan progress
					{
						Console.Write("..");
					}
					break;
				case Nlc.BusScanInfo.Finished:				
					Console.WriteLine(" done.");
					break;
			}
			*/
			return new ResultVoid();
		}
	}
}