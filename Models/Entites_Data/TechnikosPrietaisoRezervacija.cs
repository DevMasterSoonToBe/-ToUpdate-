using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AlausDaryklosGamybosValdymoSistema.Models
{
  public partial class TechnikosPrietaisoRezervacija
  {
    public decimal KlientoUzsakymoId { get; set; }
    public decimal TechnikosPrietaisoId { get; set; }
    [Required]
    public int PrietaisoKiekis { get; set; }


    public KlientoUzsakymoAplankas KlientoUzsakymo { get; set; }
    public TechnikosPrietaisas TechnikosPrietaiso { get; set; }
  }
}
