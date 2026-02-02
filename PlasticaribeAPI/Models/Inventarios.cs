using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Inventarios
    {
        [Key]
        public int Inv_Id { get; set; }

        public int InvSnap_Id { get; set; }
        public Inventarios_Snapshot? Inventarios_Snapshot { get; set; }

        public long Inv_NumeroRollo { get; set; }

        public long Inv_Etiqueta { get; set; }

        public long Inv_OT { get; set; }

        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Clientes { get; set; }

        [Precision(18, 2)]
        public decimal Inv_Existencias { get; set; }

        [Precision(18, 2)]
        public decimal Inv_Cantidad { get; set; }

        [Precision(18, 2)]
        public decimal Inv_PesoBruto { get; set; }

        public string Presentacion { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        [Precision(18, 2)]
        public decimal Inv_PrecioVenta { get; set; }


        [Column(TypeName = "date")]
        public DateTime Inv_Fecha { get; set; }


        [Column(TypeName = "varchar(20)")]
        public string Inv_Hora { get; set; }


        public long UsuaRegistro_Id { get; set; }
        public Usuario? Registra { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Inv_Ubicacion { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Inv_Observacion { get; set; }
    }
}
