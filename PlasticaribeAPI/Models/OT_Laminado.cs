using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OT_Laminado
    {
        [Key]
        public int LamCapa_Id { get; set; }
        public long OT_Id { get; set; }
        public Orden_Trabajo? Orden_Trabajo { get; set; }
        public int Capa_Id1 { get; set; }
        public Laminado_Capa? Laminado_Capa { get; set; }
        public int Capa_Id2 { get; set; }
        public Laminado_Capa? Laminado_Capa2 { get; set; }
        public int Capa_Id3 { get; set; }
        public Laminado_Capa? Laminado_Capa3 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Calibre1 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Calibre2 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Calibre3 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Cantidad1 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Cantidad2 { get; set; }

        [Precision(14, 2)]
        public double LamCapa_Cantidad3 { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
