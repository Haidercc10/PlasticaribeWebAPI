using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class OT_Extrusion
    {
        [Key]
        public int Extrusion_Id { get; set; }
        public long Ot_Id { get; set; }
        public Orden_Trabajo? Orden_Trabajo { get; set; }
        public int MatPri_Id { get; set; }
        public Materia_Prima? Materia_Prima { get; set; }
        public long Formato_Id { get; set; }
        public Formato? Formato { get; set; }
        public int Pigmt_Id { get; set; }
        public Pigmento? Pigmento { get; set; }

        [Precision(14, 2)]
        public int Extrusion_Calibre { get; set; }

        [Precision(14, 2)]
        public int Extrusion_Ancho1 { get; set; }

        [Precision(14, 2)]
        public int Extrusion_Ancho2 { get; set; }

        [Precision(14, 2)]
        public int Extrusion_Ancho3 { get; set; }

        [Precision(14, 2)]
        public int Tratado_Id { get; set; }
        public Tratado? Tratado { get; set; }
    }
}
