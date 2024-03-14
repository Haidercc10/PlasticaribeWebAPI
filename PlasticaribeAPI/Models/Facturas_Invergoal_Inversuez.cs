using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Facturas_Invergoal_Inversuez
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Registro { get; set; }
        public string Hora_Registro { get; set; }
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }
        public string Nit_Empresa { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Codigo_Factura { get; set; }
        public long Nit_Proveedor { get; set; }
        public Proveedor? Proveedor { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Factura { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Vencimiento { get; set; }

        [Precision(14, 2)]
        public decimal Valor_Factura { get; set; }
        public string Cuenta { get; set; }
        public int Estado_Factura { get; set; }
        public Estado? Estados { get; set; }
        public string Observacion { get; set; }
        public bool Restar_DashboardCostos { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
