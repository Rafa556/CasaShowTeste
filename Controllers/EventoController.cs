using Microsoft.AspNetCore.Mvc;
using PartyHome.Models;
using PartyHome.Data;
using System.Linq;

namespace PartyHome.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext database; 
        public EventoController(ApplicationDbContext database){
            this.database = database;
        }
        public IActionResult Index(){
            var evento  = database.Eventos.ToList();
            return View(evento);
        }
        public IActionResult Cadastrar(){
            return View();
        }
        public IActionResult Editar(int Id){
            Evento evento = database.Eventos.First(registro => registro.Id == Id);
            return View("Cadastrar", evento);
        }
        public IActionResult Deletar(int Id){
           Evento evento = database.Eventos.First(registro => registro.Id == Id);
            database.Eventos.Remove(evento);
            database.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Comprar(){
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult Salvar( Evento evento){
            if(evento.Id == 0){
                database.Eventos.Add(evento);
            }else{
               Evento eventoDoBanco = database.Eventos.First(registro => registro.Id == evento.Id);

                eventoDoBanco.Event = evento.Event;
                eventoDoBanco.Capacidade = evento.Capacidade;
                eventoDoBanco.QtdIngressos = evento.QtdIngressos;
                eventoDoBanco.Data = evento.Data;
                eventoDoBanco.Custo = evento.Custo;
                eventoDoBanco.Local = evento.Local;
                eventoDoBanco.Genero = evento.Genero;

            }
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}