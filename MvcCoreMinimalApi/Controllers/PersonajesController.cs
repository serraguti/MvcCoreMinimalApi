using Microsoft.AspNetCore.Mvc;
using MvcCoreMinimalApi.Models;
using MvcCoreMinimalApi.Services;

namespace MvcCoreMinimalApi.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceMinimalApiPersonajes service;

        public PersonajesController(ServiceMinimalApiPersonajes service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync(username, password);
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.AddPersonajeAsync(personaje);
            return RedirectToAction("Index");
        }
    }
}
