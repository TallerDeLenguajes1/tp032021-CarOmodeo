using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class AltaUsuarioViewModel
    {
        
        [Required(ErrorMessage = "Ingrese su nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una {0}")]
        [StringLength(8)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione el Rol")]
        public string Rol { get; set; }

        public AltaUsuarioViewModel()
        {

        }
    }
}
