﻿using Consultas.Models;
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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod]
        public bool ActualizarDatos(Alumno alumno)
        {
            try
            {
                _log.Info("Datos: " + JsonConvert.SerializeObject(alumno, Formatting.Indented));
                new AlumnosRepository().Actualizar(alumno);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
                throw ex;
            }
        }

        [WebMethod]
        public bool ActualizarConfiguracion(double interésPorMora)
        {
            try
            {
                new ConfiguracionRepository().Actualizar(interésPorMora);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _log.Info("interésPorMora = " + interésPorMora);
                return false;
                throw ex;
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
                //        JsonConvert.SerializeObject(new { IdAlumno = idAlumno, Pwd = pwd }, Formatting.Indented);
                //System.IO.File.AppendAllText(Server.MapPath("~") + @"\datos.txt", s);
                _log.Error(ex);
                _log.Info("Datos = " + JsonConvert.SerializeObject(new { IdAlumno = idAlumno, Pwd = pwd }, Formatting.Indented));
                return false;
                throw ex;
            }
        }

        [WebMethod]
        public bool TestAlive()
        {
            return true;
        }

        [WebMethod]
        public bool ActualizarTasasMora(List<TasaMora> tasasMora)
        {
            try
            {
                new TasasMoraRepository().Actualizar(tasasMora);
                return true;
            }
            catch (Exception ex)
            {
                //var s = DateTime.Now.ToString() + " - " + ex.ToString() + Environment.NewLine +
                //        "Datos ========================" + Environment.NewLine +
                //        JsonConvert.SerializeObject(tasasMora, Formatting.Indented);
                //System.IO.File.AppendAllText(Server.MapPath("~") + @"\datos.txt", s);
                _log.Error(ex);
                _log.Info("Datos = " + JsonConvert.SerializeObject(tasasMora, Formatting.Indented));
                return false;
                throw ex;
            }
       }
    }
}
