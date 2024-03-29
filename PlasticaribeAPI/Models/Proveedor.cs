﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Proveedor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Prov_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Prov_Id { get; set; }
        public string TipoIdentificacion_Id { get; set; } //Llave foranea de TipoID
        public TipoIdentificacion? TipoIdentificacion { get; set; } //Propiedad de navegación de TipoID


        [Column(TypeName = "varchar(MAX)")]
        public string Prov_Nombre { get; set; }
        public int TpProv_Id { get; set; } //Llave foranea de Tipo proveedor
        public Tipo_Proveedor? TpProv { get; set; }//Propiedad de navegación de Tipo proveedor

        [Column(TypeName = "varchar(100)")]
        public string? Prov_Ciudad { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Prov_Telefono { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string? Prov_Email { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Prov_Fecha { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Prov_Hora { get; set; }
        public int ReteFuente { get; set; }
        public Conceptos_Automaticos? CA_ReteFuente { get; set; }
        public int ReteIVA { get; set; }
        public Conceptos_Automaticos? CA_ReteIVA { get; set; }
        public int ReteICA { get; set; }
        public Conceptos_Automaticos? CA_ReteICA { get; set; }

        //Lista requerida para relación proveedor-materiaprima
        //public IList<Provedor_MateriaPrima>? ProvMatPri { get; set; } 
    }
}
