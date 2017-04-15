using System;
using System.Threading;
using System.IO;
using System.Xml;

namespace HILPcUsage
{
    class Program
    {

        static void Main(string[] args)
        {
            while (1 == 1)
            {
                updateFile();
                Thread.Sleep(1000);
            }
        }


        /*
         *1-Belirli bir aralıkta sürekli çalıştır.
         *2.1-Süreçleri al.
         *2.2-Süreçlerin Ram,Cpu larını hesaplayıp belirli bir yapıda tut.
         *2.3-Benzer olanları birleştir.
         *3-Xml dosyasını düzenle
         *3.1-Varsa hazır dökümanı alıp üstüne ekle.
         *3.2-Yoksa xml dökümanını ve dosyayı oluştur.
         */
        private static void updateFile()
        {

            Console.WriteLine("OLLEY BE");

            //2
            ProcessInfo[] processList = ProcessController.listProcesses();
            foreach (ProcessInfo pi in processList)
            {
                if (pi.CPUUsage > 0)
                {
                    Console.WriteLine("{0}  --   {1} - {2}", pi.Name, pi.CPUUsage, pi.RAMUsage);
                }
            }


            //3
            string xmlPath = "C:/test.xml";

            if (File.Exists(xmlPath))
            {

            }else
            {

            }

            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
        }
    }
}
