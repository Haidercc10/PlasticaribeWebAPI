using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Models
{
    public class Detalles_SolicitudMateriaPrima
    {
        [Key]
        public long DtSolicitud_Id { get; set; }
        public Solicitud_MateriaPrima? Solicitud_MateriaPrima { get; set; }
        public long Solicitud_Id { get; set; }
        public Materia_Prima? Materia_Prima { get; set; }
        public long MatPri_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long Tinta_Id { get; set; }
        public Bopp_Generico? Bopp { get; set; }
        public long Bopp_Id { get; set; }

        [Precision(18, 2)]
        public decimal DtSolicitud_Cantidad { get; set; }
        public Unidad_Medida? UndMed { get; set; }
        public string UndMed_Id { get; set; }
        public Estado? Estado { get; set; }
        public int Estado_Id { get; set; }
    }
}
