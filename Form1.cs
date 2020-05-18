using System;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        UDP udp = new UDP("192.168.0.187", 4210);

        public Form1()
        {
            InitializeComponent();
            getServerDataOnInterval();
        }

        private void button1_Click(object s, EventArgs e)
        {
            udp.Send("power", 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            udp.Send("power", 0);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            TrackBar track = sender as TrackBar;
            udp.Send("brightness", track.Value);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            int index = box.SelectedIndex + 1;
            udp.Send("mode", index);
        }

        void getServerDataOnInterval()
        {
            Timer timer;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            ServerData serverData = udp.GetServerData();
            if(serverData.power)
            {
                label4.Text = "On";
            } else
            {
                label4.Text = "Off";
            }
        }

        
    }
}