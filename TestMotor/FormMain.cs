using System.Diagnostics;
using System.Globalization;
using Nanotec.UsbMotor;

namespace TestMotor
{
    public partial class FormMain : Form
    {
        private readonly Motor m = new();
        public FormMain()
        {
            InitializeComponent();
            m.Log += Log;
        }

        private void Log(object? sender, (string, EventLogEntryType) e)
        {

            var fColor = e.Item2 switch
            {
                EventLogEntryType.Error => Color.Red,
                EventLogEntryType.FailureAudit => Color.Crimson,
                EventLogEntryType.Warning => Color.Brown,
                EventLogEntryType.Information => Color.Green,
                EventLogEntryType.SuccessAudit => Color.DarkSeaGreen,
                _ => throw new ArgumentOutOfRangeException()
            };

            lstLog.Invoke(() =>
            {
                var item = new ListViewItem
                {
                    Text = $"<{e.Item2}>  {e.Item1}",
                    ForeColor = fColor
                };
                lstLog.Items.Add(item);
            });
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            var allok = await m.ConnectAsync(txtSn.Text);
            if (allok)
            {
                var velocity = await m.GetProfileVelocityAsync();

                this.Invoke(() =>
                {
                    numVelocity.Value = Convert.ToDecimal(velocity);
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                });
            }
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m.Disconnect(true);
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            await m.StopAsync();
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Homing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            throw new NotImplementedException();

        }

        private async void btnAbs_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAbs.Text, out var iVal))
                await m.MoveAbsoluteAsync(iVal);
            else
                Log(sender, ("Move abs error. Wrong value", EventLogEntryType.Error));
        }

        private async void btnRel_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtRel.Text, out var iVal))
                await m.MoveRelativeAsync(iVal);
            else
                Log(sender, ("Move rel error. Wrong value", EventLogEntryType.Error));
        }

        private async void btnReadNum_Click(object sender, EventArgs e)
        {
            if (ushort.TryParse(txtIndex.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var index) &&
                byte.TryParse(txtSubindex.Text.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var subIndex))
            {
                var val = await m.ReadDataNumAsync(index, subIndex);

                txtValue.Invoke(() =>
                {
                    txtValue.Text = val.ToString();
                });
            }
        }
        private async void btnReadString_Click(object sender, EventArgs e)
        {
            if (ushort.TryParse(txtIndex.Text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var index) &&
                byte.TryParse(txtSubindex.Text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var subIndex))
            {
                var val = await m.ReadDataStrAsync(index, subIndex);

                txtValue.Invoke(() =>
                {
                    txtValue.Text = val;
                });
            }
        }

        private async void numVelocity_ValueChanged(object sender, EventArgs e)
        {
            var velocity = Convert.ToInt32(numVelocity.Value);
            await m.SetProfileVelocityAsync(velocity);
        }

        private void btnSetZero_Click(object sender, EventArgs e)
        {
            m.SetPositionZero();
        }

        private async void btnCurrentPosition_Click(object sender, EventArgs e)
        {
            var curP = await m.GetCurrentPositionAsync();

            txtValue.Invoke(() =>
            {
                txtValue.Text = curP.ToString();
            });
        }
    }
}