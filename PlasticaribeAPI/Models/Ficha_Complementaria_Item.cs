using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Datos complementarios del item (producto) que no existen en BagPro y se capturan en "Crear Item"
    /// (reunión del 21-nov). Una fila por producto; la Orden de Trabajo los lee por Prod_Id.
    /// </summary>
    public class Ficha_Complementaria_Item
    {
        /// <summary>Código del item (Prod_Id de Productos). PK y FK 1 a 1 con Productos.</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prod_Id { get; set; }
        public Producto? Producto { get; set; }

        /// <summary>Tipo de desembobinado final, 1..16 según el cuadro "Tipos de embobinado" (1-8 exterior, 9-16 interior). Sin tabla catálogo.</summary>
        public int? Embobinado_Id { get; set; }

        /// <summary>Código de barras (solo dígitos). Se guarda como texto para conservar los ceros a la izquierda.</summary>
        [Column(TypeName = "varchar(20)")]
        public string? CodigoBarras { get; set; }

        /// <summary>Repeticiones (texto libre: números y letras).</summary>
        [Column(TypeName = "varchar(50)")]
        public string? Repeticiones { get; set; }

        /// <summary>Paso entre guías (texto libre).</summary>
        [Column(TypeName = "varchar(50)")]
        public string? PasoGuias { get; set; }

        /// <summary>Ubicación de la guía.</summary>
        [Column(TypeName = "varchar(100)")]
        public string? UbicacionGuia { get; set; }

        /// <summary>Indica si la lámina es de largo continuo.</summary>
        public bool LargoContinuo { get; set; }

        /// <summary>
        /// true si la mezcla del item en BagPro se ajustó en "Crear Item": la OT y su PDF deben mostrar la composición
        /// del item en vez de la mezcla predefinida.
        /// </summary>
        public bool MezclaAjustada { get; set; }

        /// <summary>Usuario que creó la ficha.</summary>
        public long Usua_Id { get; set; }
        public Usuario? Usuario { get; set; }

        /// <summary>Fecha y hora de creación de la ficha.</summary>
        public DateTime Fecha_Creacion { get; set; }

        /// <summary>Usuario de la última modificación; null si nunca se ha modificado.</summary>
        public long? Usua_Modifica { get; set; }
        public Usuario? UsuarioModifica { get; set; }

        /// <summary>Fecha y hora de la última modificación; null si nunca se ha modificado.</summary>
        public DateTime? Fecha_Modifica { get; set; }
    }
}
