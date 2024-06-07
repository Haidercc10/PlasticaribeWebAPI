using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Ingreso_Peletizado
    {
        [Key]
        public long IngPel_Id { get; set; }

        public long? Rollo_Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string TpRecu_Id { get; set; }
        public Tipo_Recuperado? Tipo_Recuperado { get; set; }

        public long? OT { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto {  get; set; }

        public long MatPri_Id { get; set; }
        public Materia_Prima? MatPrima { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; } 

        public int Material_Id { get; set; }
        public Material_MatPrima? Materiales { get; set; }

        public int Falla_Id { get; set; }
        public Falla_Tecnica? Fallas { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }


        public bool IngPel_Area1 { get; set; }

        public bool IngPel_Area2 { get; set; }

        [Precision(18,2)]
        public decimal IngPel_Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string IngPel_Observacion { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usuario {  get; set; }


        [Column(TypeName = "Date")]
        public DateTime? IngPel_FechaIngreso { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? IngPel_HoraIngreso { get; set; }


        public long? Usua_Modifica { get; set; }
        public Usuario? Usuario2 { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? IngPel_FechaModifica { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? IngPel_HoraModifica { get; set; }

    }
}
