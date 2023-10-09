using Microsoft.AspNetCore.Mvc;

using CrudProject.Datos;
using CrudProject.Models;

namespace CrudProject.Controllers
{
    public class ContactoController : Controller

    {
        ContactoDatos _ContactoDatos = new ContactoDatos();
        public IActionResult Listar()

        {
            var oLista=_ContactoDatos.Listar();

            return View(oLista);

        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar (ContactoModel _contactoModel)
        {

            var respuesta = _ContactoDatos.Guardar(_contactoModel);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
         
        }
        public IActionResult Editar(int IdContacto)
        {
            //metodo solo devuelve la vista
            var ocontacto = _ContactoDatos.Obtener(IdContacto);
            return View(ocontacto);
        }
        [HttpPost]
        public IActionResult Editar(ContactoModel oContacto)
        {
            //metodo solo devuelve la vista
           

             if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _ContactoDatos.Editar(oContacto);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }else
                return View();
           
        }
        public IActionResult Eliminar(int IdContacto)
        {
            //metodo solo devuelve la vista
            var ocontacto = _ContactoDatos.Obtener(IdContacto);
            return View(ocontacto);
        }

        [HttpPost]
        public IActionResult Eliminar(ContactoModel oContacto)
        {
            //metodo solo devuelve la vista

            var respuesta = _ContactoDatos.Eliminar(oContacto.IdContacto);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();

        }

    }

}
