using ApiInscripción.Models.ViewModels;
using ApiInscripción.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiInscripción.Controllers
{
    public class InscribirController : ApiController
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ResponseType(typeof(Boolean))]
        public IHttpActionResult PostAlumno(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var a = AlumnosRepository.Insertar(alumno.Nombre, alumno.Apellido, alumno.IdTipoDocumento,
                    alumno.NroDocumento, alumno.FechaNacimiento, alumno.EMail, alumno.Direccion,
                    alumno.Sexo);
                _log.Info($"Se creó el alumno ID {a.Id}");
                return Ok();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return InternalServerError();                
            }
        }

        //[ResponseType(typeof(BookDto))]
        //public async Task<IHttpActionResult> PostBook(Book book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Books.Add(book);
        //    await db.SaveChangesAsync();

        //    // New code:
        //    // Load author name
        //    db.Entry(book).Reference(x => x.Author).Load();

        //    var dto = new BookDto()
        //    {
        //        Id = book.Id,
        //        Title = book.Title,
        //        AuthorName = book.Author.Name
        //    };

        //    return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        //}
    }
}
