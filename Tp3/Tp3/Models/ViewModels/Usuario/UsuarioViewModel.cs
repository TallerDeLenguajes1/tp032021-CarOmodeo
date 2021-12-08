using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }

        public UsuarioViewModel()
        {
            
        }
    }
}
