﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Tipos_Sellados
    {
        [Key]
        public int TpSellado_Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string TpSellados_Nombre { get; set; }
        [Column(TypeName = "text")]
        public string TpSellado_Descripcion{ get; set; }
    }
}