using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPROJE.Models;

namespace WEBPROJE.Controllers
{
    public class MakaleController : YetkiliController
    {
        WebDB db = new WebDB();
        
        // GET: Makale
        public ActionResult Index()
        {
            var makaleler = db.Makales.ToList();
            return View(makaleler);
        }

        // GET: Makale/Details/5
        public ActionResult Details(int id)
        {
            var makale = db.Makales.Where(i => i.id == id).SingleOrDefault();
            return View(makale);
        }
        public ActionResult KisiMakaleListesi()
        {
            var kullaniciadi = Session["username"].ToString();
            var makaleler = db.Kullanicis.Where(a => a.KullaniciAdi == kullaniciadi).SingleOrDefault().Makales.ToList();
            return View(makaleler);
        }

        // GET: Makale/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd");
            return View();
        }

        // POST: Makale/Create
        [HttpPost]
        public ActionResult Create(Makale model)
        {
            try
            {
                string kullaniciadi = Session["username"].ToString();
                var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
                model.KullaniciId = kullanici.id;
                model.MakaleTarihi = DateTime.Now;

                db.Makales.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index","Kullanici");
            }
            catch
            {
                return View();
            }
        }

        // GET: Makale/Edit/5
        public ActionResult Edit(int id)
        {
            string kullaniciadi = Session["username"].ToString();
            var makale = db.Makales.Where(i => i.id == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            if (makale.Kullanici.KullaniciAdi == kullaniciadi)
            {
                ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAd");
                return View(makale);

            }
            return HttpNotFound();
        }

        // POST: Makale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Makale model)
        {
            try
            {
                var makale = db.Makales.Where(i => i.id == id).SingleOrDefault();
                makale.Baslik = model.Baslik;
                makale.İcerik = model.İcerik;
                makale.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Makale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Makale/Delete/5
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
