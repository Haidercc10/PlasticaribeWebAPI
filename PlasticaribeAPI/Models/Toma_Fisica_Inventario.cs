using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Toma_Fisica_Inventario
    {
        [Key]
        public int Tfi_Id { get; set; }

        public long Tfi_NumeroRollo { get; set; }

        public long Tfi_Etiqueta { get; set; }

        public long Tfi_OT { get; set; }
        
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Clientes { get; set; }

        [Precision(18, 2)]
        public decimal Tfi_CantidadReal { get; set; }

        [Precision(18, 2)]
        public decimal Tfi_PesoBruto { get; set; }

        public string Presentacion { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }

        public int Estado_Rollo { get; set; }
        public Estado? Estado { get; set; }


        [Precision(18, 2)]
        public decimal Tfi_PrecioVenta { get; set; }

        public bool Tfi_EnvioZeus { get; set; }


        [Column(TypeName = "date")]
        public DateTime Tfi_Fecha { get; set; }


        [Column(TypeName = "varchar(20)")]
        public string Tfi_Hora { get; set; }


        public long UsuaRegistro_Id { get; set; }
        public Usuario? Registra { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Tfi_Ubicacion { get; set; }

        public int TpBod_Id { get; set; }
        public Tipo_Bodega? Tipo_Bodega { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Tipo_Inventario { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string? Tfi_Observacion { get; set; }
    }
}
