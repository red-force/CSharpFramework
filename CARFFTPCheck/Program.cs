using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace CARFFTPCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            testConntecion("ftp://192.10.200.33");
        }

        static private void testConntecion(String URL, String UserName="", String Password="", Boolean PassiveMode = true)
        {
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(URL);
            FtpWebResponse res;
            StreamReader reader;

            ftp.Credentials = new NetworkCredential(UserName, Password);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftp.UsePassive = PassiveMode;
            Console.WriteLine(FtpWebRequest.DefaultWebProxy.GetProxy(new Uri(URL)).Port);
            
            try
            {
                using (res = (FtpWebResponse)ftp.GetResponse())
                {
                    reader = new StreamReader(res.GetResponseStream());
                    Console.WriteLine("Cool Beans");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Handling code here.
            }
            Console.ReadLine();
        }

    }
}
