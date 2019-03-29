using Consultas.Models;
using Consultas.Models.WebServices;
using Consultas.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Consultas.Web_Services
{
    /// <summary>
    /// Descripción breve de SMP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SMP : System.Web.Services.WebService
    {

        [WebMethod]
        public bool ActualizarDatos(Alumno alumno)
        {
            try
            {
                new AlumnosRepository().Actualizar(alumno);
                return true;
            }
            catch (Exception ex)
            {
                //var s = DateTime.Now.ToString() + " - " + ex.ToString() + Environment.NewLine + 
                //        "Datos ========================" + Environment.NewLine + 
                //        JsonConvert.SerializeObject(alumno, Formatting.Indented);
                //System.IO.File.AppendAllText(Server.MapPath("~") + @"\datos.txt", s);
                return false;
                throw;
            }
        }

        [WebMethod]
        public bool ActualizarPwd(int idAlumno, string pwd)
        {
            try
            {
                return new AlumnosRepository().ActualizarContraseña(idAlumno, pwd);
            }
            catch (Exception ex)
            {
                //var s = DateTime.Now.ToString() + " - " + ex.ToString() + Environment.NewLine + 
                //        "Datos ========================" + Environment.NewLine + 
                //        JsonConvert.SerializeObject(alumno, Formatting.Indented);
                //System.IO.File.AppendAllText(Server.MapPath("~") + @"\datos.txt", s);
                return false;
                throw;
            }
        }
    }
}
