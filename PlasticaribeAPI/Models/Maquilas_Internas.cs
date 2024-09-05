using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Maquilas_Internas
    {
        
        [Key]
        public int MaqInt_Id { get; set; }

        public int MaqInt_OT { get; set; } //Requerido

        public int Prod_Id { get; set; } 
        public Producto? Producto { get; set; }

        public string Cono_Id { get; set; } //Requerido
        public Cono? Cono { get; set; }


        [Precision(18, 2)]
        public decimal Ancho_Cono { get; set; }

        [Precision(18, 2)]
        public decimal Tara_Cono { get; set; }

        [Precision(18,2)]
        public decimal Peso_Bruto { get; set; }


        [Precision(18, 2)]
        public decimal Peso_Neto { get; set; }


        [Precision(18, 2)]
        public decimal Cantidad { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Presentacion { get; set; }
        public Unidad_Medida? UndMedida { get; set; } 


        [Column(TypeName = "varchar(10)")]
        public string? MaqInt_Medida { get; set; } //Opcional


        public int Maquina { get; set; } //Requerido


        public long Operario_Id { get; set; } //Requerido
        public Usuario? Operario { get; set; }


        [Column(TypeName = "date")]
        public DateTime MaqInt_Fecha { get; set; } //Requerido


        [Column(TypeName = "varchar(10)")]
        public string Proceso_Id { get; set; }
        public Proceso? Procesos { get; set; }


        public int Estado_Id { get; set; }
        public Estado? Estados { get; set; }


        public int SvcProd_Id { get; set; }
        public Servicios_Produccion? Servicio_Produccion { get; set; } //Requerido


        [Column(TypeName = "date")]
        public DateTime MaqInt_FechaRegistro { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string MaqInt_HoraRegistro { get; set; }
        

        public long Creador_Id { get; set; }
        public Usuario? Creador { get; set; }

        public int MaqInt_Codigo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? MaqInt_Observacion { get; set; }

    }
}
