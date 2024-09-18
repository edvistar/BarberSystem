using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength =1, ErrorMessage ="El nombre debe ser minimo 1 Maximo 60 caracteres")]
        public string NameEspeciality { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripcion debe ser minimo 1 Maximo 100 caracteres")]
        public string Description { get; set; }

        public bool Status { get; set; }
    }
}
