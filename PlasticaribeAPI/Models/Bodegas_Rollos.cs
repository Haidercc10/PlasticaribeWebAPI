﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Bodegas_Rollos
    {
        [Key]
        public long BgRollo_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaEntrada { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraEntrada { get; set; }

        [Column(TypeName = "date")]
        public DateTime BgRollo_FechaModifica { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string BgRollo_HoraModifica { get; set; }
        public string BgRollo_Observacion { get; set; }
    }
}
