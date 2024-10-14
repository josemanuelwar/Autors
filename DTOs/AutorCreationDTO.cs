using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiResFull.DTOs
{
    public class AutorCreationDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:120  ,ErrorMessage = "El campo {0} no debe tener mas de 4 carateres")]
        [validation.PrimeraLetraMayuscula]
        public string nombres { get; set; }
    }
}