using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone7Mashape.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult GetAPI (int day, int month)
        {

            HttpWebRequest WR = WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/{month}/{day}/date?fragment=true&json=true");
            WR.UserAgent = ".NET Framework Test Client";

            WR.Headers.Add("X-Mashape-Key", "oAstbut85SmshVtJNbZsKlzFixlgp1QIziwjsn8TjVeLoegjNx");

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CardInfo = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(CardInfo);
                ViewBag.Text = JsonData["text"];
                ViewBag.Year = JsonData["year"];

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            string[] MonthNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            ViewBag.Month = MonthNames[month - 1];

            if (day == 1)
            {
                ViewBag.Day = (day + "st");
            }
            else if (day == 2)
            {
                ViewBag.Day = (day + "nd");
            }
            else if (day == 3)
            {
                ViewBag.Day = (day + "rd");
            }
            else
            {
                ViewBag.Day = (day + "th");
            }

            return View();
        }

        public ActionResult GetRandomFact()
        {
            Random rnd = new Random();
            int typenum = rnd.Next(0, 3);

            string[] Types = new string[] { "trivia", "math", "date", "year" };

            string type = Types[typenum];



            HttpWebRequest WR = WebRequest.CreateHttp($"https://numbersapi.p.mashape.com/random/{type}?fragment=true&json=true");
            WR.UserAgent = ".NET Framework Test Client";

            WR.Headers.Add("X-Mashape-Key", "oAstbut85SmshVtJNbZsKlzFixlgp1QIziwjsn8TjVeLoegjNx");

            HttpWebResponse Response;

            try
            {
                Response = (HttpWebResponse)WR.GetResponse();
            }
            catch (WebException e)
            {
                ViewBag.Error = "Exception";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            if (Response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.Error = Response.StatusCode;
                ViewBag.ErrorDescription = Response.StatusDescription;
                return View();
            }

            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string CardInfo = reader.ReadToEnd();

            try
            {
                JObject JsonData = JObject.Parse(CardInfo);
                ViewBag.RanText = JsonData["text"];
                ViewBag.RanNum = JsonData["number"];

            }
            catch (Exception e)
            {
                ViewBag.Error = "JSON Issue";
                ViewBag.ErrorDescription = e.Message;
                return View();
            }

            return View();
        }
    }
}