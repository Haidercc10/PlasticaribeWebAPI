namespace PlasticaribeAPI.DTOs
{
    /// <summary>
    /// Ficha complementaria del item (datos que no existen en BagPro). Se usa como cuerpo del PUT (upsert) y como respuesta.
    /// En el PUT, Prod_Id y Usua_Id son opcionales (si se envían deben coincidir con la ruta y con el usuario del token)
    /// y los campos de auditoría (fechas y Usua_Modifica) se ignoran: los asigna el servidor.
    /// </summary>
    public sealed class FichaComplementariaItemDto
    {
        /// <summary>Código del item (Prod_Id).</summary>
        public int? Prod_Id { get; set; }

        /// <summary>Tipo de desembobinado final, 1..16 (1-8 exterior, 9-16 interior). Opcional.</summary>
        public int? Embobinado_Id { get; set; }

        /// <summary>Código de barras: solo dígitos, máximo 20. Opcional.</summary>
        public string? CodigoBarras { get; set; }

        /// <summary>Repeticiones (texto libre), máximo 50 caracteres. Opcional.</summary>
        public string? Repeticiones { get; set; }

        /// <summary>Paso entre guías (texto libre), máximo 50 caracteres. Opcional.</summary>
        public string? PasoGuias { get; set; }

        /// <summary>Ubicación de la guía, máximo 100 caracteres. Opcional.</summary>
        public string? UbicacionGuia { get; set; }

        /// <summary>Indica si la lámina es de largo continuo.</summary>
        public bool LargoContinuo { get; set; }

        /// <summary>true si la mezcla del item se ajustó en Crear Item (la OT muestra la composición del item y no la predefinida).</summary>
        public bool MezclaAjustada { get; set; }

        /// <summary>Usuario que creó la ficha. En el PUT, si se envía, debe ser el usuario del token.</summary>
        public long? Usua_Id { get; set; }

        /// <summary>Fecha de creación (solo respuesta).</summary>
        public DateTime? Fecha_Creacion { get; set; }

        /// <summary>Usuario de la última modificación (solo respuesta).</summary>
        public long? Usua_Modifica { get; set; }

        /// <summary>Fecha de la última modificación (solo respuesta).</summary>
        public DateTime? Fecha_Modifica { get; set; }
    }
}
