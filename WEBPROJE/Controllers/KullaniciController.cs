using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPROJE.Models;

namespace WEBPROJE.Controllers
{
    public class KullaniciController : Controller
    {
        WebDB db = new WebDB();
        // GET: Kullanici
        public ActionResult Index()
        {
            string kullaniciadi = Session["username"].ToString();
            var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
            ViewBag.deneme = kullanici.isim;
            return View();
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int id)
        {
            var kisi = db.Kullanicis.Where(i => i.id == id).SingleOrDefault();
            return View(kisi);
        }
        public ActionResult Profil()
        {
            string kullanciadi = Session["username"].ToString();
            var kisi = db.Kullanicis.Where(i => i.KullaniciAdi == kullanciadi).SingleOrDefault();
            return View(kisi);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici model)
        {
            try
            {
                var varmi = db.Kullanicis.Where(i => i.KullaniciAdi == model.KullaniciAdi).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                }
                if (varmi.Sifre == model.Sifre)
                {
                    Session["username"] = model.KullaniciAdi;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }

        }
        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        [HttpPost]
        public ActionResult Create(Kullanici model)
        {
            try
            {

                var varmi = db.Kullanicis.Where(i => i.KullaniciAdi == model.KullaniciAdi).SingleOrDefault();
                if (varmi != null)
                {
                    return View();
                }

                if (string.IsNullOrEmpty(model.Sifre))
                {
                    return View();
                }
                model.Tarih = DateTime.Now;
                model.YetkiId = 1;
                db.Kullanicis.Add(model);
                db.SaveChanges();
                Session["username"] = model.KullaniciAdi;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int id)
        {
            string kullaniciadi = Session["username"].ToString();
            var user = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();

            if(user.id==id)
              {
                return View(user);
                

            }
            else if(user.YetkiId > 3)
            {
                var kisi = db.Kullanicis.Where(i => i.id == id).SingleOrDefault();
                return View(kisi);
            }
            return HttpNotFound();
        }

        // POST: Kullanici/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Kullanici model)
        {
            try
            {
                var kisi = db.Kullanicis.Where(i => i.id == id).SingleOrDefault();
                kisi.isim = model.isim;
                kisi.Soyisim = model.Soyisim;
                kisi.Sifre = model.Sifre;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kullanici/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
