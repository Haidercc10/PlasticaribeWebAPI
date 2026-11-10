namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Reglas puras (sin base de datos) para cambiar los nombres al editar una OT:
    /// el del producto se cambia en el maestro (Productos.Prod_Nombre) y TR_CrearOTs lo lleva a BagPro y Zeus;
    /// el del cliente se cambia solo en la OT (Orden_Trabajo.Ot_NombreCliente) y TR_CrearOTs lo lleva a ClientesOT y
    /// ClientesOT_Item de BagPro, sin tocar Clientes.Cli_Nombre.
    /// </summary>
    public static class OrdenTrabajoNombres
    {
        /// <summary>Largo de Productos.Prod_Nombre (varchar(100)).</summary>
        public const int LargoMaximoProducto = 100;

        /// <summary>Largo de Orden_Trabajo.Ot_NombreCliente (varchar(150)): igual a Existencias_Productos.ExProd_Cliente, adonde BagPro lleva ClienteNom.</summary>
        public const int LargoMaximoCliente = 150;

        /// <summary>
        /// Indica el nombre que se debe guardar en el maestro de productos.
        /// </summary>
        /// <param name="enviado">Nombre recibido del front (puede venir con espacios, vacío o null).</param>
        /// <param name="actual">Nombre guardado en Productos.Prod_Nombre.</param>
        /// <returns>El nombre nuevo, sin espacios al inicio ni al final; null si no se envió o es igual al guardado.</returns>
        public static string? NombreProductoACambiar(string? enviado, string? actual)
        {
            var nuevo = enviado?.Trim();
            if (string.IsNullOrEmpty(nuevo))
                return null;
            return string.Equals(nuevo, actual?.Trim(), StringComparison.Ordinal) ? null : nuevo;
        }

        /// <summary>
        /// Calcula el valor de Orden_Trabajo.Ot_NombreCliente después de editar la OT.
        /// </summary>
        /// <param name="enviado">Nombre recibido del front (puede venir con espacios, vacío o null).</param>
        /// <param name="guardadoEnOt">Valor actual de Ot_NombreCliente.</param>
        /// <param name="maestro">Nombre del cliente en Clientes.Cli_Nombre.</param>
        /// <returns>
        /// El valor guardado si no se envió nombre; null si el enviado es igual al del maestro (la OT vuelve a usar el del maestro);
        /// en otro caso, el nombre enviado sin espacios al inicio ni al final.
        /// </returns>
        public static string? NombreClienteOt(string? enviado, string? guardadoEnOt, string? maestro)
        {
            var nuevo = enviado?.Trim();
            if (string.IsNullOrEmpty(nuevo))
                return guardadoEnOt;
            return string.Equals(nuevo, maestro?.Trim(), StringComparison.Ordinal) ? null : nuevo;
        }
    }
}
