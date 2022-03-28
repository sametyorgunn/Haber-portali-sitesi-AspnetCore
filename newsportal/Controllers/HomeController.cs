using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using newsportal.Models;
using newsportal.Abstractions;
using System.Net;
using System.Xml.Linq;

namespace newsportal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsRepository _newsRepository;

        public HomeController(ILogger<HomeController> logger, INewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            var listOfPosts = _newsRepository.GetAllPosts();
            return View(listOfPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //================== SEE DETAILED INFO ABOUT POST
        public IActionResult Detail(int id)
        {
            var post = _newsRepository.GetPost(id);

            return View(post);
        }


        //========================= INCREASE LIKE DISLIKE AND VISITED AMOUNT
        public IActionResult IncreasePostAffect(PostAffect postAffect)
        {
            var changedPost = _newsRepository.IncreasePostAffect(postAffect);
            
            if (changedPost != null)
            {
                var json = JsonConvert.SerializeObject(changedPost);
                return Content(json);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Economy()
        {
            return View();
        }
        public IActionResult Global()
        {
            return View();
        }
        public IActionResult Sport()
        {
            return View();
        }
        public IActionResult Korona()
        {
            return View();
        }

        public IActionResult hava()
        {
            string api = "8c1c03334ce3d960bab323a61adcb27d";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid="+api;
            string baglanti = "https://api.openweathermap.org/data/2.5/weather?q=izmir&mode=xml&lang=tr&units=metric&appid=" + api;
            string bgl = "https://api.openweathermap.org/data/2.5/weather?q=duzce&mode=xml&lang=tr&units=metric&appid=" + api;
            string bg = "https://api.openweathermap.org/data/2.5/weather?q=ankara&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            XDocument d = XDocument.Load(baglanti);
            XDocument b = XDocument.Load(bgl);
            XDocument s = XDocument.Load(bg);

            ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            ViewBag.v5 = d.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            ViewBag.v6 = b.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            ViewBag.v7 = s.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            return View();
        }

       
    }
}
