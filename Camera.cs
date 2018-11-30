using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CamControler
{
    class Camera
    {
        public bool existDevice { get; set; }
        private FilterInfoCollection videoDevice;
        private VideoCaptureDevice videoFont; 

        public Camera() {
            videoFont = null;
            videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            existDevice = false;
        }

        public List<String> loadDevices() {
            List<String> devices = new List<String>();
            if (videoDevice.Count == 0)
            {
                existDevice = false;
                return devices;
            }
            existDevice = true;
            for (int i = 0; i < videoDevice.Count; i++) {
                devices.Add(videoDevice[i].ToString());
            }
            return devices;
        }

        public void stopVideoFont() {
            if(videoFont != null && videoFont.IsRunning)
            {
                videoFont.SignalToStop();
                videoFont = null;
            }
        }

        public VideoCaptureDevice getVideoFont(int index)
        {
            videoFont = new VideoCaptureDevice(videoDevice[index].MonikerString);
            return videoFont;
        }
    }
}
