using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioFrontOffice.Data;
using BiblioFrontOffice.Models;
using NuGet.Protocol.Plugins;

namespace BiblioFrontOffice.Controllers
{
    public class PaniersController : Controller
    {
        private readonly BiblioFrontOfficeContext _context;

        public PaniersController(BiblioFrontOfficeContext context)
        {
            _context = context;
        }

        // GET: Paniers
        public async Task<IActionResult> Index()
        {
            // await CheckConnexion.v(HttpContext);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
               // HttpContext.Response.Redirect("/Adherents/Login");
                //return View("../Adherents/Login");
                return RedirectToAction("Login", "Adherents");
            }
            return View(await _context.Panier.Where(p => p.IdAdherent == int.Parse(HttpContext.Session.GetString("AdherentId"))).ToListAsync());
            //return View(await _context.Panier.ToListAsync());
            
        }

        // GET: Paniers/Details/5
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

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }


        // GET: Paniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            CheckConnexion.v(HttpContext);

            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panier = await _context.Panier.FindAsync(id);
            if (panier != null)
            {
                _context.Panier.Remove(panier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
            return _context.Panier.Any(e => e.Id == id);
        }
    }
}
