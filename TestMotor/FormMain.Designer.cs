namespace TestMotor
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnDisconnect = new Button();
            lblMotor = new Label();
            txtSn = new TextBox();
            splitMain = new SplitContainer();
            btnCurrentPosition = new Button();
            btnSetZero = new Button();
            lblVelocity = new Label();
            numVelocity = new NumericUpDown();
            btnReadString = new Button();
            btnReadNum = new Button();
            label1 = new Label();
            lblSubIndex = new Label();
            txtValue = new TextBox();
            lblIndex = new Label();
            txtSubindex = new TextBox();
            txtIndex = new TextBox();
            txtRel = new TextBox();
            txtAbs = new TextBox();
            btnRel = new Button();
            btnAbs = new Button();
            btnStop = new Button();
            lstLog = new ListView();
            columnHeader1 = new ColumnHeader();
            btnHoming = new Button();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numVelocity).BeginInit();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(171, 45);
            btnConnect.Margin = new Padding(4, 5, 4, 5);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(107, 38);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(287, 45);
            btnDisconnect.Margin = new Padding(4, 5, 4, 5);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(107, 38);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // lblMotor
            // 
            lblMotor.AutoSize = true;
            lblMotor.Location = new Point(19, 17);
            lblMotor.Margin = new Padding(4, 0, 4, 0);
            lblMotor.Name = "lblMotor";
            lblMotor.Size = new Size(142, 25);
            lblMotor.TabIndex = 2;
            lblMotor.Text = "Motor Serial No:";
            // 
            // txtSn
            // 
            txtSn.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtSn.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSn.Location = new Point(20, 47);
            txtSn.Margin = new Padding(4, 5, 4, 5);
            txtSn.Name = "txtSn";
            txtSn.Size = new Size(141, 31);
            txtSn.TabIndex = 3;
            txtSn.Text = "B958799";
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 0);
            splitMain.Margin = new Padding(4, 5, 4, 5);
            splitMain.Name = "splitMain";
            splitMain.Orientation = Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(btnCurrentPosition);
            splitMain.Panel1.Controls.Add(btnSetZero);
            splitMain.Panel1.Controls.Add(lblVelocity);
            splitMain.Panel1.Controls.Add(numVelocity);
            splitMain.Panel1.Controls.Add(btnReadString);
            splitMain.Panel1.Controls.Add(btnReadNum);
            splitMain.Panel1.Controls.Add(label1);
            splitMain.Panel1.Controls.Add(lblSubIndex);
            splitMain.Panel1.Controls.Add(txtValue);
            splitMain.Panel1.Controls.Add(lblIndex);
            splitMain.Panel1.Controls.Add(txtSubindex);
            splitMain.Panel1.Controls.Add(txtIndex);
            splitMain.Panel1.Controls.Add(txtRel);
            splitMain.Panel1.Controls.Add(txtAbs);
            splitMain.Panel1.Controls.Add(btnRel);
            splitMain.Panel1.Controls.Add(btnAbs);
            splitMain.Panel1.Controls.Add(btnHoming);
            splitMain.Panel1.Controls.Add(btnStop);
            splitMain.Panel1.Controls.Add(lblMotor);
            splitMain.Panel1.Controls.Add(txtSn);
            splitMain.Panel1.Controls.Add(btnConnect);
            splitMain.Panel1.Controls.Add(btnDisconnect);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(lstLog);
            splitMain.Size = new Size(992, 974);
            splitMain.SplitterDistance = 597;
            splitMain.SplitterWidth = 7;
            splitMain.TabIndex = 4;
            // 
            // btnCurrentPosition
            // 
            btnCurrentPosition.Location = new Point(21, 534);
            btnCurrentPosition.Margin = new Padding(4, 5, 4, 5);
            btnCurrentPosition.Name = "btnCurrentPosition";
            btnCurrentPosition.Size = new Size(236, 38);
            btnCurrentPosition.TabIndex = 22;
            btnCurrentPosition.Text = "Current position";
            btnCurrentPosition.UseVisualStyleBackColor = true;
            btnCurrentPosition.Click += btnCurrentPosition_Click;
            // 
            // btnSetZero
            // 
            btnSetZero.Location = new Point(701, 45);
            btnSetZero.Margin = new Padding(4, 5, 4, 5);
            btnSetZero.Name = "btnSetZero";
            btnSetZero.Size = new Size(261, 38);
            btnSetZero.TabIndex = 21;
            btnSetZero.Text = "Set position Zero";
            btnSetZero.UseVisualStyleBackColor = true;
            btnSetZero.Click += btnSetZero_Click;
            // 
            // lblVelocity
            // 
            lblVelocity.AutoSize = true;
            lblVelocity.Location = new Point(436, 20);
            lblVelocity.Margin = new Padding(4, 0, 4, 0);
            lblVelocity.Name = "lblVelocity";
            lblVelocity.Size = new Size(77, 25);
            lblVelocity.TabIndex = 20;
            lblVelocity.Text = "Velocity:";
            // 
            // numVelocity
            // 
            numVelocity.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numVelocity.Location = new Point(436, 48);
            numVelocity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numVelocity.Name = "numVelocity";
            numVelocity.Size = new Size(140, 31);
            numVelocity.TabIndex = 19;
            numVelocity.TextAlign = HorizontalAlignment.Center;
            numVelocity.ValueChanged += numVelocity_ValueChanged;
            // 
            // btnReadString
            // 
            btnReadString.Location = new Point(705, 456);
            btnReadString.Name = "btnReadString";
            btnReadString.Size = new Size(257, 63);
            btnReadString.TabIndex = 17;
            btnReadString.Text = "Read String";
            btnReadString.UseVisualStyleBackColor = true;
            btnReadString.Click += btnReadString_Click;
            // 
            // btnReadNum
            // 
            btnReadNum.Location = new Point(705, 371);
            btnReadNum.Name = "btnReadNum";
            btnReadNum.Size = new Size(257, 63);
            btnReadNum.TabIndex = 16;
            btnReadNum.Text = "Read Num";
            btnReadNum.UseVisualStyleBackColor = true;
            btnReadNum.Click += btnReadNum_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(286, 341);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(58, 25);
            label1.TabIndex = 15;
            label1.Text = "Value:";
            // 
            // lblSubIndex
            // 
            lblSubIndex.AutoSize = true;
            lblSubIndex.Location = new Point(21, 426);
            lblSubIndex.Margin = new Padding(4, 0, 4, 0);
            lblSubIndex.Name = "lblSubIndex";
            lblSubIndex.Size = new Size(145, 25);
            lblSubIndex.TabIndex = 14;
            lblSubIndex.Text = "Sub-Index (HEX):";
            // 
            // txtValue
            // 
            txtValue.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtValue.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtValue.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtValue.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            txtValue.Location = new Point(279, 371);
            txtValue.Margin = new Padding(4, 5, 4, 5);
            txtValue.Multiline = true;
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(414, 201);
            txtValue.TabIndex = 13;
            txtValue.TextAlign = HorizontalAlignment.Center;
            // 
            // lblIndex
            // 
            lblIndex.AutoSize = true;
            lblIndex.Location = new Point(21, 341);
            lblIndex.Margin = new Padding(4, 0, 4, 0);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new Size(107, 25);
            lblIndex.TabIndex = 12;
            lblIndex.Text = "Index (HEX):";
            // 
            // txtSubindex
            // 
            txtSubindex.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtSubindex.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSubindex.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSubindex.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            txtSubindex.Location = new Point(21, 456);
            txtSubindex.Margin = new Padding(4, 5, 4, 5);
            txtSubindex.Name = "txtSubindex";
            txtSubindex.Size = new Size(236, 50);
            txtSubindex.TabIndex = 11;
            txtSubindex.TextAlign = HorizontalAlignment.Center;
            // 
            // txtIndex
            // 
            txtIndex.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtIndex.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtIndex.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtIndex.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            txtIndex.Location = new Point(21, 371);
            txtIndex.Margin = new Padding(4, 5, 4, 5);
            txtIndex.Name = "txtIndex";
            txtIndex.Size = new Size(236, 50);
            txtIndex.TabIndex = 10;
            txtIndex.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRel
            // 
            txtRel.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtRel.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtRel.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtRel.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            txtRel.Location = new Point(436, 128);
            txtRel.Margin = new Padding(4, 5, 4, 5);
            txtRel.Name = "txtRel";
            txtRel.Size = new Size(257, 71);
            txtRel.TabIndex = 9;
            txtRel.TextAlign = HorizontalAlignment.Center;
            // 
            // txtAbs
            // 
            txtAbs.AutoCompleteCustomSource.AddRange(new string[] { "B958799", "B955987" });
            txtAbs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtAbs.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtAbs.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            txtAbs.Location = new Point(171, 128);
            txtAbs.Margin = new Padding(4, 5, 4, 5);
            txtAbs.Name = "txtAbs";
            txtAbs.Size = new Size(257, 71);
            txtAbs.TabIndex = 8;
            txtAbs.TextAlign = HorizontalAlignment.Center;
            // 
            // btnRel
            // 
            btnRel.Location = new Point(436, 207);
            btnRel.Name = "btnRel";
            btnRel.Size = new Size(257, 94);
            btnRel.TabIndex = 7;
            btnRel.Text = "REL";
            btnRel.UseVisualStyleBackColor = true;
            btnRel.Click += btnRel_Click;
            // 
            // btnAbs
            // 
            btnAbs.Location = new Point(171, 207);
            btnAbs.Name = "btnAbs";
            btnAbs.Size = new Size(257, 94);
            btnAbs.TabIndex = 6;
            btnAbs.Text = "ABS";
            btnAbs.UseVisualStyleBackColor = true;
            btnAbs.Click += btnAbs_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(701, 128);
            btnStop.Margin = new Padding(4, 5, 4, 5);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(261, 173);
            btnStop.TabIndex = 4;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // lstLog
            // 
            lstLog.Activation = ItemActivation.OneClick;
            lstLog.BorderStyle = BorderStyle.FixedSingle;
            lstLog.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lstLog.Dock = DockStyle.Fill;
            lstLog.FullRowSelect = true;
            lstLog.HeaderStyle = ColumnHeaderStyle.None;
            lstLog.HotTracking = true;
            lstLog.HoverSelection = true;
            lstLog.LabelWrap = false;
            lstLog.Location = new Point(0, 0);
            lstLog.Margin = new Padding(4, 5, 4, 5);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(992, 370);
            lstLog.TabIndex = 0;
            lstLog.TileSize = new Size(21, 400);
            lstLog.UseCompatibleStateImageBehavior = false;
            lstLog.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 600;
            // 
            // btnHoming
            // 
            btnHoming.Enabled = false;
            btnHoming.Location = new Point(20, 128);
            btnHoming.Margin = new Padding(4, 5, 4, 5);
            btnHoming.Name = "btnHoming";
            btnHoming.Size = new Size(141, 173);
            btnHoming.TabIndex = 5;
            btnHoming.Text = "Homing";
            btnHoming.UseVisualStyleBackColor = true;
            btnHoming.Click += btnHoming_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(992, 974);
            Controls.Add(splitMain);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormMain";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Test motor";
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numVelocity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnConnect;
        private Button btnDisconnect;
        private Label lblMotor;
        private TextBox txtSn;
        private SplitContainer splitMain;
        private ListView lstLog;
        private Button btnStop;
        private TextBox txtRel;
        private TextBox txtAbs;
        private Button btnRel;
        private Button btnAbs;
        private Label label1;
        private Label lblSubIndex;
        private TextBox txtValue;
        private Label lblIndex;
        private TextBox txtSubindex;
        private TextBox txtIndex;
        private Button btnReadNum;
        private Button btnReadString;
        private Label lblVelocity;
        private NumericUpDown numVelocity;
        private ColumnHeader columnHeader1;
        private Button btnSetZero;
        private Button btnCurrentPosition;
        private Button btnHoming;
    }
}