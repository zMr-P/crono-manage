using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CronoManage.Domain.Validations
{
    public class MyProjectValidation
    {
        [Required]
        [MaxLength(30, ErrorMessage = "A quantidade Máxima de caracteres é {1}")]
        public string Name {  get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Aquantidade máxima de caracteres é {1}")]
        public string Description { get; set; }
    }
}
