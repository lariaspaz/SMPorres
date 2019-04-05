using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consultas.Models.ViewModels
{
    public class ChangePwd
    {
        [Required(ErrorMessage = "No puede estar vacío.")]
        [Display(Name = "Contraseña actual")]
        public string ContraseñaActual { get; set; }

        [Required(ErrorMessage = "No puede estar vacío.")]
        [Display(Name = "Nueva contraseña")]
        public string ContraseñaNueva { get; set; }

        [Required(ErrorMessage = "No puede estar vacío.")]
        [Display(Name = "Ingrese nuevamente la nueva contraseña")]
        public string ContraseñaNuevaRepetida { get; set; }
    }
}