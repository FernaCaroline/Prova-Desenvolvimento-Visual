using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal; 

namespace Fernanda.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public double m3Consumidos { get; set; }
        public string bandeira { get; set; }
        public bool possuiEsgoto { get; set; }
        public double consumoFaturado { get; set; }
        public double tarifa { get; set; }
        public double valorAgua { get; set; }
        public double adicinionalBandeira { get; set; }
        public double taxaEsgoto { get; set; }
        public double total { get; set; }

    }
}