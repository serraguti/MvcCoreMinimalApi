using Microsoft.AspNetCore.Mvc;
using MvcCoreMinimalApi.Models;
using MvcCoreMinimalApi.Services;

namespace MvcCoreMinimalApi.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceMinimalApiPersonajes service;

        public SeriesController(ServiceMinimalApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            return View(series);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie serie)
        {
            await this.service.AddSerieAsync(serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarPersonajeSerie(int idserie)
        {
            List<Personaje> personajes = 
                await this.service.FindPersonajesSerieAsync(idserie);
            return View(personajes);
        }
    }
}
