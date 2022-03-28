using Microsoft.AspNetCore.Hosting;
using newsportal.Abstractions;
using newsportal.Areas.Admin.Models;
using newsportal.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace newsportal.Models
{
    public class NewsRepository : INewsRepository
    {
        NewsContext _context;
        private readonly IWebHostEnvironment _webHost;

        public NewsRepository(NewsContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        //======================== CREATE
        public bool CreatePost(NewsViewModel news)
        {
            var nameOfImage = Path.GetFileNameWithoutExtension(news.FormFile.FileName);
            var extensionOfImage = Path.GetExtension(news.FormFile.FileName);
            var guid = Guid.NewGuid();

            var newFileName = nameOfImage + guid + extensionOfImage;


            var rootPath = Path.Combine(_webHost.WebRootPath, "images", "PostsGallery", newFileName);

            using (var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                news.FormFile.CopyTo(fileStream);
            }

            var newPost = new News()
            {
                PostDate = DateTime.Now.ToString("MMMM dd"),
                PostTime = DateTime.Now.ToString("HH:mm"),
                LikeAmount = 0,
                DislikeAmount = 0,
                VisitedAmount = 0,
                Title = news.Title,
                FileName = newFileName
            };

            _context.News.Add(newPost);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //================================ GET ALL POSTS
        public List<News> GetAllPosts()
        {
            var unsortedPost = _context.News.ToList();

            //========= MAKES LAST POST TO BE SHOWN FIRST
            List<News> SortedList = unsortedPost.OrderByDescending(x => x.id).ToList();

            return SortedList;
        }

        //============================== GET DETAILED INFO
        public News GetPost(int id)
        {
            var post = _context.News.FirstOrDefault(x => x.id == id);
            return post;
        }

        //=============================== DELETE POST
        public bool DeletePost(int id)
        {
            var post = _context.News.FirstOrDefault(x => x.id == id);
            var imageName = post.FileName;

            _context.Remove(post);
            var result = _context.SaveChanges();


            var rootPath = Path.Combine(_webHost.WebRootPath, "images", "PostsGallery", imageName);

            File.Delete(rootPath);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //================================ EDIT POST
        public bool EditPost(EditNewsViewModel news)
        {
            var post = _context.News.FirstOrDefault(x => x.id == news.id);

            if (news.FormFile != null)
            {
                var nameOfImage = Path.GetFileNameWithoutExtension(news.FormFile.FileName);
                var extensionOfImage = Path.GetExtension(news.FormFile.FileName);
                var guid = Guid.NewGuid();

                var newFileName = nameOfImage + guid + extensionOfImage;


                var rootPath = Path.Combine(_webHost.WebRootPath, "images", "PostsGallery", newFileName);

                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    news.FormFile.CopyTo(fileStream);
                }

                news.FileName = newFileName;


                var previousFileName = post.FileName;
                var deleteImage = Path.Combine(_webHost.WebRootPath, "images", "PostsGallery", previousFileName);
                File.Delete(deleteImage);
            }


            post.FileName = news.FileName;
            post.Title = news.Title;

            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }


        //================ INCREASE LIKE DISLIKE AND VISITED AMOUNT
        public News IncreasePostAffect(PostAffect postAffect)
        {
            var post = _context.News.FirstOrDefault(x => x.Title == postAffect.PostTitle);

            if (postAffect.Category == 1)
            {
                post.LikeAmount++;
            }
            else if (postAffect.Category == 2)
            {
                post.DislikeAmount++;
            }
            else
            {
                post.VisitedAmount++;
            }

            var result = _context.SaveChanges();

            if (result > 0)
            {
                return post;
            }
            else
            {
                return post;
            }
        }
    }
}
