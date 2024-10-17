using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioFrontOffice.Data;
using BiblioFrontOffice.Models;

namespace BiblioFrontOffice.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly BiblioFrontOfficeContext _context;

        public DocumentsController(BiblioFrontOfficeContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Document.ToListAsync());
        }

      
        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.Id == id);
        }

        // POST: Documents/reserver/5
        //[HttpPost, ActionName("Reserver")]
        public async Task<IActionResult> Reserver(int id)
        {
            //TODO: ajout dans login et supprimer cette line apres
            //HttpContext.Session.SetString("idAdherent", "1");

            //ajoout si non deja exist
            var p = await _context.Panier
                .FirstOrDefaultAsync(m => m.IdDocument == id & m.IdAdherent== int.Parse(HttpContext.Session.GetString("AdherentId")));
            if (p == null)
            {
                Panier panier = new Panier(int.Parse(HttpContext.Session.GetString("AdherentId")),id);
                    _context.Add(panier);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
            }
            else
            {
                //TODO: ajouter message deja exist dans panier (paramter message dans url)
                return RedirectToAction(nameof(Index));
            }

/*
            if(HttpContext.Session.GetString("Panier") is null)
            {
                HttpContext.Session.SetString("Panier", id + "");
            }
            else
            {

                //12,45,10
                List<string> panier =HttpContext.Session.GetString("Panier").Split(',').ToList();
                panier.Add(id.ToString());

                HttpContext.Session.SetString("Panier", String.Join(",",panier));

            }
            return HttpContext.Session.GetString("Panier");
*/
            //var reservation = await _context.Reservation.FindAsync(id);
            //if (reservation != null)
            //{
            //    _context.Reservation.Remove(reservation);
            //}

            //await _context.SaveChangesAsync();
            //  return  RedirectToAction(nameof(Index));

        }
    }
}
