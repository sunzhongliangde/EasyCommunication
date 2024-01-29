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
            txt_tcpPort = new TextBox();
            label1 = new Label();
            btn_tcpListener = new Button();
            groupBox1 = new GroupBox();
            txt_log = new RichTextBox();
            txt_sendMsg = new TextBox();
            btn_send = new Button();
            groupBox1.SuspendLayout();
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
            btn_tcpListener.Location = new Point(986, 9);
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
            groupBox1.Dock = DockStyle.Bottom;
            groupBox1.Location = new Point(0, 363);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1160, 349);
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
            txt_log.Size = new Size(1154, 309);
            txt_log.TabIndex = 4;
            txt_log.Text = "";
            // 
            // txt_sendMsg
            // 
            txt_sendMsg.BorderStyle = BorderStyle.FixedSingle;
            txt_sendMsg.ForeColor = SystemColors.WindowText;
            txt_sendMsg.Location = new Point(12, 311);
            txt_sendMsg.Name = "txt_sendMsg";
            txt_sendMsg.Size = new Size(968, 38);
            txt_sendMsg.TabIndex = 4;
            // 
            // btn_send
            // 
            btn_send.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_send.Location = new Point(986, 305);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(150, 46);
            btn_send.TabIndex = 5;
            btn_send.Tag = "1";
            btn_send.Text = "发送";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // ModbusTcpServerForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 712);
            Controls.Add(btn_send);
            Controls.Add(txt_sendMsg);
            Controls.Add(groupBox1);
            Controls.Add(btn_tcpListener);
            Controls.Add(label1);
            Controls.Add(txt_tcpPort);
            Name = "ModbusTcpServerForm";
            Text = "ModbusTcpServer";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_tcpPort;
        private Label label1;
        private Button btn_tcpListener;
        private GroupBox groupBox1;
        private RichTextBox txt_log;
        private TextBox txt_sendMsg;
        private Button btn_send;
    }
}
