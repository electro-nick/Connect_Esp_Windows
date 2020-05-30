using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
            label4.Text = serverData.power ? "Вкл" : "Выкл";
            trackBar1.Value = serverData.brightness;
            comboBox1.SelectedIndex = serverData.mode - 1;
            if (serverData.color[0] == 255) comboBox2.SelectedIndex = 0;
            if (serverData.color[1] == 255) comboBox2.SelectedIndex = 1;
            if (serverData.color[2] == 255) comboBox2.SelectedIndex = 2;
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            switch(box.SelectedIndex)
            {
                case 0: 
                    udp.Send("color", "255 0 0");
                break;
                case 1:
                    udp.Send("color", "0 255 0");
                break;
                case 2:
                    udp.Send("color", "0 0 255");
                break;
            }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
            panel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, 10, 10));
        }
    }
}