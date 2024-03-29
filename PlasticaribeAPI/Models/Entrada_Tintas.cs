﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Entrada_Tintas
    {
        [Key]
        public int EntTinta_Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime entTinta_FechaEntrada { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? entTinta_Hora { get; set; }

        public long Usua_Id { get; set; }
        public Usuario? Usua { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? EntTinta_Observacion { get; set; }
    }
}
