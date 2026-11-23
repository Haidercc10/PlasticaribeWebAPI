namespace PlasticaribeAPI.Models
{
    /// <summary>
    /// Identificadores de la tabla Estados. El valor de cada miembro debe coincidir con el Estado_Id en base de datos.
    /// En cada miembro se indica su tipo de estado (<see cref="TipoEstadoEnum"/>). Algunos nombres se repiten en la
    /// tabla con distinto tipo (p. ej. PENDIENTE 11 y 56); el nombre del miembro lleva el prefijo de su proceso.
    /// </summary>
    /// <remarks>Actualizado con PlasticaribeBDD_Test el 2026-11-23 (Estados 1 a 61; no existe el 7).</remarks>
    public enum EstadoEnum
    {
        /// <summary>ACTIVO. Tipo General.</summary>
        Activo = 1,

        /// <summary>EN PROCESO. Tipo Documentos. En precargues de despacho: el usuario sigue agregando rollos.</summary>
        EnProceso = 2,

        /// <summary>ANULADO. Tipo Documentos.</summary>
        Anulado = 3,

        /// <summary>CANCELADO. Tipo Documentos.</summary>
        Cancelado = 4,

        /// <summary>FINALIZADO. Tipo Documentos.</summary>
        Finalizado = 5,

        /// <summary>INICIADO. Tipo Documentos.</summary>
        Iniciado = 6,

        /// <summary>INACTIVO. Tipo General.</summary>
        Inactivo = 8,

        /// <summary>EN PEDIDO. Tipo Pedido.</summary>
        EnPedido = 9,

        /// <summary>EN PRODUCCIÓN. Tipo Pedido.</summary>
        EnProduccion = 10,

        /// <summary>PENDIENTE. Tipo Documentos. En precargues de despacho: finalizado, pendiente de asociar a una orden de facturación.</summary>
        Pendiente = 11,

        /// <summary>PARCIAL. Tipo Documentos.</summary>
        Parcial = 12,

        /// <summary>TOTAL. Tipo Documentos.</summary>
        Total = 13,

        /// <summary>ASIGNADA. Tipo Orden Trabajo: la OT tiene asignaciones de materia prima.</summary>
        Asignada = 14,

        /// <summary>ABIERTA. Tipo Orden Trabajo: sin asignaciones ni producción.</summary>
        Abierta = 15,

        /// <summary>EN PROCESO. Tipo Orden Trabajo (segundo registro con el mismo nombre en la tabla Estados).</summary>
        EnProceso2 = 16,

        /// <summary>TERMINADA. Tipo Orden Trabajo.</summary>
        Terminada = 17,

        /// <summary>CERRADA. Tipo Orden Trabajo.</summary>
        Cerrada = 18,

        /// <summary>DISPONIBLE. Tipo Rollos / Productos. Rollo disponible en inventario de despacho.</summary>
        Disponible = 19,

        /// <summary>FACTURADO. Tipo Rollos / Productos.</summary>
        Facturado = 20,

        /// <summary>ENVIADO. Tipo Rollos / Productos.</summary>
        Enviado = 21,

        /// <summary>ELIMINADO. Tipo Rollos / Productos.</summary>
        Eliminado = 22,

        /// <summary>NO DISPONIBLE. Tipo Rollos / Productos.</summary>
        NoDisponible = 23,

        /// <summary>DEVUELTO. Tipo Rollos / Productos.</summary>
        Devuelto = 24,

        /// <summary>ELIMINADO 2. Tipo Rollos / Productos.</summary>
        Eliminado2 = 25,

        /// <summary>ACEPTADO. Tipo General.</summary>
        Aceptado = 26,

        /// <summary>PEDIDO PENDIENTE ZEUS. Tipo General.</summary>
        PedidoPendienteZeus = 27,

        /// <summary>ABIERTO. Tipo Tickets.</summary>
        Abierto = 28,

        /// <summary>EN REVISIÓN. Tipo Tickets.</summary>
        EnRevision = 29,

        /// <summary>RESUELTO. Tipo Tickets.</summary>
        Resuelto = 30,

        /// <summary>ENTREGADO. Tipo Rollos / Productos.</summary>
        Entregado = 31,

        /// <summary>DEVUELTO POR CALIDAD. Tipo Rollos / Productos.</summary>
        DevueltoPorCalidad = 32,

        /// <summary>DEVUELTO CONFORME. Tipo Rollos / Productos.</summary>
        DevueltoConforme = 33,

        /// <summary>DEVUELTO POR CALIDAD REINGRESADO. Tipo Rollos / Productos.</summary>
        DevueltoPorCalidadReingresado = 34,

        /// <summary>POR REVISAR. Tipo Rollos / Productos.</summary>
        PorRevisar = 35,

        /// <summary>DEVUELTO NO CONFORME. Tipo Rollos / Productos.</summary>
        DevueltoNoConforme = 36,

        /// <summary>REVISADO. Tipo Rollos / Productos.</summary>
        Revisado = 37,

        /// <summary>POR REPONER. Tipo Rollos / Productos.</summary>
        PorReponer = 38,

        /// <summary>EN REPOSICION. Tipo Rollos / Productos.</summary>
        EnReposicion = 39,

        /// <summary>PREINGRESADO. Tipo Recuperados.</summary>
        Preingresado = 40,

        /// <summary>INGRESADO. Tipo Recuperados.</summary>
        Ingresado = 41,

        /// <summary>ENTREGA PARCIAL. Tipo Recuperados.</summary>
        EntregaParcial = 42,

        /// <summary>ENTREGA TOTAL. Tipo Recuperados.</summary>
        EntregaTotal = 43,

        /// <summary>PELETIZADO. Tipo Rollos / Productos.</summary>
        Peletizado = 44,

        /// <summary>REEMPAQUE. Tipo Rollos / Productos.</summary>
        Reempaque = 45,

        /// <summary>REEMPACADO. Tipo Rollos / Productos.</summary>
        Reempacado = 46,

        /// <summary>DEVUELTO REINGRESADO. Tipo Rollos / Productos.</summary>
        DevueltoReingresado = 47,

        /// <summary>SOLICITADO. Tipo Rollos / Productos.</summary>
        Solicitado = 48,

        /// <summary>SALIDA PARCIAL. Tipo Recuperados.</summary>
        SalidaParcial = 49,

        /// <summary>PRECARGADO. Tipo Despacho. Rollo agregado a un precargue de despacho.</summary>
        Precargado = 50,

        /// <summary>PROCESADO. Tipo Rollos / Productos.</summary>
        Procesado = 51,

        /// <summary>REINGRESADO INV202504. Tipo Despacho (inventario del 19 y 20 de abril de 2025).</summary>
        ReingresadoInv202504 = 52,

        /// <summary>PRE-DEVUELTO. Tipo Rollos / Productos.</summary>
        PreDevuelto = 53,

        /// <summary>POR FINALIZAR. Tipo Rollos / Productos.</summary>
        PorFinalizar = 54,

        /// <summary>
        /// VERIFICADO. Tipo Rollos / Productos. Lo usaban las solicitudes de material antes del 2026-11-23; ahora usan
        /// <see cref="SolicitudMpVerificada"/>.
        /// </summary>
        Verificado = 55,

        /// <summary>PENDIENTE. Tipo Solicitud y asignación MP: sin material asignado.</summary>
        SolicitudMpPendiente = 56,

        /// <summary>PARCIAL. Tipo Solicitud y asignación MP: con material que no cubre lo solicitado (o el tope de la OT).</summary>
        SolicitudMpParcial = 57,

        /// <summary>COMPLETADA. Tipo Solicitud y asignación MP: recibió todo, o la OT llegó al tope o se completó a mano.</summary>
        SolicitudMpCompletada = 58,

        /// <summary>FINALIZADA. Tipo Solicitud y asignación MP: se cerró la asignación de materiales de la OT.</summary>
        SolicitudMpFinalizada = 59,

        /// <summary>VERIFICADA. Tipo Solicitud y asignación MP: el solicitante comprobó lo recibido para la OT.</summary>
        SolicitudMpVerificada = 60,

        /// <summary>CANCELADA. Tipo Solicitud y asignación MP.</summary>
        SolicitudMpCancelada = 61,
    }

    /// <summary>
    /// Identificadores de la tabla Tipos_Estados. El valor de cada miembro debe coincidir con el TpEstado_Id en base de datos.
    /// </summary>
    /// <remarks>Actualizado con PlasticaribeBDD_Test el 2026-11-23 (Solicitud y asignación MP es el 10).</remarks>
    public enum TipoEstadoEnum
    {
        /// <summary>Documentos: pedidos, cotizaciones, facturas, devoluciones, etc.</summary>
        Documentos = 1,

        /// <summary>General: activo, inactivo y otros de uso general.</summary>
        General = 2,

        /// <summary>Pedido: clientes y productos creados en el pedido que aún no están autorizados.</summary>
        Pedido = 3,

        /// <summary>Orden Trabajo: exclusivo de las órdenes de trabajo.</summary>
        OrdenTrabajo = 4,

        /// <summary>Rollos / Productos: exclusivo de los rollos de cada producto.</summary>
        RollosProductos = 5,

        /// <summary>Mantenimiento.</summary>
        Mantenimiento = 6,

        /// <summary>Tickets.</summary>
        Tickets = 7,

        /// <summary>Recuperados.</summary>
        Recuperados = 8,

        /// <summary>Despacho.</summary>
        Despacho = 9,

        /// <summary>Solicitud y asignación MP: solicitudes de material, solicitud de cada OT y su asignación de materia prima.</summary>
        SolicitudAsignacionMp = 10,
    }
}
