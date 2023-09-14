using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlasticaribeAPI.Models
{
    public class Productos_MateriasPrimas
    {
        [Key]
        public int Id { get; set; }
        public Producto? Producto { get; set; }
        public int Prod_Id { get; set; }
        public Unidad_Medida? Unidad_Medida { get; set; }
        public string UndMed_Id { get; set; }
        public Existencia_Productos? Existencia_Productos { get; set; }
        public long ExProd_Id { get; set; }
        public Materia_Prima? Materia_Prima { get; set; }
        public long MatPri_Id { get; set; }
        public Tinta? Tinta { get; set; }
        public long Tinta_Id { get; set; }
        public Bopp_Generico? Bopp_Generico { get; set; }
        public long Bopp_Id { get; set; }

        [Precision(18, 6)]
        public decimal Cantidad_Minima { get; set; }
        public Usuario? Usuario { get; set; }
        public long Usua_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Registro { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Hora_Registro { get; set; }
    }
}
