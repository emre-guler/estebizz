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
            return View();
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
                    if (hashPass.ToString() == user.Password)
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(7);
                        var token = md5.ComputeHash(Convert.FromBase64String(user.CreatedAt + user.Email + user.Id));
                        Response.Cookies.Append("EstebizzToken", token.ToString(), option);
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
            var user = _db.Users.FirstOrDefault(x => x.Id == int.Parse(idCookie));
            var md5 = new MD5CryptoServiceProvider();
            var token = md5.ComputeHash(Encoding.ASCII.GetBytes(user.CreatedAt + user.Email + user.Id));
            if (tokenCookie == token.ToString())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Giris");
            }
        }

    }
}
