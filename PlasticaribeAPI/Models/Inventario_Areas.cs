using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Inventario_Areas
    {
        [Key]
        public long InvCodigo { get; set; }
        public long OT { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Item { get; set; }
        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }

        [Precision(18, 2)]
        public decimal InvStock { get; set; }

        [Precision(18, 2)]
        public decimal InvPrecio { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvFecha_Inventario { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvFecha_Registro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string InvHora_Registro { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? InvObservacion { get; set; }
    }
}
