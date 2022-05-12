using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Empresa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Empresa_Codigo { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "bigint")]
        public long Empresa_Id { get; set; }
        public string TipoIdentificacion_Id { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Empresa_Nombre { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Empresa_Direccion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Empresa_Ciudad { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String Empresa_Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String Empresa_Correo { get; set; }
        public int Empresa_COdigoPostal { get; set; }
    }
}

