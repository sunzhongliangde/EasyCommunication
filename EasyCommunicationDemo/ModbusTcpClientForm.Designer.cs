namespace EasyCommunicationDemo
{
    partial class ModbusTcpClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_connect = new Button();
            txt_ipAddress = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txt_port = new TextBox();
            groupBox1 = new GroupBox();
            txt_log = new RichTextBox();
            btn_send = new Button();
            txt_message = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_connect
            // 
            btn_connect.Location = new Point(638, 11);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(150, 46);
            btn_connect.TabIndex = 0;
            btn_connect.Text = "连接";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // txt_ipAddress
            // 
            txt_ipAddress.BorderStyle = BorderStyle.FixedSingle;
            txt_ipAddress.Location = new Point(60, 17);
            txt_ipAddress.Name = "txt_ipAddress";
            txt_ipAddress.Size = new Size(200, 38);
            txt_ipAddress.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(42, 31);
            label1.TabIndex = 2;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 20);
            label2.Name = "label2";
            label2.Size = new Size(92, 31);
            label2.TabIndex = 3;
            label2.Text = "端口号:";
            // 
            // txt_port
            // 
            txt_port.BorderStyle = BorderStyle.FixedSingle;
            txt_port.Location = new Point(432, 18);
            txt_port.Name = "txt_port";
            txt_port.Size = new Size(83, 38);
            txt_port.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txt_log);
            groupBox1.Dock = DockStyle.Bottom;
            groupBox1.Location = new Point(0, 199);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 251);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "日志";
            // 
            // txt_log
            // 
            txt_log.BackColor = SystemColors.Control;
            txt_log.BorderStyle = BorderStyle.None;
            txt_log.Dock = DockStyle.Bottom;
            txt_log.Location = new Point(3, 37);
            txt_log.Name = "txt_log";
            txt_log.Size = new Size(794, 211);
            txt_log.TabIndex = 0;
            txt_log.Text = "";
            // 
            // btn_send
            // 
            btn_send.Location = new Point(638, 149);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(150, 46);
            btn_send.TabIndex = 6;
            btn_send.Text = "发送";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // txt_message
            // 
            txt_message.BorderStyle = BorderStyle.FixedSingle;
            txt_message.Location = new Point(12, 155);
            txt_message.Name = "txt_message";
            txt_message.Size = new Size(610, 38);
            txt_message.TabIndex = 7;
            // 
            // ModbusTcpClientForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txt_message);
            Controls.Add(btn_send);
            Controls.Add(groupBox1);
            Controls.Add(txt_port);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_ipAddress);
            Controls.Add(btn_connect);
            Name = "ModbusTcpClientForm";
            Text = "ModbusTcpClientForm";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_connect;
        private TextBox txt_ipAddress;
        private Label label1;
        private Label label2;
        private TextBox txt_port;
        private GroupBox groupBox1;
        private RichTextBox txt_log;
        private Button btn_send;
        private TextBox txt_message;
    }
}