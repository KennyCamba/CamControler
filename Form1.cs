using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CamControler
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort arduinoPort;
        private Camera camera;
        private bool isRun;
        Bitmap bitmap = null;

        public Form1()
        {
            InitializeComponent();
            isRun = true;
            arduinoPort = new System.IO.Ports.SerialPort();
            arduinoPort.PortName = "COM7";
            arduinoPort.BaudRate = 9600;
            arduinoPort.Open();
            this.FormClosing += closeForm;
            camera = new Camera();
            loadCombo();
            this.start.Click += startEvt;
            arduinoPort.DataReceived += takeFoto;
        }

        private void takeFoto(object sender, SerialDataReceivedEventArgs e)
        {
            string read = arduinoPort.ReadLine();
            char[] aa = read.ToCharArray();
            if (aa.Length > 0)
            {
                char a = aa[0];
                Console.WriteLine("Serial: " + a);
                if (a == 'm' && bitmap != null)
                {
                    Console.WriteLine("Entroooooooooo");
                    var dir = System.IO.Directory.GetCurrentDirectory() + "\\foto_" + DateTime.Now.Millisecond + ".png";
                    bitmap.Save(dir, ImageFormat.Png);

                    Console.WriteLine("Listo");
                }
            }
        }

        private void startEvt(Object sender, EventArgs e)
        {
            if (camera.existDevice)
            {
                VideoCaptureDevice vd = camera.getVideoFont(camaras.SelectedIndex);
                vd.NewFrame += create;
                vd.Start();
            }
        }

        private void create(object sender, NewFrameEventArgs eventArgs)
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            picture.Image = bitmap;
            
        }

        private void loadCombo()
        {
            List<String> list = camera.loadDevices();
            for (int i = 0; i < list.Count; i++) {
                camaras.Items.Add(list[i]);
                //camaras.Text = camaras.Items[i].ToString();
            }
        }

        private void closeForm(object sender, FormClosingEventArgs e)
        {
            //if (arduinoPort.IsOpen)
            // arduinoPort.Close();
            camera.stopVideoFont();
            arduinoPort.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            camera.stopVideoFont();
            var dir = System.IO.Directory.GetCurrentDirectory() + "\\foto_" + DateTime.Now.Millisecond + ".png";
            picture.Image.Save(dir, ImageFormat.Png);
        }
    }
}
