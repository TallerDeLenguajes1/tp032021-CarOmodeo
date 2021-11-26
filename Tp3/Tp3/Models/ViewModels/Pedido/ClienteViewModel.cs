using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class ClienteViewModel
    {
        [Required(ErrorMessage = "DNI requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una Dirección")]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese un Teléfono")]
        [StringLength(100)]
        public string Telefono { get; set; }

        public ClienteViewModel()
        {

        }
    }
}
