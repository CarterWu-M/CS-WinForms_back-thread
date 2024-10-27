using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace background_thread
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.LightGray;
            btnCancel.BackColor = Color.LightBlue;

            await Task.Run(() => {
                testTask();
            });

            btnStart.BackColor = Color.LightGreen;
            btnCancel.BackColor = Color.LightGray;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._cancellationTokenSource?.Cancel();
        }
        private void testTask()
        {
            this._cancellationTokenSource?.Dispose();
            this._cancellationTokenSource = new CancellationTokenSource();

            try
            {
                for (uint i = 0; 10 > i; i++)
                {
                    Thread.Sleep(1000);
                    txt1.Invoke(new Action(() => txt1.Text = i.ToString()));
                    txt1.Invoke(new Action(() => txt1.Refresh()));
                    this._cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Task has been Cancend!");
            }
        }
    }
}
