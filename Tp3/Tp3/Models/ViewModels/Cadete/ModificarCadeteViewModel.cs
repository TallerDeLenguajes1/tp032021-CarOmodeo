using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class ModificarCadeteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su {0}")]
        [Display(Name = "Domicilio")]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        public ModificarCadeteViewModel()
        {

        }

    }
}
