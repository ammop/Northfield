using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTCF
{
    class Program
    {
        /*  GLOBAL VARS     */
        public static string ROOTDIRECTORY = @"C:\Your\Target\Directory\";
        public static string USERAGENT = @"Your User Agent String";
        public static string HOST = "www.northinfo.com";
        public static string ACCEPT = "application/pdf, */*";

        static void Main(string[] args)
        {

            string url = "https://www.northinfo.com/documents/";

            // As of 2022-MAR-21, the PDFs go up to 1031
            for (int i = 1; i <= 1031; i++)
            {
                string uri = url.ToString() + i.ToString() + ".pdf";
                string file = i.ToString() + ".pdf";
                download(uri, file);
            }
        }

        public static void download(string uri, string file)
        {
            Console.WriteLine("Downloading: {0}", uri.ToString());
            string destination = ROOTDIRECTORY.ToString() + file.ToString();
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("User-Agent", USERAGENT);
                    wc.Headers.Add("Host", HOST);
                    wc.Headers.Add("Accept", ACCEPT);
                    wc.DownloadFile(new System.Uri(uri), destination);
                }
            }
            catch (WebException e)
            {
                var resp = (HttpWebResponse)e.Response;
                if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("404: {0}", uri.ToString());
                }
                else
                {
                    Console.WriteLine("Status: {0}: {1}", resp.StatusCode, uri.ToString());
                }
                return;
            }
        }
    }
}
