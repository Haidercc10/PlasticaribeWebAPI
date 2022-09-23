using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class PreEntrega_RolloDespacho
    {
        [Key]
        public long PreEntRollo_Id { get; set; }

        public long PreEntRollo_OT { get; set; }


        [Column(TypeName = "date")]
        public DateTime PreEntRollo_Fecha { get; set; }


        public long Cli_Id { get; set; }
        public Clientes? Cliente { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Prod { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string UndMed_Id { get; set; }
        public Unidad_Medida? UndMedida { get; set; }


        [Column(TypeName = "text")]
        public string? PreEntRollo_Observacion { get; set; }


        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
