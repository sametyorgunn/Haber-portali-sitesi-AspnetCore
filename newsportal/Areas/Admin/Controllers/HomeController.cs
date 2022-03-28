using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using newsportal.Abstractions;
using newsportal.Areas.Admin.Models;
using newsportal.Models;

namespace newsportal.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var posts = _newsRepository.GetAllPosts();

            return View(posts);
        }


        //======================== CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel news)
        {
            if (ModelState.IsValid)
            {
                var isCreated = _newsRepository.CreatePost(news);

                if (isCreated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Create", "Home");
        }

        //======================== DELETE
        public IActionResult Delete(int id)
        {
            if (_newsRepository.DeletePost(id))
            {
                return RedirectToAction("Index", "Home");
            }
            throw new Exception("Failed To Delete");
        }

        //======================== EDIT
        public IActionResult Edit(int id)
        {
            var post = _newsRepository.GetPost(id);

            EditNewsViewModel news = new EditNewsViewModel()
            {
                id = post.id,
                FileName = post.FileName,
                Title = post.Title
            };

            return View(news);
        }

        [HttpPost]
        public IActionResult Edit(EditNewsViewModel news)
        {
            if (ModelState.IsValid)
            {
                if (_newsRepository.EditPost(news))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(news);
        }


        public IActionResult Detail(int id)
        {
            var post = _newsRepository.GetPost(id);

            return View(post);
        }

       
    }
}
