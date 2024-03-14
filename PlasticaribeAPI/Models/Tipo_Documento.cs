﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Tipo_Documento
    {

        /*Llave Primaria en string para darle unas siglas Ej: Facco = Factura de Compra */
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string TpDoc_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string TpDoc_Nombre { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string TpDoc_Descripcion { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
