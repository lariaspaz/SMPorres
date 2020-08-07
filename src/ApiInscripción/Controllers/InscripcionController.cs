using ApiInscripción.Filters.Jwt;
using ApiInscripción.Models.ViewModels;
using ApiInscripción.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiInscripción.Controllers
{
    public class InscripcionController : ApiController
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [JwtAuthentication]
        public IHttpActionResult PostAlumno(Alumno alumno)
        {
            if (!ModelState.IsValid || alumno == null)
            {
                return BadRequest("Los datos del alumno son incorrectos.");
            }

            _log.Debug("Insertando el alumno: \n" + JsonConvert.SerializeObject(alumno));
            try
            {
                AlumnosRepository.Insertar(alumno.Nombre, alumno.Apellido, alumno.IdTipoDocumento,
                    alumno.NroDocumento, alumno.FechaNacimiento, alumno.EMail, alumno.Direccion,
                    alumno.Sexo, alumno.IdCarrera);
                return Ok("Los datos se grabaron correctamente.");
            }
            catch (ValidationException ex)
            {
                _log.Error(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (HttpException ex)
            {
                _log.Error(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return BadRequest("Se produjo un error al intentar grabar los datos.");
            }
        }
    }
}
