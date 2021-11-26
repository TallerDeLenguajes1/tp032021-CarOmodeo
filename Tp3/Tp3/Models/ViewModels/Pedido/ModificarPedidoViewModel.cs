using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class ModificarPedidoViewModel
    {
        public int Nro { get; set; }

        [Required(ErrorMessage = "Descripción Requerida")]
        [StringLength(300)]
        [Display(Name = "Observaciones")]
        public string Obs { get; set; }

        [Required]
        public ClienteViewModel Cliente { get; set; }

        public ModificarPedidoViewModel()
        {

        }
    }
}
