using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiResFull.validation;

namespace ApiResFull.DTOs
{
    public class LibroCreationDTO
    {

        [Required]
        [StringLength(maximumLength:200  ,ErrorMessage = "El campo {0} no debe tener mas de 4 carateres")]
        [PrimeraLetraMayuscula]
        public string titulo  { get; set; }
    }
}