namespace EasyCommunicationDemo
{
    partial class ModbusTcpServerForm
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
            components = new System.ComponentModel.Container();
            txt_tcpPort = new TextBox();
            label1 = new Label();
            btn_tcpListener = new Button();
            groupBox1 = new GroupBox();
            txt_log = new RichTextBox();
            txt_sendAddress = new TextBox();
            btn_send = new Button();
            label2 = new Label();
            groupBox2 = new GroupBox();
            cbx_Mode = new ComboBox();
            label7 = new Label();
            cbx_slaveAddress = new ComboBox();
            txt_preview = new TextBox();
            label6 = new Label();
            txt_sendData = new TextBox();
            txt_functionText = new Label();
            label4 = new Label();
            label3 = new Label();
            cbx_functionCode = new ComboBox();
            toolTip1 = new ToolTip(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txt_tcpPort
            // 
            txt_tcpPort.BorderStyle = BorderStyle.FixedSingle;
            txt_tcpPort.Location = new Point(104, 15);
            txt_tcpPort.MaxLength = 3;
            txt_tcpPort.Name = "txt_tcpPort";
            txt_tcpPort.Size = new Size(98, 38);
            txt_tcpPort.TabIndex = 0;
            txt_tcpPort.Text = "10123";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(92, 31);
            label1.TabIndex = 1;
            label1.Text = "端口号:";
            // 
            // btn_tcpListener
            // 
            btn_tcpListener.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_tcpListener.Location = new Point(964, 9);
            btn_tcpListener.Name = "btn_tcpListener";
            btn_tcpListener.Size = new Size(150, 46);
            btn_tcpListener.TabIndex = 2;
            btn_tcpListener.Tag = "1";
            btn_tcpListener.Text = "监听";
            btn_tcpListener.UseVisualStyleBackColor = true;
            btn_tcpListener.Click += btn_tcpListener_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txt_log);
            groupBox1.Location = new Point(12, 630);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1136, 349);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "日志";
            // 
            // txt_log
            // 
            txt_log.BackColor = SystemColors.Menu;
            txt_log.BorderStyle = BorderStyle.None;
            txt_log.Dock = DockStyle.Bottom;
            txt_log.Location = new Point(3, 37);
            txt_log.Name = "txt_log";
            txt_log.Size = new Size(1130, 309);
            txt_log.TabIndex = 4;
            txt_log.Text = "";
            // 
            // txt_sendAddress
            // 
            txt_sendAddress.BorderStyle = BorderStyle.FixedSingle;
            txt_sendAddress.ForeColor = SystemColors.WindowText;
            txt_sendAddress.Location = new Point(152, 201);
            txt_sendAddress.MaxLength = 4;
            txt_sendAddress.Name = "txt_sendAddress";
            txt_sendAddress.Size = new Size(99, 38);
            txt_sendAddress.TabIndex = 4;
            // 
            // btn_send
            // 
            btn_send.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_send.Location = new Point(952, 58);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(150, 97);
            btn_send.TabIndex = 5;
            btn_send.Tag = "1";
            btn_send.Text = "发送";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 140);
            label2.Name = "label2";
            label2.Size = new Size(140, 31);
            label2.TabIndex = 6;
            label2.Text = "从站设备号:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cbx_Mode);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(cbx_slaveAddress);
            groupBox2.Controls.Add(txt_preview);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(txt_sendData);
            groupBox2.Controls.Add(btn_send);
            groupBox2.Controls.Add(txt_functionText);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txt_sendAddress);
            groupBox2.Controls.Add(cbx_functionCode);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(12, 88);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1124, 350);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "发送消息";
            // 
            // cbx_Mode
            // 
            cbx_Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_Mode.FormattingEnabled = true;
            cbx_Mode.IntegralHeight = false;
            cbx_Mode.Items.AddRange(new object[] { "Modbus RTU", "Modbus TCP" });
            cbx_Mode.Location = new Point(125, 55);
            cbx_Mode.MaxDropDownItems = 10;
            cbx_Mode.MaxLength = 6;
            cbx_Mode.Name = "cbx_Mode";
            cbx_Mode.Size = new Size(217, 39);
            cbx_Mode.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 58);
            label7.Name = "label7";
            label7.Size = new Size(134, 31);
            label7.TabIndex = 16;
            label7.Text = "协议类型：";
            // 
            // cbx_slaveAddress
            // 
            cbx_slaveAddress.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_slaveAddress.FormattingEnabled = true;
            cbx_slaveAddress.IntegralHeight = false;
            cbx_slaveAddress.Location = new Point(152, 137);
            cbx_slaveAddress.MaxDropDownItems = 10;
            cbx_slaveAddress.MaxLength = 6;
            cbx_slaveAddress.Name = "cbx_slaveAddress";
            cbx_slaveAddress.Size = new Size(99, 39);
            cbx_slaveAddress.TabIndex = 15;
            // 
            // txt_preview
            // 
            txt_preview.BorderStyle = BorderStyle.FixedSingle;
            txt_preview.Enabled = false;
            txt_preview.ForeColor = SystemColors.WindowText;
            txt_preview.Location = new Point(152, 275);
            txt_preview.MaxLength = 5;
            txt_preview.Name = "txt_preview";
            txt_preview.ReadOnly = true;
            txt_preview.Size = new Size(629, 38);
            txt_preview.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 277);
            label6.Name = "label6";
            label6.Size = new Size(116, 31);
            label6.TabIndex = 13;
            label6.Text = "报文预览:";
            // 
            // txt_sendData
            // 
            txt_sendData.BorderStyle = BorderStyle.FixedSingle;
            txt_sendData.ForeColor = SystemColors.WindowText;
            txt_sendData.Location = new Point(430, 201);
            txt_sendData.MaxLength = 4;
            txt_sendData.Name = "txt_sendData";
            txt_sendData.Size = new Size(351, 38);
            txt_sendData.TabIndex = 12;
            // 
            // txt_functionText
            // 
            txt_functionText.AutoSize = true;
            txt_functionText.Location = new Point(284, 203);
            txt_functionText.Name = "txt_functionText";
            txt_functionText.Size = new Size(116, 31);
            txt_functionText.TabIndex = 11;
            txt_functionText.Text = "线圈数量:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 203);
            label4.Name = "label4";
            label4.Size = new Size(140, 31);
            label4.TabIndex = 10;
            label4.Text = "寄存器地址:";
            // 
            // label3
            // 
            label3.Location = new Point(284, 140);
            label3.Name = "label3";
            label3.Size = new Size(140, 31);
            label3.TabIndex = 9;
            label3.Text = "功 能 码:";
            // 
            // cbx_functionCode
            // 
            cbx_functionCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_functionCode.FormattingEnabled = true;
            cbx_functionCode.Location = new Point(430, 137);
            cbx_functionCode.Name = "cbx_functionCode";
            cbx_functionCode.Size = new Size(351, 39);
            cbx_functionCode.TabIndex = 8;
            cbx_functionCode.SelectedIndexChanged += cbx_functionCode_SelectedIndexChanged;
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 100;
            toolTip1.AutoPopDelay = 3000;
            toolTip1.BackColor = Color.FromArgb(192, 192, 255);
            toolTip1.InitialDelay = 100;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 20;
            // 
            // ModbusTcpServerForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1166, 1024);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btn_tcpListener);
            Controls.Add(label1);
            Controls.Add(txt_tcpPort);
            MaximizeBox = false;
            Name = "ModbusTcpServerForm";
            Text = "ModbusTcpServer（By 郑州正控）";
            FormClosing += ModbusTcpServerForm_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_tcpPort;
        private Label label1;
        private Button btn_tcpListener;
        private GroupBox groupBox1;
        private RichTextBox txt_log;
        private TextBox txt_sendAddress;
        private Button btn_send;
        private Label label2;
        private GroupBox groupBox2;
        private Label label3;
        private Label label4;
        private Label txt_functionText;
        private TextBox txt_sendData;
        private ComboBox cbx_functionCode;
        private Label label6;
        private TextBox txt_preview;
        private ComboBox cbx_slaveAddress;
        private Label label7;
        private ComboBox cbx_Mode;
        private ToolTip toolTip1;
    }
}
