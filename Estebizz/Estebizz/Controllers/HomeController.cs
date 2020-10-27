using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estebizz.Models;
using Estebizz.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Text;
using Estebizz.Models.Entities;
using System.IO;
using Estebizz.Models.ViewModels;

namespace Estebizz.Controllers
{
    public class HomeController : Controller
    {
        readonly EstebizzContext _db;
        public HomeController(EstebizzContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/hakkimizda")]
        public IActionResult Hakkimizda()
        {
            return View();
        }

        [Route("/iletisim")]
        public IActionResult Iletisim()
        {
            return View();
        }

        [Route("/giris")]
        public IActionResult Giris()
        {
            string tokenCookie = Request.Cookies["EstebizzToken"];
            string idCookie = Request.Cookies["EstebizzId"];
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == int.Parse(idCookie));
                var md5 = new MD5CryptoServiceProvider();
                var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
                StringBuilder tokenReadable = new StringBuilder();
                for (int i = 0; i < token.Length; i++)
                {
                    tokenReadable.Append(token[i].ToString("x2"));
                }
                if (tokenCookie == tokenReadable.ToString())
                {
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost("/giris")]
        public IActionResult GirisKontrol(string email, string password)
        {
            if (!String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(password))
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    var md5 = new MD5CryptoServiceProvider();
                    var hashPass = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                    StringBuilder passwordReadable = new StringBuilder();
                    for (int i = 0; i < hashPass.Length; i++)
                    {
                        passwordReadable.Append(hashPass[i].ToString("x2"));
                    }
                    if (passwordReadable.ToString() == user.Password)
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(7);
                        var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
                        StringBuilder tokenReadable = new StringBuilder();
                        for (int i = 0; i < token.Length; i++)
                        {
                            tokenReadable.Append(token[i].ToString("x2"));
                        }
                        Response.Cookies.Append("EstebizzToken", tokenReadable.ToString(), option);
                        Response.Cookies.Append("EstebizzId", user.Id.ToString(), option);
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("Giris");
                    }
                }
                else
                {
                    return RedirectToAction("Giris");
                }
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

        [Route("/yonetici-panel")]
        public IActionResult AdminPanel()
        {
            string tokenCookie = Request.Cookies["EstebizzToken"];
            string idCookie = Request.Cookies["EstebizzId"];
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == int.Parse(idCookie));
                var md5 = new MD5CryptoServiceProvider();
                var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
                StringBuilder tokenReadable = new StringBuilder();
                for (int i = 0; i < token.Length; i++)
                {
                    tokenReadable.Append(token[i].ToString("x2"));
                }
                if (tokenCookie == tokenReadable.ToString())
                {
                    BlogViewModel BlogVM = new BlogViewModel()
                    {
                        Blogs = _db.Blogs.ToList()
                    };
                    return View(BlogVM);
                }
                else
                {
                    return RedirectToAction("Giris");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Giris");
            }

        }

        [HttpPost("/yonetici-panel")]
        public IActionResult AddBlog(string title, string content, IFormFile photo)
        {
            string tokenCookie = Request.Cookies["EstebizzToken"];
            string idCookie = Request.Cookies["EstebizzId"];
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == int.Parse(idCookie));
                var md5 = new MD5CryptoServiceProvider();
                var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
                StringBuilder tokenReadable = new StringBuilder();
                for (int i = 0; i < token.Length; i++)
                {
                    tokenReadable.Append(token[i].ToString("x2"));
                }
                if (tokenCookie == tokenReadable.ToString())
                {
                    var currentDirectory = Directory.GetCurrentDirectory();
                    if (photo != null)
                    {
                        var extension = Path.GetExtension(photo.FileName);
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        var path = Path.Combine(currentDirectory, "uploads", fileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            photo.CopyTo(stream);
                        }
                        string blogUrl = title.ToLower();
                        foreach (var item in blogUrl)
                        {
                            blogUrl.Replace(" ", "-");
                        }
                        var newBlog = new Blog
                        {
                            BlogUrl = blogUrl,
                            Content = content,
                            CreatedAt = DateTime.Now,
                            Title = title,
                            Path = path,
                            Extension = extension,
                            FileName = fileName
                        };
                        _db.Blogs.Add(newBlog);
                        _db.SaveChanges();
                        return RedirectToAction("AdminPanel");
                    }
                    else
                    {
                        return RedirectToAction("AdminPanel");
                    }
                }
                else
                {
                    return RedirectToAction("Giris");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Giris");
            }
        }

        [HttpPost("/yonetici-panel/blog-sil")]
        public IActionResult RemoveBlog(int blogId)
        {
            string tokenCookie = Request.Cookies["EstebizzToken"];
            string idCookie = Request.Cookies["EstebizzId"];
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == int.Parse(idCookie));
                var md5 = new MD5CryptoServiceProvider();
                var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
                StringBuilder tokenReadable = new StringBuilder();
                for (int i = 0; i < token.Length; i++)
                {
                    tokenReadable.Append(token[i].ToString("x2"));
                }
                if (tokenCookie == tokenReadable.ToString())
                {
                    var blogData = _db.Blogs.FirstOrDefault(x => x.Id == blogId);
                    _db.Blogs.Remove(blogData);
                    var control = _db.SaveChanges();
                    if(control > 0)
                    {
                        return Json(new
                        {
                            data = true
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            data = false
                        });
                    }
                }
                else
                {
                    return RedirectToAction("Giris");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Giris");
            }
        }
    }
}
