using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    List<Alumnos> lista = db.Alumnos.Where(a => a.Edad >= 18).ToList(); // Muestra sólo los mayores de 18 años. LINQ.
                    return View(lista);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Agregar2()
        {
            return View();
        }

        public ActionResult ListaCiudades()
        {
            using (var db = new AlumnosContext())
            {
                return PartialView(db.Ciudad.ToList());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumnos a)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                using (var db = new AlumnosContext())
                {
                    a.FechaRegistro = DateTime.Now;
                    db.Alumnos.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Error al registrar alumno " +  ex.Message);
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //Alumnos al = db.Alumnos.Where(a => a.Id == id).FirstOrDefault(); //Lambda y LINQ
                    Alumnos al = db.Alumnos.Find(id); //Este se usa cuando se usa sólo una clave primaria
                    return View(al);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

                
        }

        [HttpPost]
        public ActionResult Editar(Alumnos alForm)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                using (var db = new AlumnosContext())
                {
                    Alumnos al = db.Alumnos.Find(alForm.Id);
                    al.Nombres = alForm.Nombres;
                    al.Apellidos = alForm.Apellidos;
                    al.Edad = alForm.Edad;
                    al.Sexo = alForm.Sexo;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult DetallesAlumno (int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumnos alu = db.Alumnos.Find(id);
                    return View(alu);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EliminarAlumno(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumnos alu = db.Alumnos.Find(id);
                    db.Alumnos.Remove(alu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}