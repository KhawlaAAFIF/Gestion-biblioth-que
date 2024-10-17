using BiblioFrontOffice.Data;
using BiblioFrontOffice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BiblioFrontOffice.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly BiblioFrontOfficeContext _context;

        public ReservationsController(BiblioFrontOfficeContext context)
        {
            _context = context;

          

        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
                return RedirectToAction("Login", "Adherents");
            }
            var status = _context.Reservation.Join(
            _context.Status_Reservation,
                r => r.Status,
                s => s.id,
                (r,s) => new
                {
                    r.Id,
                    s.id,
                    s.libelle,
                    s.color,
                    r.Status,
                    r.DateReservation,
                    r.DateDebutEmprunt,
                    r.DateFinEmprunt,
                    r.IdAdherent

                }
                ).Where(p => p.IdAdherent == int.Parse(HttpContext.Session.GetString("AdherentId"))).ToList();
            ViewBag.status = status;


            //return View(await _context.Reservation.Where(p => p.IdAdherent == int.Parse(HttpContext.Session.GetString("AdherentId"))).ToListAsync());
            return View(status);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
                return RedirectToAction("Login", "Adherents");
            }
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            //var listDetail = _context.Detail.Where(d => d.idReservation == id).ToList();
            var listDetail = _context.Detail.Join(
                _context.Document,
                dt=>dt.idDocument,
                doc=>doc.Id,
                (dt, doc) => new
                {
                    doc.Libelle,
                    doc.NomAuteur,
                    dt.idReservation

                }
                ).Where(d => d.idReservation == id).ToList();
            ViewBag.reservation = reservation;
            ViewBag.detail = listDetail;


            return View();
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
                return RedirectToAction("Login", "Adherents");
            }
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateReservation,DateDebutEmprunt,DateFinEmprunt,Status")] Reservation reservation)
        {

            //HttpContext.Session.SetString("AdherentId", "1");
            if (ModelState.IsValid)
            {
                DateTime datereserv = DateTime.Now;
                reservation.DateReservation = datereserv;
                reservation.Status=1;
                reservation.IdAdherent = int.Parse(HttpContext.Session.GetString("AdherentId"));
                _context.Add(reservation);
                await _context.SaveChangesAsync();

                //start add detail

                //List<Panier> list_pan = await _context.Panier.ToListAsync();
                List<Panier> list_pan = await _context.Panier.Where(p => p.IdAdherent == int.Parse(HttpContext.Session.GetString("AdherentId"))).ToListAsync();//FindAsync(a => a.IdAdherent == HttpContext.Session.GetString("AdherentId"));
                foreach(Panier p in list_pan)
                {
                    Console.WriteLine(p.IdDocument);
                    _context.Add(new Detail(p.IdDocument,reservation.Id));
                    _context.Panier.Remove(p);
                }
                    await _context.SaveChangesAsync();
                //end add detail
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        

        

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
                return RedirectToAction("Login", "Adherents");
            }
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                var detail_rows = _context.Detail.Where(d => d.idReservation== reservation.Id).ToList();
                _context.Detail.RemoveRange(detail_rows);
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }

        //GET : Reservations/Panier
        public  IActionResult Panier()
        {
            return View();
        }

    }
}
