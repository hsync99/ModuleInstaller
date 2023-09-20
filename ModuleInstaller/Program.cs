using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModuleInstaller
{

    internal class Program
    {
        
        //String word = str.Substring(0, str.IndexOf('^'));
        static string diskSpace = "C:\\Users\\";
        static string Name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        static string desktopName = Name.Substring(0, Name.IndexOf('\\'));
        static string path = "\\AppData\\Roaming\\NCALayer\\bundles\\";
        static string exepath = "\\AppData\\Roaming\\NCALayer\\NCALayer.exe";
        static string pathtoResource = "ModuleInstaller.kz.nitec.eup.eupsigner_1.0_1023ad25-d3ed-4569-a628-d61325f02dc6.jar";
       
        static void Main(string[] args)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string username = Name.Replace(desktopName+"\\","");
            string fullpath = diskSpace + username + path;
            string filepath = Path.GetFullPath("kz.nitec.eup.eupsigner_1.0_1023ad25-d3ed-4569-a628-d61325f02dc6.jar");
            //  var fileByteArray = ConvertToByteArray(filepath);
            // CopyFile(filepath, fullpath + Path.GetFileName(filepath));

            try
            {
                Console.WriteLine("Export Module Process Started...");
                using (var stream = assembly.GetManifestResourceStream(pathtoResource))
                {
                    var file = UseStreamDotReadMethod(stream);
                     SaveByteArrayToFileWithStaticMethod(file, fullpath);
                }

                Console.WriteLine("Restart NCALayer...");
                CheckNcaLayerProcess();

                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Read();
            }
           // CheckNcaLayerProcess();
            //Console.WriteLine(fullpath);


        }

       static void SaveByteArrayToFileWithStaticMethod(byte[] data, string filePath)
 => File.WriteAllBytes(filePath + "kz.nitec.eup.eupsigner_1.0_1023ad25-d3ed-4569-a628-d61325f02dc6.jar", data);
       static byte[] UseStreamDotReadMethod(Stream stream)
        {
            byte[] bytes;
            List<byte> totalStream = new List<byte>();
            byte[] buffer = new byte[32];
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                totalStream.AddRange(buffer.Take(read));
            }
            bytes = totalStream.ToArray();
            Console.WriteLine("Export Completed Successfully!");
            return bytes;
        }


       static void CheckNcaLayerProcess()
        {
            try
            {
                Process[] ncaproceses = Process.GetProcessesByName("javaw");
                foreach (var proc in ncaproceses)
                {
                    proc.Kill();
                }
                Thread.Sleep(1000);
                string username = Name.Replace(desktopName + "\\", "");
                string NcaExe = diskSpace + username + exepath;
                Process.Start(NcaExe);
                Task.Delay(1000);
                Console.WriteLine("NcaLayer Started!");
                Console.WriteLine("Now you can close this windows!");
            }
            catch
            {

            }

        }
        public static byte[] ConvertToByteArray(string filePath)
        {
            byte[] fileByteArray = File.ReadAllBytes(filePath);
            return fileByteArray;
        }
    }
}
