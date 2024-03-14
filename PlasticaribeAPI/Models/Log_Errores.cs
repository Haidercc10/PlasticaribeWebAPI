﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Log_Errores
    {
        [Key]
        public int Id { get; set; }
        public string? Base_Datos { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string? ErrorNumber { get; set; }
        public string? ErrorState { get; set; }
        public string? ErrorSeverity { get; set; }
        public string? ErrorProcedure { get; set; }
        public string? ErrorLine { get; set; }
        public string? ErrorMessage { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
