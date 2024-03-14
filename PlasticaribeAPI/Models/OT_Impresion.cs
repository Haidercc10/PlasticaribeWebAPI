using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OT_Impresion
    {
        [Key]
        public int Impresion_Id { get; set; }
        public long Ot_Id { get; set; }
        public Orden_Trabajo? Orden_Trabajo { get; set; }
        public int TpImpresion_Id { get; set; }
        public Tipos_Impresion? Tipos_Impresion { get; set; }

        [Column(TypeName = "int")]
        public int Rodillo_Id { get; set; }
        //public Rodillos? Rodillos { get; set; }

        [Column(TypeName = "int")]
        public int Pista_Id { get; set; }
        //public Pistas? Pistas { get; set; }
        public long Tinta1_Id { get; set; }
        public Tinta? Tinta1 { get; set; }
        public long Tinta2_Id { get; set; }
        public Tinta? Tinta2 { get; set; }
        public long Tinta3_Id { get; set; }
        public Tinta? Tinta3 { get; set; }
        public long Tinta4_Id { get; set; }
        public Tinta? Tinta4 { get; set; }
        public long Tinta5_Id { get; set; }
        public Tinta? Tinta5 { get; set; }
        public long Tinta6_Id { get; set; }
        public Tinta? Tinta6 { get; set; }
        public long Tinta7_Id { get; set; }
        public Tinta? Tinta7 { get; set; }
        public long Tinta8_Id { get; set; }
        public Tinta? Tinta8 { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
