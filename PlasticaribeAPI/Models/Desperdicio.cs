using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Desperdicio
    {
        [Key]
        public long Desp_Id { get; set; }


        public long Desp_OT { get; set; }


        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }


        public int Material_Id { get; set; }
        public Material_MatPrima? Material { get; set; }


        public long Maquina { get; set; }
        //public Activo? Activo { get; set; }


        public long Usua_Operario { get; set; }
        public Usuario? Usuario1 { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Desp_Impresion { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }


        public int Falla_Id { get; set; }
        public Falla_Tecnica? Falla { get; set; }


        [Precision(18, 2)]
        public decimal Desp_PesoKg { get; set; }


        [Column(TypeName = "date")]
        public DateTime Desp_Fecha { get; set; }



        [Column(TypeName = "varchar(max)")]
        public string? Desp_Observacion { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usuario2 { get; set; }


        [Column(TypeName = "date")]
        public DateTime? Desp_FechaRegistro { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Desp_HoraRegistro { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Turno_Id { get; set; }
        public Turno? Turnos { get; set; }

    }
}
