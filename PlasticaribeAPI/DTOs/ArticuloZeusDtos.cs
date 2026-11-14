namespace PlasticaribeAPI.DTOs
{
    /// <summary>Cuerpo de la solicitud para crear el artículo de un producto en Zeus.</summary>
    public sealed class CrearArticuloZeusRequest
    {
        /// <summary>Precio unitario de venta en la presentación indicada. Obligatorio y mayor que cero.</summary>
        public decimal? Precio { get; set; }
    }

    /// <summary>Resultado de la creación del artículo en Zeus.</summary>
    public sealed class CrearArticuloZeusResponse
    {
        /// <summary>Indica si en esta solicitud se creó el artículo en Zeus.</summary>
        public bool Creado { get; set; }

        /// <summary>Indica si el artículo ya existía en Zeus con esa presentación (no se envió nada).</summary>
        public bool YaExistia { get; set; }
    }

    /// <summary>Datos con los que se arma el XML del artículo enviado al servicio SOAP de Zeus.</summary>
    public sealed class ArticuloZeusDatos
    {
        /// <summary>Código del artículo (Prod_Id de Plasticaribe).</summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>Nombre del artículo.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripción del artículo (también se envía como descripción en otro idioma).</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Grupo de inventario Zeus (también se usa como grupo auxiliar).</summary>
        public string Grupo { get; set; } = string.Empty;

        /// <summary>Presentación Zeus del artículo.</summary>
        public string Presentacion { get; set; } = string.Empty;

        /// <summary>Grupo de facturación (grupo + presentación). Si es nulo no se envía el elemento.</summary>
        public string? GrupoFacturacion { get; set; }

        /// <summary>Costo promedio del artículo.</summary>
        public decimal Costo { get; set; }

        /// <summary>Precio de venta del artículo.</summary>
        public decimal PrecioVenta { get; set; }
    }

    /// <summary>
    /// Artículo de Zeus de un producto en una presentación (dbo.zeus_ArticuloItems), para el estado de sincronización.
    /// </summary>
    public sealed class ArticuloZeusResumen
    {
        /// <summary>Código del artículo (Prod_Id).</summary>
        public string Codigo { get; set; } = string.Empty;
        /// <summary>Presentación Zeus (UND, KLS, PAQ, MTS).</summary>
        public string Presentacion { get; set; } = string.Empty;
        /// <summary>Nombre del artículo.</summary>
        public string? Nombre { get; set; }
        /// <summary>Descripción del artículo.</summary>
        public string? Descripcion { get; set; }
        /// <summary>Grupo de inventario.</summary>
        public string? Grupo { get; set; }
        /// <summary>Grupo de facturación.</summary>
        public string? GrupoFacturacion { get; set; }
        /// <summary>Precio de venta (PrecioVenta).</summary>
        public decimal? Precio { get; set; }
        /// <summary>Costo del artículo (CostoArticulo).</summary>
        public decimal? Costo { get; set; }
    }
}
