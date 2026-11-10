using System.ComponentModel.DataAnnotations;
using PlasticaribeAPI.Service;

namespace PlasticaribeAPI.DTOs
{
    /// <summary>
    /// Solicitud para crear una OT con sus cuatro procesos (F2-02). Solo contiene datos planos, sin navegaciones,
    /// así que un payload con objetos anidados no puede insertar registros adicionales.
    /// El número de OT, el creador (token), la fecha, la hora y el estado inicial los fija el servidor.
    /// </summary>
    public sealed class CrearOrdenTrabajoCompletaDto
    {
        /// <summary>Datos generales de la OT.</summary>
        [Required]
        public CrearOrdenTrabajoDatosDto OrdenTrabajo { get; set; } = null!;

        /// <summary>Datos del proceso de extrusión.</summary>
        [Required]
        public OtExtrusionDto Extrusion { get; set; } = null!;

        /// <summary>Datos del proceso de impresión.</summary>
        [Required]
        public OtImpresionDto Impresion { get; set; } = null!;

        /// <summary>Datos del proceso de laminado.</summary>
        [Required]
        public OtLaminadoDto Laminado { get; set; } = null!;

        /// <summary>Datos del proceso de sellado o corte.</summary>
        [Required]
        public OtSelladoCorteDto SelladoCorte { get; set; } = null!;
    }

    /// <summary>
    /// Solicitud para editar una OT con sus cuatro procesos (F2-03). Solo se aplican los campos de la lista blanca;
    /// <see cref="Ot_RowVersion"/> es la versión con la que se cargó la OT y permite detectar ediciones concurrentes.
    /// </summary>
    public sealed class EditarOrdenTrabajoCompletaDto
    {
        /// <summary>Versión de la fila (base64) recibida al consultar la OT; si cambió, la API responde 409.</summary>
        [Required]
        public string Ot_RowVersion { get; set; } = string.Empty;

        /// <summary>Datos generales editables de la OT.</summary>
        [Required]
        public EditarOrdenTrabajoDatosDto OrdenTrabajo { get; set; } = null!;

        /// <summary>Datos del proceso de extrusión.</summary>
        [Required]
        public OtExtrusionDto Extrusion { get; set; } = null!;

        /// <summary>Datos del proceso de impresión.</summary>
        [Required]
        public OtImpresionDto Impresion { get; set; } = null!;

        /// <summary>Datos del proceso de laminado.</summary>
        [Required]
        public OtLaminadoDto Laminado { get; set; } = null!;

        /// <summary>Datos del proceso de sellado o corte.</summary>
        [Required]
        public OtSelladoCorteDto SelladoCorte { get; set; } = null!;
    }

    /// <summary>
    /// Campos de la OT comunes a crear y editar (lista blanca de edición aprobada: cantidad, precio, margen, fecha de entrega,
    /// observación, mezcla, vendedor, etiquetas, cyrel/corte, procesos y costos validados).
    /// </summary>
    public abstract class OrdenTrabajoDatosBaseDto
    {
        /// <summary>Cantidad pedida en la presentación de la OT; debe ser mayor que 0.</summary>
        public decimal Ot_CantidadPedida { get; set; }

        /// <summary>Precio por unidad de la presentación; debe ser mayor que 0.</summary>
        public decimal Ot_ValorUnidad { get; set; }

        /// <summary>Margen adicional en porcentaje; no puede ser negativo.</summary>
        public decimal Ot_MargenAdicional { get; set; }

        /// <summary>Peso neto en kilos calculado por el front; el servidor lo valida.</summary>
        public decimal Ot_PesoNetoKg { get; set; }

        /// <summary>Valor del kilo calculado por el front; el servidor lo valida (en MTS lo fija el servidor).</summary>
        public decimal Ot_ValorKg { get; set; }

        /// <summary>Valor total de la OT (cantidad × precio); el servidor lo valida.</summary>
        public decimal Ot_ValorOT { get; set; }

        /// <summary>La OT pasa por extrusión.</summary>
        public bool Extrusion { get; set; }

        /// <summary>La OT pasa por impresión.</summary>
        public bool Impresion { get; set; }

        /// <summary>Proceso descontinuado; se conserva la columna.</summary>
        public bool Rotograbado { get; set; }

        /// <summary>La OT pasa por laminado.</summary>
        public bool Laminado { get; set; }

        /// <summary>La OT pasa por corte.</summary>
        public bool Ot_Corte { get; set; }

        /// <summary>La OT pasa por sellado.</summary>
        public bool Sellado { get; set; }

        /// <summary>La OT lleva cyrel.</summary>
        public bool Ot_Cyrel { get; set; }

        /// <summary>Mezcla de la OT.</summary>
        public long Mezcla_Id { get; set; }

        /// <summary>Vendedor (Usua_Id) de la OT.</summary>
        public long Id_Vendedor { get; set; }

        /// <summary>Observación general.</summary>
        public string? Ot_Observacion { get; set; }

        /// <summary>Indica si se muestra la empresa en las etiquetas.</summary>
        public bool MotrarEmpresaEtiquetas { get; set; }

        /// <summary>Fecha de entrega; obligatoria y no anterior a la fecha de creación.</summary>
        public DateTime? Ot_FechaEntrega { get; set; }
    }

    /// <summary>Datos generales para crear una OT.</summary>
    public sealed class CrearOrdenTrabajoDatosDto : OrdenTrabajoDatosBaseDto
    {
        /// <summary>Sede del cliente (SedeCli_Id).</summary>
        public long SedeCli_Id { get; set; }

        /// <summary>Producto (item) de la OT.</summary>
        public int Prod_Id { get; set; }

        /// <summary>Presentación de la OT; al crear solo se permiten Kg, Paquete, Und y MTS.</summary>
        public string UndMed_Id { get; set; } = string.Empty;

        /// <summary>Pedido externo asociado (hoy el front envía 517, sin pedido).</summary>
        public long PedExt_Id { get; set; }

        /// <summary>
        /// Opcional. Nombre (referencia) del producto: si difiere del guardado, se actualiza Productos.Prod_Nombre antes de
        /// crear la OT (TR_CrearOTs lo lleva a ClientesOT, ClientesOT_Item y Zeus) y queda el cambio en Productos_HistorialNombres.
        /// Null o vacío: no se cambia.
        /// </summary>
        [MaxLength(OrdenTrabajoNombres.LargoMaximoProducto)]
        public string? Prod_Nombre { get; set; }
    }

    /// <summary>
    /// Datos generales para editar una OT. Producto, presentación y estado no son editables: si se envían
    /// y difieren de los guardados, la API responde 422 (la anulación y los cambios de estado van en PR4).
    /// Sí se pueden cambiar la sede del cliente y los nombres: el del producto en el maestro y el del cliente solo en esta OT.
    /// </summary>
    public sealed class EditarOrdenTrabajoDatosDto : OrdenTrabajoDatosBaseDto
    {
        /// <summary>
        /// Opcional. Nombre del producto: si difiere del guardado, se actualiza Productos.Prod_Nombre (maestro) y TR_CrearOTs
        /// lo lleva a ClientesOT, ClientesOT_Item y al artículo de Zeus. Null o vacío: no se cambia.
        /// </summary>
        [MaxLength(OrdenTrabajoNombres.LargoMaximoProducto)]
        public string? Prod_Nombre { get; set; }

        /// <summary>
        /// Opcional. Nombre del cliente solo para esta OT (ClientesOT y ClientesOT_Item en BagPro); Clientes.Cli_Nombre no cambia.
        /// Igual al del maestro: la OT vuelve a usar el del maestro. Null o vacío: no se cambia.
        /// </summary>
        [MaxLength(OrdenTrabajoNombres.LargoMaximoCliente)]
        public string? Ot_NombreCliente { get; set; }

        /// <summary>Opcional. Sede del cliente: si difiere de la guardada, la OT pasa a ese cliente (debe existir; si no, 422).</summary>
        public long? SedeCli_Id { get; set; }

        /// <summary>Opcional, solo para comprobar que no cambió.</summary>
        public int? Prod_Id { get; set; }

        /// <summary>Opcional, solo para comprobar que no cambió.</summary>
        public string? UndMed_Id { get; set; }

        /// <summary>Opcional, solo para comprobar que no cambió.</summary>
        public int? Estado_Id { get; set; }
    }

    /// <summary>Datos del proceso de extrusión de la OT (sin Ids ni navegaciones).</summary>
    public sealed class OtExtrusionDto
    {
        /// <summary>Material.</summary>
        public int Material_Id { get; set; }
        /// <summary>Formato de extrusión.</summary>
        public long Formato_Id { get; set; }
        /// <summary>Pigmento.</summary>
        public int Pigmt_Id { get; set; }
        /// <summary>Calibre.</summary>
        public decimal Extrusion_Calibre { get; set; }
        /// <summary>Ancho 1.</summary>
        public decimal Extrusion_Ancho1 { get; set; }
        /// <summary>Ancho 2.</summary>
        public decimal Extrusion_Ancho2 { get; set; }
        /// <summary>Ancho 3.</summary>
        public decimal Extrusion_Ancho3 { get; set; }
        /// <summary>Unidad de medida de extrusión (Cms o Plgs).</summary>
        public string UndMed_Id { get; set; } = string.Empty;
        /// <summary>Tratado.</summary>
        public int Tratado_Id { get; set; }
        /// <summary>Peso de extrusión.</summary>
        public decimal Extrusion_Peso { get; set; }
        /// <summary>Grafilado.</summary>
        public bool? Extrusion_Grafilado { get; set; }
        /// <summary>Observación de extrusión.</summary>
        public string? Extrusion_Observacion { get; set; }
    }

    /// <summary>Datos del proceso de impresión de la OT (sin Ids ni navegaciones).</summary>
    public sealed class OtImpresionDto
    {
        /// <summary>Tipo de impresión.</summary>
        public int TpImpresion_Id { get; set; }
        /// <summary>Rodillo.</summary>
        public int Rodillo_Id { get; set; }
        /// <summary>Pista.</summary>
        public int Pista_Id { get; set; }
        /// <summary>Tinta 1.</summary>
        public long Tinta1_Id { get; set; }
        /// <summary>Tinta 2.</summary>
        public long Tinta2_Id { get; set; }
        /// <summary>Tinta 3.</summary>
        public long Tinta3_Id { get; set; }
        /// <summary>Tinta 4.</summary>
        public long Tinta4_Id { get; set; }
        /// <summary>Tinta 5.</summary>
        public long Tinta5_Id { get; set; }
        /// <summary>Tinta 6.</summary>
        public long Tinta6_Id { get; set; }
        /// <summary>Tinta 7.</summary>
        public long Tinta7_Id { get; set; }
        /// <summary>Tinta 8.</summary>
        public long Tinta8_Id { get; set; }
        /// <summary>Observación de impresión.</summary>
        public string? Impresion_Observacion { get; set; }
    }

    /// <summary>Datos del proceso de laminado de la OT (sin Ids ni navegaciones).</summary>
    public sealed class OtLaminadoDto
    {
        /// <summary>Capa 1.</summary>
        public int Capa_Id1 { get; set; }
        /// <summary>Capa 2.</summary>
        public int Capa_Id2 { get; set; }
        /// <summary>Capa 3.</summary>
        public int Capa_Id3 { get; set; }
        /// <summary>Calibre de la capa 1.</summary>
        public double LamCapa_Calibre1 { get; set; }
        /// <summary>Calibre de la capa 2.</summary>
        public double LamCapa_Calibre2 { get; set; }
        /// <summary>Calibre de la capa 3.</summary>
        public double LamCapa_Calibre3 { get; set; }
        /// <summary>Cantidad de la capa 1.</summary>
        public double LamCapa_Cantidad1 { get; set; }
        /// <summary>Cantidad de la capa 2.</summary>
        public double LamCapa_Cantidad2 { get; set; }
        /// <summary>Cantidad de la capa 3.</summary>
        public double LamCapa_Cantidad3 { get; set; }
        /// <summary>Observación de laminado.</summary>
        public string? Laminado_Observacion { get; set; }
    }

    /// <summary>Datos del proceso de sellado o corte de la OT (sin Ids ni navegaciones).</summary>
    public sealed class OtSelladoCorteDto
    {
        /// <summary>La OT es de corte.</summary>
        public bool Corte { get; set; }
        /// <summary>La OT es de sellado.</summary>
        public bool Sellado { get; set; }
        /// <summary>Formato del producto terminado (Tipos_Productos.TpProd_Id); su nombre decide la regla D2.</summary>
        public int Formato_Id { get; set; }
        /// <summary>Ancho.</summary>
        public decimal SelladoCorte_Ancho { get; set; }
        /// <summary>Largo.</summary>
        public decimal SelladoCorte_Largo { get; set; }
        /// <summary>Fuelle.</summary>
        public decimal? SelladoCorte_Fuelle { get; set; }
        /// <summary>Solapa.</summary>
        public decimal? SelladoCorte_Solapa { get; set; }
        /// <summary>Ancho 1.</summary>
        public decimal? SelladoCorte_Ancho1 { get; set; }
        /// <summary>Ancho 2.</summary>
        public decimal? SelladoCorte_Ancho2 { get; set; }
        /// <summary>Ancho 3.</summary>
        public decimal? SelladoCorte_Ancho3 { get; set; }
        /// <summary>Peso millar del producto (base del cálculo de costos).</summary>
        public decimal SelladoCorte_PesoMillar { get; set; }
        /// <summary>Tipo de sellado.</summary>
        public int TpSellado_Id { get; set; }
        /// <summary>Precio de sellado de día.</summary>
        public decimal SelladoCorte_PrecioSelladoDia { get; set; }
        /// <summary>Precio de sellado de noche.</summary>
        public decimal SelladoCorte_PrecioSelladoNoche { get; set; }
        /// <summary>Bolsas por paquete; es la que se guarda y la que usa el cálculo del Valor Kg de Paquete.</summary>
        public decimal SelladoCorte_CantBolsasPaquete { get; set; }
        /// <summary>Paquetes por bulto.</summary>
        public decimal SelladoCorte_CantBolsasBulto { get; set; }
        /// <summary>Peso del paquete.</summary>
        public decimal SelladoCorte_PesoPaquete { get; set; }
        /// <summary>Peso del bulto.</summary>
        public decimal SelladoCorte_PesoBulto { get; set; }
        /// <summary>Peso del producto.</summary>
        public decimal SelladoCorte_PesoProducto { get; set; }
        /// <summary>Etiqueta de ancho.</summary>
        public string SelladoCorte_Etiqueta_Ancho { get; set; } = string.Empty;
        /// <summary>Etiqueta de largo.</summary>
        public string SelladoCorte_Etiqueta_Largo { get; set; } = string.Empty;
        /// <summary>Etiqueta de fuelle.</summary>
        public string SelladoCorte_Etiqueta_Fuelle { get; set; } = string.Empty;
        /// <summary>Precio wiketiado de día, máquina 50.</summary>
        public decimal SelladoCorte_PrecioDia_Wik_Mq50 { get; set; }
        /// <summary>Precio wiketiado de noche, máquina 50.</summary>
        public decimal SelladoCorte_PrecioNoche_Wik_Mq50 { get; set; }
        /// <summary>Precio wiketiado de día, máquina 9.</summary>
        public decimal SelladoCorte_PrecioDia_Wik_Mq9 { get; set; }
        /// <summary>Precio wiketiado de noche, máquina 9.</summary>
        public decimal SelladoCorte_PrecioNoche_Wik_Mq9 { get; set; }
        /// <summary>Observación de sellado o corte.</summary>
        public string? SelladoCorte_Observacion { get; set; }
        /// <summary>Calibre de producto terminado.</summary>
        public decimal? SelladoCorte_Calibre { get; set; }
    }
}
