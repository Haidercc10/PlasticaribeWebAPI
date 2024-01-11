using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Produccion_Procesos
    {
        [Key]
        public int Id { get; set; }
        public long Numero_Rollo { get; set; }
        public long OT { get; set; }
        //public Orden_Trabajo? Orden_Trabajo { get; set; }
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }
        public long Cli_Id { get; set; }
        public Clientes? Clientes { get; set; }
        public long? Operario1_Id { get; set; }
        public Usuario? Operario1 { get; set; }
        public long? Operario2_Id { get; set; }
        public Usuario? Operario2 { get; set; }
        public long? Operario3_Id { get; set; }
        public Usuario? Operario3 { get; set; }
        public long? Operario4_Id { get; set; }
        public Usuario? Operario4 { get; set; }
        public int Pesado_Entre { get; set; }
        public int Maquina { get; set; }
        public string Cono_Id { get; set; }
        public Cono? Cono { get; set; }

        [Precision(18, 2)]
        public decimal Ancho_Cono { get; set; }

        [Precision(18, 2)]
        public decimal Tara_Cono { get; set; }

        [Precision(18, 2)]
        public decimal Peso_Bruto { get; set; }

        [Precision(18, 2)]
        public decimal Peso_Neto { get; set; }

        [Precision(18, 2)]
        public decimal Cantidad { get; set; }

        [Precision(18, 2)]
        public decimal Peso_Teorico { get; set; }

        [Precision(18, 2)]
        public decimal Desviacion { get; set; }

        [Precision(18, 2)]
        public decimal Precio { get; set; }

        [Precision(18, 2)]
        public decimal PrecioVenta_Producto { get; set; }
        public string Presentacion { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }
        public string Proceso_Id { get; set; }
        public Proceso? Proceso { get; set; }
        public string Turno_Id {  get; set; }
        public Turno? Turno { get; set; }
        public bool Envio_Zeus { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Datos_Etiqueta { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Hora { get; set; }
        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }
        public int Estado_Rollo { get; set; }
        public Estado? Estado { get; set; }
        public long NumeroRollo_BagPro { get; set; }
    }
}
