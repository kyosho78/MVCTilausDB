using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTilausDB.Models;
using MVCTilausDB.ViewModels;

namespace MVCTilausDB.Controllers
{
    public class TilauksetController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: Tilaukset
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);
                return View(tilaukset.ToList());
            }
        }

        // GET: Tilaukset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // GET: Tilaukset/Create
        public ActionResult Create()
        {
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
            return View();
        }

        // POST: Tilaukset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // POST: Tilaukset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // POST: Tilaukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Ordersummary()
        {
            var orderSummary = from t in db.Tilaukset
                               join ti in db.Tilausrivit on t.TilausID equals ti.TilausID
                               join tu in db.Tuotteet on ti.TuoteID equals tu.TuoteID
                               join a in db.Asiakkaat on t.AsiakasID equals a.AsiakasID
                               

                               select new OrderSummaryData
                               {
                                   TilausID = t.TilausID,
                                   TuoteID = tu.TuoteID,
                                   AsiakasID = a.AsiakasID,

                                   AsiakkaanNimi = a.Nimi,
                                 

                                   Toimitusosoite = t.Toimitusosoite,
                                   
                                   Tilauspvm = (DateTime)t.Tilauspvm,
                                   Toimituspvm = (DateTime)t.Toimituspvm,

                                   Maara = (int)ti.Maara,
                                   Ahinta = (decimal)ti.Ahinta,

                                   TuotteenNimi = tu.Nimi,
                                   ahinta = (decimal)tu.Ahinta





                               };

            ViewBag.TotalOrders = orderSummary.Count();


            return View(orderSummary);
        }

        public ActionResult TilausOtsikot()
        {
                var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);
                return View(tilaukset.ToList()); 
        }

        public ActionResult _TilausRivit(int? tilausid)
        {
            var orderRowsList = from ti in db.Tilausrivit
                               join tu in db.Tuotteet on ti.TuoteID equals tu.TuoteID
                               where ti.TilausID == tilausid


                               select new OrderRows
                               {
                                   TilausID = (int)ti.TilausID,
                                   TuoteID = tu.TuoteID,
                                   Maara = (int)ti.Maara,
                                   Ahinta = (decimal)ti.Ahinta,
                                   TuotteenNimi = tu.Nimi,
                               };


            return PartialView(orderRowsList);
        }

    }
}
