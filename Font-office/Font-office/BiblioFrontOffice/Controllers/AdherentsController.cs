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
    public class AdherentsController : Controller
    {
        private readonly BiblioFrontOfficeContext _context;

        public AdherentsController(BiblioFrontOfficeContext context)
        {
            _context = context;
        }


        // GET: Adherents/Login
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AdherentId")))
            {
                return RedirectToAction("Index", "Documents");
            }
            return View();
        }
        // GET: Adherents/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();// .SetString("AdherentId", null);
            return RedirectToAction("Login", "Adherents");

        }
        // POST: Adherents/Checklogin
        [HttpPost]
        public IActionResult Checklogin(string email, string password)
        {
            var ad = _context.Adherent.Where(a => a.email == email & a.password == password).ToList();
            if (ad.Count() == 0)
            {
                ViewBag.ErrorMessage = "Invalid login ";

                return View("Login");
            }
            HttpContext.Session.SetString("AdherentId", ad[0].id.ToString());

            return RedirectToAction("Index","Documents");
        }
        // GET: Adherents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adherents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nom,prenom,email,password,dateinscription")] Adherent adherent)
        {
            // Check if the email already exists
            if (_context.Adherent.Any(a => a.email == adherent.email))
            {
                ViewBag.ErrorMessage = "Un compte avec cet email existe déjà. ";

                return View("Create");
            }

            if (ModelState.IsValid)
            {
                DateTime dateinscrip = DateTime.Now;
                adherent.dateinscription = dateinscrip;
                _context.Add(adherent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Documents");
            }

            return RedirectToAction("Index", "Documents");
        }


        // GET: Adherents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adherent = await _context.Adherent.FindAsync(id);
            if (adherent == null)
            {
                return NotFound();
            }
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nom,prenom,email,password,dateinscription")] Adherent adherent)
        {
            if (id != adherent.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adherent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdherentExists(adherent.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adherent);
        }


        private bool AdherentExists(int id)
        {
            return _context.Adherent.Any(e => e.id == id);
        }
    }
}
