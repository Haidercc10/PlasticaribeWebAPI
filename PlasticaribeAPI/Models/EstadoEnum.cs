namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Identificadores de la tabla Estados. El valor de cada miembro debe coincidir con el Estado_Id en base de datos.
    /// </summary>
    public enum EstadoEnum
    {
        Activo = 1,

        /// <summary>EN PROCESO. En precargues de despacho: el usuario sigue agregando rollos.</summary>
        EnProceso = 2,

        Anulado = 3,
        Cancelado = 4,
        Finalizado = 5,
        Iniciado = 6,
        Inactivo = 8,
        EnPedido = 9,
        EnProduccion = 10,

        /// <summary>PENDIENTE. En precargues de despacho: finalizado, pendiente de asociar a una orden de facturación.</summary>
        Pendiente = 11,

        Parcial = 12,
        Total = 13,
        Asignada = 14,
        Abierta = 15,

        /// <summary>EN PROCESO (segundo registro con el mismo nombre en la tabla Estados).</summary>
        EnProceso2 = 16,

        Terminada = 17,
        Cerrada = 18,

        /// <summary>DISPONIBLE. Rollo disponible en inventario de despacho.</summary>
        Disponible = 19,

        Facturado = 20,
        Enviado = 21,
        Eliminado = 22,
        NoDisponible = 23,
        Devuelto = 24,

        /// <summary>ELIMINADO 2.</summary>
        Eliminado2 = 25,

        Aceptado = 26,
        PedidoPendienteZeus = 27,
        Abierto = 28,
        EnRevision = 29,
        Resuelto = 30,
        Entregado = 31,
        DevueltoPorCalidad = 32,
        DevueltoConforme = 33,
        DevueltoPorCalidadReingresado = 34,
        PorRevisar = 35,
        DevueltoNoConforme = 36,
        Revisado = 37,
        PorReponer = 38,
        EnReposicion = 39,
        Preingresado = 40,
        Ingresado = 41,
        EntregaParcial = 42,
        EntregaTotal = 43,
        Peletizado = 44,
        Reempaque = 45,
        Reempacado = 46,
        DevueltoReingresado = 47,
        Solicitado = 48,
        SalidaParcial = 49,

        /// <summary>PRECARGADO. Rollo agregado a un precargue de despacho.</summary>
        Precargado = 50,

        Procesado = 51,

        /// <summary>REINGRESADO INV202504.</summary>
        ReingresadoInv202504 = 52,

        PreDevuelto = 53,
        PorFinalizar = 54,
    }
}
