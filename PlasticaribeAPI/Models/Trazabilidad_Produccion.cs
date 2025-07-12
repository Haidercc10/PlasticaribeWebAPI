using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Trazabilidad_Produccion
    {
        [Key]
        public int Trz_Id { get; set; }

        // Info Actual
        public long Trz_Etiqueta { get; set; }

        public int Trz_Ot { get; set; }
        
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        public long Cli_Id { get; set; }
        public Clientes? Clientes { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Procesos { get; set; }


        [Column(TypeName = "date")]
        public DateTime Trz_Fecha { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Trz_Hora { get; set; }


        [Precision(18,2)]
        public decimal Trz_PesoNeto { get; set; }

        [Precision(18, 2)]
        public decimal Trz_PesoBruto { get; set; }

        [Precision(18, 2)]
        public decimal Trz_Cantidad { get; set; }

        public string Presentacion { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }

        public int Trz_Maquina { get; set; }

        public long Operario_1 { get; set; }
        public Usuario? Usuario1 { get; set; }

        public long? Operario_2 { get; set; }
        public Usuario? Usuario2 { get; set; }

        public long? Operario_3 { get; set; }
        public Usuario? Usuario3 { get; set; }

        public long? Operario_4 { get; set; }
        public Usuario? Usuario4 { get; set; }

        public long? Empacador_Id { get; set; }
        public Usuario? Empacador { get; set; }

        public string? Turno_Id { get; set; }
        public Turno? Turno { get; set; }

        // Anterior
        public long? Trz_EtiquetaAnterior { get; set; }

        public long? Trz_OtAnterior { get; set; }

        public int? Prod_Anterior { get; set; }
        public Producto? ProductoAnt { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string? Proceso_Anterior { get; set; }
        public Proceso? ProcesoAnt { get; set; }

        public long? Autoriza_Id { get; set; }
        public Usuario? Autoriza { get; set; }

    }
}
