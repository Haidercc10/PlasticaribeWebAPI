using PlasticaribeAPI.DTOs;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Reglas puras (sin base de datos) de la creación y edición de OT: presentaciones permitidas, equivalencias con
    /// BagPro y Zeus que usa TR_CrearOTs, validación de rangos y clasificación de errores de SQL Server.
    /// </summary>
    public static class OrdenTrabajoReglas
    {
        /// <summary>Estado con el que se crea toda OT.</summary>
        public const int EstadoInicial = 15;

        /// <summary>Presentaciones permitidas al CREAR una OT (Rollo no se usa). La edición no se bloquea por presentación.</summary>
        public static readonly IReadOnlyList<string> PresentacionesPermitidasCrear = new[] { "Kg", "Paquete", "Und", "MTS" };

        /// <summary>
        /// Nombre de la presentación en BagPro (ClientesOT_Item.PtPresentacionNom), igual que en TR_CrearOTs.
        /// </summary>
        /// <param name="presentacion">Presentación de Plasticaribe (UndMed_Id).</param>
        /// <returns>El nombre en BagPro, o cadena vacía si no tiene equivalencia.</returns>
        public static string PresentacionBagPro(string presentacion) => presentacion switch
        {
            "Und" => "Unidad",
            "Kg" => "Kilo",
            "Paquete" => "Paquete",
            "MTS" => "MTS",
            _ => string.Empty,
        };

        /// <summary>
        /// Presentación del artículo en Zeus (Articulo.Presentacion), igual que en TR_CrearOTs.
        /// </summary>
        /// <param name="presentacion">Presentación de Plasticaribe (UndMed_Id).</param>
        /// <returns>La presentación en Zeus, o cadena vacía si no tiene equivalencia.</returns>
        public static string PresentacionZeus(string presentacion) => presentacion switch
        {
            "Und" => "UND",
            "Kg" => "KLS",
            "Paquete" => "PAQ",
            "MTS" => "MTS",
            _ => string.Empty,
        };

        /// <summary>
        /// Valida los rangos de los datos generales de la OT (respuesta 400).
        /// </summary>
        /// <param name="datos">Datos recibidos.</param>
        /// <param name="fechaCreacion">Fecha de creación de la OT (hoy al crear; la guardada al editar).</param>
        /// <param name="fechaEntregaGuardada">Fecha de entrega guardada (al editar); si no cambió, no se exige que sea posterior a la creación.</param>
        /// <returns>Errores por campo; vacío si los datos son válidos.</returns>
        public static Dictionary<string, string[]> ValidarRangos(OrdenTrabajoDatosBaseDto datos, DateTime fechaCreacion, DateTime? fechaEntregaGuardada = null)
        {
            var errores = new Dictionary<string, string[]>();
            if (datos.Ot_CantidadPedida <= 0)
                errores["Ot_CantidadPedida"] = new[] { "La cantidad debe ser mayor que 0." };
            if (datos.Ot_ValorUnidad < 0)
                errores["Ot_ValorUnidad"] = new[] { "El precio no puede ser negativo." };
            if (datos.Ot_MargenAdicional < 0)
                errores["Ot_MargenAdicional"] = new[] { "El margen no puede ser negativo." };
            if (datos.Ot_PesoNetoKg < 0 || datos.Ot_ValorKg < 0 || datos.Ot_ValorOT < 0)
                errores["Costos"] = new[] { "Neto Kg, Valor Kg y Valor OT no pueden ser negativos." };

            if (datos.Ot_FechaEntrega == null)
                errores["Ot_FechaEntrega"] = new[] { "La fecha de entrega es obligatoria." };
            else
            {
                var cambio = fechaEntregaGuardada == null || fechaEntregaGuardada.Value.Date != datos.Ot_FechaEntrega.Value.Date;
                if (cambio && datos.Ot_FechaEntrega.Value.Date < fechaCreacion.Date)
                    errores["Ot_FechaEntrega"] = new[] { $"La fecha de entrega no puede ser anterior a la fecha de creación ({fechaCreacion:yyyy-MM-dd})." };
            }
            return errores;
        }

        /// <summary>
        /// Clasifica un error de SQL Server de la creación o edición de una OT en una respuesta HTTP legible (F2-12).
        /// </summary>
        /// <param name="numeroError">Número del error de SQL Server.</param>
        /// <returns>Código HTTP y mensaje, o null si el error no es de los previstos (se deja como 500).</returns>
        public static (int StatusCode, string Mensaje)? ClasificarErrorSql(int numeroError) => numeroError switch
        {
            50001 => (409, "El número de OT ya existe en BagPro. Intente crear la OT de nuevo."),
            2627 or 2601 => (409, "El número de OT ya está en uso. Intente crear la OT de nuevo."),
            547 => (422, "Alguno de los datos de la OT hace referencia a un registro que no existe (cliente, producto, mezcla, material, tinta, etc.)."),
            7202 or 7303 or 7314 or 7321 or 7343 or 7356 or 7391 or 7399 or 8501 or 8524 =>
                (502, "No fue posible comunicarse con BagPro o Zeus para replicar la OT. No se guardó ningún cambio; intente de nuevo más tarde."),
            _ => null,
        };
    }
}
