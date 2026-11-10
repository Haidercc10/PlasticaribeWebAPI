using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Historial de nombres (referencia) de un producto: una fila por cada cambio de Productos.Prod_Nombre hecho al crear
    /// o editar una OT. El maestro (Productos) y ClientesOT_Item de BagPro conservan siempre el último nombre.
    /// </summary>
    public class Producto_HistorialNombre
    {
        /// <summary>Identificador del registro.</summary>
        [Key]
        public long ProdHN_Id { get; set; }

        /// <summary>Producto (item) renombrado.</summary>
        public int Prod_Id { get; set; }
        /// <summary>Producto (navegación).</summary>
        public Producto? Prod { get; set; }

        /// <summary>Nombre que tenía el producto antes del cambio.</summary>
        [Column(TypeName = "varchar(100)")]
        public string ProdHN_NombreAnterior { get; set; } = "";

        /// <summary>Nombre que quedó en el maestro con el cambio.</summary>
        [Column(TypeName = "varchar(100)")]
        public string ProdHN_NombreNuevo { get; set; } = "";

        /// <summary>OT en la que se cambió el nombre.</summary>
        public long Ot_Id { get; set; }
        /// <summary>OT (navegación).</summary>
        public Orden_Trabajo? Ot { get; set; }

        /// <summary>"Creación" o "Edición" de la OT.</summary>
        [Column(TypeName = "varchar(20)")]
        public string ProdHN_Origen { get; set; } = "";

        /// <summary>Usuario (token) que hizo el cambio.</summary>
        public long Usua_Id { get; set; }
        /// <summary>Usuario (navegación).</summary>
        public Usuario? Usua { get; set; }

        /// <summary>Fecha y hora del cambio.</summary>
        public DateTime ProdHN_Fecha { get; set; }
    }
}
