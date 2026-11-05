using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Imagen adjunta a un ticket. El archivo se guarda con un nombre GUID bajo la carpeta configurada en "Tickets:RutaAdjuntos".
    /// </summary>
    public class Tickets_Adjuntos
    {
        /// <summary>Id del adjunto.</summary>
        [Key]
        public long Adj_Id { get; set; }

        /// <summary>Ticket al que pertenece.</summary>
        public long Ticket_Id { get; set; }
        /// <summary>Navegación al ticket.</summary>
        public Tickets? Ticket { get; set; }

        /// <summary>Nombre con el que el usuario subió el archivo (solo informativo).</summary>
        [Column(TypeName = "varchar(255)")]
        public string Adj_NombreOriginal { get; set; } = string.Empty;

        /// <summary>Ruta relativa a la carpeta de adjuntos, p. ej. "2026/11/{guid}.png".</summary>
        [Column(TypeName = "varchar(300)")]
        public string Adj_RutaRelativa { get; set; } = string.Empty;

        /// <summary>image/png o image/jpeg.</summary>
        [Column(TypeName = "varchar(50)")]
        public string Adj_TipoContenido { get; set; } = string.Empty;

        /// <summary>Tamaño del archivo en bytes.</summary>
        public long Adj_TamanoBytes { get; set; }

        /// <summary>Fecha y hora de carga.</summary>
        [Column(TypeName = "datetime2")]
        public DateTime Adj_Fecha { get; set; }

        /// <summary>Usuario que subió el archivo.</summary>
        public long Usua_Id { get; set; }
        /// <summary>Navegación al usuario.</summary>
        public Usuario? Usuario { get; set; }
    }
}
