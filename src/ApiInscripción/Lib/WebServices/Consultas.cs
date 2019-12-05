using ApiInscripción.ConsultasWeb;
using ApiInscripción.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ApiInscripción.Lib.WebServices
{
    public class Consultas
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool SubirAlumno(Alumno alumno, double interesPorMora)
        {
            SMPSoapClient cliente = CrearCliente();
            bool result = cliente.ActualizarDatos(alumno);
            if (result)
            {
                cliente.ActualizarConfiguracion(interesPorMora);
            }
            else
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(ConsultasWeb.Alumno));
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, alumno);                        
                        _log.Error(sww);
                    }
                }                
            }
            return result;
        }

        private static SMPSoapClient CrearCliente()
        {
            //Specify the binding to be used for the client.
            BasicHttpBinding binding = new BasicHttpBinding();

            //Specify the address to be used for the client.
            EndpointAddress address =
               new EndpointAddress(Repositories.ConfiguracionRepository.ObtenerConfiguracion().EndpointAddress);

            return new SMPSoapClient(binding, address);
        }
    }
}