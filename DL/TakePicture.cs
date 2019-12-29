using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DL
{
    public class TakePicture
    {
        private static readonly string imageDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static readonly int SleepAmount = 100;

        static readonly int MaxElapsedTime=30000;

        public static string TakePictureProcess()
        {
            int elapsedTime = 0;

            string path = null;


            using (Process myProcess = new Process())
            {
                myProcess.StartInfo.FileName = "microsoft.windows.camera:";
                myProcess.Start();

                while (elapsedTime < MaxElapsedTime)
                {
                    elapsedTime += SleepAmount;
                    var now = DateTime.Now;

                    string checkPath = imageDirectory + string.Format(@"\Camera Roll\WIN_{0}{1}{2}_{3}_{4}_{5}_Pro.jpg", now.Year.ToString("D4"), now.Month.ToString("D2"), now.Day.ToString("D2"), now.Hour.ToString("D2"), now.Minute.ToString("D2"), now.Second.ToString("D2"));
                    if (File.Exists(checkPath))
                    {
                        path = checkPath;
                        foreach (var pros in Process.GetProcessesByName("WindowsCamera"))
                        {
                            pros.Kill();
                        }

                        break;
                    }


                    Thread.Sleep(SleepAmount);
                }
                return path;
            }
        }
    }
}