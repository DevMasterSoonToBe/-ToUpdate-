using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class IngredientoPrasymas
    {
        public decimal KlientoUzsakymoId { get; set; }
        public decimal IngredientoId { get; set; }
          [Required]
          public double IngredientoKiekis { get; set; }
          
          public string Busena { get; set; }

        public Ingredientas Ingrediento { get; set; }
        public KlientoUzsakymoAplankas KlientoUzsakymo { get; set; }

        //public int i { get; set; }

    }
}
