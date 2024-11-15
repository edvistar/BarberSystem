﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [Display(Name = "Nombre del producto")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Numero de Serie es Requerido")]
        [MaxLength(30)]
        [Display(Name = "Numero de Serie")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Descripción es Requerida")]
        [MaxLength(100)]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es requerido")]
        [Display(Name = "Estado")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Si tiene oferta es Requerido")]
        [Display(Name = "Esta en oferta")]
        public bool Offer { get; set; }

        [Required(ErrorMessage = "El precio es Requerido")]
        [Range(1, 100000000)]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required(ErrorMessage = "El Costo es Requerido")]
        [Range(1, 100000000)]
        [Display(Name = "Costo")]
        public double Cost { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        //Foreing Keys
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Category Categoria { get; set; }

        [Required]
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }
    }
}
