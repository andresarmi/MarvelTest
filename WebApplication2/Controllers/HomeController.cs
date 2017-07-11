using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

           
            return View();
        }

        [HttpGet]
        public ActionResult GetCharacter()
        {
            string hash = CreateMD5("5ed40efac730b66205ca6e0d96832bfa6878aacb2465d832d1201011b5bc1d41be69cb8b5");

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://gateway.marvel.com/v1/public/characters?limit=100&ts=5&apikey=465d832d1201011b5bc1d41be69cb8b5&hash=" + hash.ToLower());
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(text);

            return PartialView(obj);
        }

        
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


        [HttpGet]
        public ActionResult GetComic()
        {
            string hash = CreateMD5("5ed40efac730b66205ca6e0d96832bfa6878aacb2465d832d1201011b5bc1d41be69cb8b5");

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://gateway.marvel.com/v1/public/comics?limit=3&ts=5&apikey=465d832d1201011b5bc1d41be69cb8b5&hash=" + hash.ToLower());
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            //Rootobject obj = JsonConvert.DeserializeObject<Rootobject>(text);

            return PartialView();
        }

    }
}