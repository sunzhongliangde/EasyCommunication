using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyCommunicationDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_tcpServer_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ModbusTcpServerForm form = new ModbusTcpServerForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            this.Visible = true;

        }

        private void btn_tcpClient_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ModbusTcpClientForm form = new ModbusTcpClientForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            this.Visible = true;
        }
    }
}
