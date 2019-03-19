using Consultas.Models;
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
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public bool ActualizarDatos(Consultas.Models.WebServices.Alumno alumno)
        {
            var s = JsonConvert.SerializeObject(alumno, Formatting.Indented);
            System.IO.File.AppendAllText(Server.MapPath("~") + @"\datos.txt", s);
            return true;
        }
    }
}
