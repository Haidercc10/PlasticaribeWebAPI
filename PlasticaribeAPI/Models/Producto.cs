﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Producto
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prod_Cod { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prod_Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Prod_Nombre { get; set; }

        [Column(TypeName = "text")]
        public string? Prod_Descripcion { get; set; }

        //Llave tipos productos agregada. 
        public int TpProd_Id { get; set; }
        public Tipo_Producto? TpProd { get; set; } //Foranea tipos productos
        
        [Precision(14, 2)]
        public decimal Prod_Peso_Bruto { get; set; }

        [Precision(14, 2)]
        public decimal Prod_Peso_Neto { get; set; }

        public string UndMedPeso { get; set; }
        public Unidad_Medida? UndMed1 { get; set; } //Foranea unidades medidas

        [Precision(14, 2)]
        public decimal? Prod_Fuelle { get; set; }

        [Precision(14, 2)]
        public decimal? Prod_Ancho { get; set; }

        [Precision(14, 2)]
        public decimal? Prod_Calibre { get; set; }

        public string UndMedACF { get; set; }
        
        public Unidad_Medida? UndMed2 { get; set; } //Foranea unidades medidas

        public int? Estado_Id { get; set; }
        public Estado? Estado { get; set; } //Foranea de estados en producto.

        public IList<Pedido_Producto>? PedExtProd { get; set; }
    }
}