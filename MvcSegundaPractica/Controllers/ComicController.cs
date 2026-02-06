using Microsoft.AspNetCore.Mvc;
using MvcSegundaPractica.Models;
using MvcSegundaPractica.Repositories;

namespace MvcSegundaPractica.Controllers
{
    public class ComicController : Controller
    {
        RepositoryComic repo;

        public ComicController()
        {
            this.repo = new RepositoryComic();
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            if(comics == null)
            {
                ViewBag.Mensaje = "No se han encontrado comics";
                return View();
            }
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string imagen, string descripcion)
        {
            await this.repo.InsertComicAsync(nombre, imagen, descripcion);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Comic c = this.repo.GetComicById(id);
            if(c == null)
            {
                ViewBag.Mensaje = "No se han detalles del comic";
                return View();
            }
            return View(c);
        }
    }
}
