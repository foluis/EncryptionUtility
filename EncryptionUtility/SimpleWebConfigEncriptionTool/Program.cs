using log4net;
using System;
using System.IO;
using System.Linq;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SimpleWebConfigEncriptionTool
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            Init();
            //Console.ReadLine();
        }

        private static void Init()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            log.Debug($"BaseDirectory: {baseDirectory}");
            string[] files = Directory.GetFiles(baseDirectory);

            if (files.Count() > 0)
            {
                log.Debug($"BaseDirectory: {baseDirectory}");
            }

            foreach (var file in files)
            {
                log.Debug(file);
            }

            //Rename file larga.config -> web.config
            //Encript web.config file
            //Rename  web.config to larga.config
        }
    }
}
