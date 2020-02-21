using Microsoft.AspNetCore.Mvc;
using PartyHome.Models;
using PartyHome.Data;
using System.Linq;

namespace PartyHome.Controllers
{
    public class CasaDeShowController : Controller
    {
        private readonly ApplicationDbContext database; 
        public CasaDeShowController(ApplicationDbContext database){
            this.database = database;
        }
        public IActionResult Index(){
            var casadeshow  = database.CasaDeShows.ToList();
            return View(casadeshow);
        }
        public IActionResult Cadastrar(){
            return View();
        }
        public IActionResult Editar(int Id){
            CasaDeShow casadeshow = database.CasaDeShows.First(registro => registro.Id == Id);
            return View("Cadastrar", casadeshow);
        }
        public IActionResult Deletar(int Id){
            CasaDeShow casadeshow = database.CasaDeShows.First(registro => registro.Id == Id);
            database.CasaDeShows .Remove(casadeshow);
            database.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Salvar( CasaDeShow casadeshow){
            if(casadeshow.Id == 0){
                database.CasaDeShows.Add(casadeshow);
            }else{
                CasaDeShow casadeshowDoBanco = database.CasaDeShows.First(registro => registro.Id == casadeshow.Id);

                casadeshowDoBanco.Nome = casadeshow.Nome;
                casadeshowDoBanco.Endereco = casadeshow.Endereco;
            }
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}