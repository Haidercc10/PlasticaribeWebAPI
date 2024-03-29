﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Proceso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Proceso_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Proceso_Nombre { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Proceso_Descripcion { get; set; }
    }
}
