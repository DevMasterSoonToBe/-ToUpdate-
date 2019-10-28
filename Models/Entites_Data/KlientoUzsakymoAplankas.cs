using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public partial class KlientoUzsakymoAplankas
    {
        public KlientoUzsakymoAplankas()
        {
            IngredientoPrasymas = new HashSet<IngredientoPrasymas>();
      IngredientoRezervacija = new HashSet<IngredientoRezervacija>();
      LaisviResursaiUzsakymui = new HashSet<ResursaiUzsakymui>();
      TechnikosPrietaisoPrasymas = new HashSet<TechnikosPrietaisoPrasymas>();
      TechnikosPrietaisoRezervacija = new HashSet<TechnikosPrietaisoRezervacija>();
    }

        public decimal KlientoUzsakymoId { get; set; }
    [Required]
    public decimal KlientoId { get; set; }
    [DataType(DataType.Date, ErrorMessage = "Įveskite tinkamą datą!")]
           public DateTime Terminas { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string Busena { get; set; }
    public string Archyvavimo_Komentaras { get; set;}
    public DateTime? ArchyvavimoLaikas { get; set; }

    public string PrasymasZaliavomsReikalingas{ get; set; }

    public string PrasymasTechnikaiReikalingas{ get; set; }



    public Klientas Kliento { get; set; }
        public GamybosInstrukcija GamybosInstrukcija { get; set; }
        public ICollection<IngredientoPrasymas> IngredientoPrasymas { get; set; }
    public ICollection<IngredientoRezervacija> IngredientoRezervacija { get; set; }
    public ICollection<ResursaiUzsakymui> LaisviResursaiUzsakymui { get; set; }
    public Sutartis Sutartis { get; set; }
        public ICollection<TechnikosPrietaisoPrasymas> TechnikosPrietaisoPrasymas { get; set; }
    public ICollection<TechnikosPrietaisoRezervacija> TechnikosPrietaisoRezervacija { get; set; }
  }
}
