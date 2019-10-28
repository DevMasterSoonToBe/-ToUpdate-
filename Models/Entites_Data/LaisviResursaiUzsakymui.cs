using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlausDaryklosGamybosValdymoSistema.Models
{
  public partial class ResursaiUzsakymui
  {
    public decimal KlientoUzsakymoId { get; set; }
    public decimal FormosId { get; set; }
    
    public string LaisvuResursuTipas { get; set; }

    public LaisviResursai ResursaiLaisvi { get; set; }
    public KlientoUzsakymoAplankas UzsakymasKliento { get; set; }

  }
}
