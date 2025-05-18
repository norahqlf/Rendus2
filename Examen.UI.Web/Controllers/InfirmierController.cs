// Examen.Web/Controllers/InfirmierController.cs
using Examen.ApplicationCore.Domain;
using Examen.Infrastructure;
using Examen.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Examen.Web.Controllers
{
    public class InfirmierController : Controller
    {
        private readonly MedicalAnalysisContext _context;

        public InfirmierController(MedicalAnalysisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new InfirmierViewModel
            {
                Laboratoires = _context.Laboratoires.ToList(),
                Specialites = Enum.GetNames(typeof(Specialite)).ToList()
            };
            return View(viewModel);
        }

        // Examen.Web/Controllers/InfirmierController.cs
        [HttpGet]
        public IActionResult Index()
        {
            var infirmiers = _context.Infirmiers
                .Include(i => i.Laboratoire)
                .ToList();
            return View(infirmiers);
        }

        // Examen.Web/Controllers/InfirmierController.cs
        [HttpGet]
        public IActionResult Patients(string codeInfirmier)
        {
            var patients = _context.Bilans
                .Where(b => b.CodeInfirmier == int.Parse(codeInfirmier))
                .Include(b => b.Patient)
                .Select(b => b.Patient)
                .Distinct()
                .ToList();
            ViewBag.CodeInfirmier = codeInfirmier;
            return View(patients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InfirmierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var infirmier = new Infirmier
                {
                    CodeInfirmier = viewModel.CodeInfirmier,
                    Nom = viewModel.Nom,
                    Specialite = viewModel.Specialite,
                    LaboratoireId = viewModel.LaboratoireId
                };
                _context.Infirmiers.Add(infirmier);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Laboratoires = _context.Laboratoires.ToList();
            viewModel.Specialites = Enum.GetNames(typeof(Specialite)).ToList();
            return View(viewModel);
        }
    }
}