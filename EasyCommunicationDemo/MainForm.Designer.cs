namespace EasyCommunicationDemo
{
    partial class MainForm
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
            btn_tcpServer = new Button();
            btn_tcpClient = new Button();
            SuspendLayout();
            // 
            // btn_tcpServer
            // 
            btn_tcpServer.Location = new Point(54, 43);
            btn_tcpServer.Name = "btn_tcpServer";
            btn_tcpServer.Size = new Size(150, 46);
            btn_tcpServer.TabIndex = 0;
            btn_tcpServer.Text = "Tcp Server";
            btn_tcpServer.UseVisualStyleBackColor = true;
            btn_tcpServer.Click += btn_tcpServer_Click;
            // 
            // btn_tcpClient
            // 
            btn_tcpClient.Location = new Point(246, 43);
            btn_tcpClient.Name = "btn_tcpClient";
            btn_tcpClient.Size = new Size(150, 46);
            btn_tcpClient.TabIndex = 1;
            btn_tcpClient.Text = "Tcp Client";
            btn_tcpClient.UseVisualStyleBackColor = true;
            btn_tcpClient.Click += btn_tcpClient_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_tcpClient);
            Controls.Add(btn_tcpServer);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_tcpServer;
        private Button btn_tcpClient;
    }
}