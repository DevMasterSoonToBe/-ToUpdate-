using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class Ingredientas
    {
        public Ingredientas()
        {
            IngredientoPrasymas = new HashSet<IngredientoPrasymas>();
      IngredientoRezervacija = new HashSet<IngredientoRezervacija>();
      IngredientoReceptas = new HashSet<IngredientoReceptas>();
        }

        public decimal IngredientoId { get; set; }
        public decimal? FormosId { get; set; }


        public string IngredientoPavadinimas { get; set; }

    public double? LaisvasKiekis { get; set; }

        public LaisviResursai Formos { get; set; }
        public ICollection<IngredientoPrasymas> IngredientoPrasymas { get; set; }
    public ICollection<IngredientoRezervacija> IngredientoRezervacija { get; set; }
    public ICollection<IngredientoReceptas> IngredientoReceptas { get; set; }
    }
}
