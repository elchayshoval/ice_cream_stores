using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FileManager
    {
        private static readonly string STORE_DIRECTORY = "/photos/stores/";
        private static readonly string ICE_CREAM_DIRECTORY="/photos/ice creams/";

        public static string AddStoreImage(string directory,string name=null)
        {
            if (name == null)
            {
                name = string.Empty;
            }
            
            string newDirectory =Directory.GetParent( Directory.GetParent( Directory.GetCurrentDirectory()).ToString())+  STORE_DIRECTORY +  name + CurrentTime() + getTypeFile(directory);
            File.Move(directory,  newDirectory);
            return newDirectory;
        }

        private static string CurrentTime()
        {
            var now = DateTime.Now;
            return string.Format("_{0}{1}{2}_{3}_{4}_{5}_", now.Year.ToString("D4"), now.Month.ToString("D2"), now.Day.ToString("D2"), now.Hour.ToString("D2"), now.Minute.ToString("D2"), now.Second.ToString("D2"));
        }

        public static string AddIceCreamImage(string directory, string name=null)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            string newDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + STORE_DIRECTORY + name + CurrentTime() + getTypeFile(directory);
            File.Move(directory, newDirectory);
            return newDirectory;
        }

        private static string getTypeFile(string directory)
        {
            return Path.GetExtension(directory);
        }
    }
}
