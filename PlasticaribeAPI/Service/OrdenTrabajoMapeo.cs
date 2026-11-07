using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Convierte los DTOs de creación y edición de OT en entidades (F2-02 y F2-03). Funciones puras: no consultan la BD.
    /// Solo se copian los campos permitidos; nunca se copian navegaciones ni llaves.
    /// </summary>
    public static class OrdenTrabajoMapeo
    {
        /// <summary>
        /// Crea la entidad de una OT nueva. El número, el creador, la fecha, la hora y el estado los fija el servidor.
        /// </summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="numeroOT">Número asignado por el servidor.</param>
        /// <param name="usuarioId">Usuario del token (creador).</param>
        /// <param name="ahora">Fecha y hora del servidor.</param>
        /// <param name="valorKg">Valor del kilo que se guarda (en MTS, 0; en las demás, el validado).</param>
        /// <returns>La OT lista para insertar.</returns>
        public static Orden_Trabajo NuevaOrden(CrearOrdenTrabajoDatosDto d, long numeroOT, long usuarioId, DateTime ahora, decimal valorKg)
        {
            var orden = new Orden_Trabajo
            {
                Numero_OT = numeroOT,
                SedeCli_Id = d.SedeCli_Id,
                Prod_Id = d.Prod_Id,
                UndMed_Id = d.UndMed_Id,
                PedExt_Id = d.PedExt_Id,
                Usua_Id = usuarioId,
                Ot_FechaCreacion = ahora.Date,
                Ot_Hora = ahora.ToString("H:mm:ss"),
                Estado_Id = OrdenTrabajoReglas.EstadoInicial,
                Ot_Observacion = string.Empty,
            };
            CopiarCamposEditables(d, orden, valorKg);
            return orden;
        }

        /// <summary>
        /// Aplica a una OT existente solo los campos de la lista blanca de edición y registra al último editor (D4).
        /// No toca Numero_OT, Usua_Id (creador), fecha y hora de creación, producto, presentación, sede, estado ni pedido.
        /// </summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="orden">OT cargada de la BD (con seguimiento de cambios).</param>
        /// <param name="usuarioId">Usuario del token (último editor).</param>
        /// <param name="ahora">Fecha y hora del servidor.</param>
        /// <param name="valorKg">Valor del kilo que se guarda (en MTS, el que ya tenía la OT).</param>
        public static void AplicarEdicion(EditarOrdenTrabajoDatosDto d, Orden_Trabajo orden, long usuarioId, DateTime ahora, decimal valorKg)
        {
            CopiarCamposEditables(d, orden, valorKg);
            orden.Ot_UsuarioModifica = usuarioId;
            orden.Ot_FechaModifica = ahora;
        }

        /// <summary>
        /// Copia los campos comunes de la lista blanca (cantidad, precio, margen, costos, procesos, mezcla, vendedor,
        /// observación, etiquetas y fecha de entrega).
        /// </summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="orden">OT destino.</param>
        /// <param name="valorKg">Valor del kilo que se guarda.</param>
        private static void CopiarCamposEditables(OrdenTrabajoDatosBaseDto d, Orden_Trabajo orden, decimal valorKg)
        {
            orden.Ot_CantidadPedida = d.Ot_CantidadPedida;
            orden.Ot_ValorUnidad = d.Ot_ValorUnidad;
            orden.Ot_MargenAdicional = d.Ot_MargenAdicional;
            orden.Ot_PesoNetoKg = d.Ot_PesoNetoKg;
            orden.Ot_ValorKg = valorKg;
            orden.Ot_ValorOT = d.Ot_ValorOT;
            orden.Extrusion = d.Extrusion;
            orden.Impresion = d.Impresion;
            orden.Rotograbado = d.Rotograbado;
            orden.Laminado = d.Laminado;
            orden.Ot_Corte = d.Ot_Corte;
            orden.Sellado = d.Sellado;
            orden.Ot_Cyrel = d.Ot_Cyrel;
            orden.Mezcla_Id = d.Mezcla_Id;
            orden.Id_Vendedor = d.Id_Vendedor;
            orden.Ot_Observacion = d.Ot_Observacion ?? string.Empty;
            orden.MotrarEmpresaEtiquetas = d.MotrarEmpresaEtiquetas;
            orden.Ot_FechaEntrega = d.Ot_FechaEntrega?.Date;
        }

        /// <summary>Crea la entidad de extrusión de la OT.</summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="otId">Id interno de la OT.</param>
        /// <returns>El registro de extrusión con la llave en 0.</returns>
        public static OT_Extrusion Extrusion(OtExtrusionDto d, long otId) => new()
        {
            Ot_Id = otId,
            Material_Id = d.Material_Id,
            Formato_Id = d.Formato_Id,
            Pigmt_Id = d.Pigmt_Id,
            Extrusion_Calibre = d.Extrusion_Calibre,
            Extrusion_Ancho1 = d.Extrusion_Ancho1,
            Extrusion_Ancho2 = d.Extrusion_Ancho2,
            Extrusion_Ancho3 = d.Extrusion_Ancho3,
            UndMed_Id = d.UndMed_Id,
            Tratado_Id = d.Tratado_Id,
            Extrusion_Peso = d.Extrusion_Peso,
            Extrusion_Grafilado = d.Extrusion_Grafilado,
            Extrusion_Observacion = d.Extrusion_Observacion,
        };

        /// <summary>Crea la entidad de impresión de la OT.</summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="otId">Id interno de la OT.</param>
        /// <returns>El registro de impresión con la llave en 0.</returns>
        public static OT_Impresion Impresion(OtImpresionDto d, long otId) => new()
        {
            Ot_Id = otId,
            TpImpresion_Id = d.TpImpresion_Id,
            Rodillo_Id = d.Rodillo_Id,
            Pista_Id = d.Pista_Id,
            Tinta1_Id = d.Tinta1_Id,
            Tinta2_Id = d.Tinta2_Id,
            Tinta3_Id = d.Tinta3_Id,
            Tinta4_Id = d.Tinta4_Id,
            Tinta5_Id = d.Tinta5_Id,
            Tinta6_Id = d.Tinta6_Id,
            Tinta7_Id = d.Tinta7_Id,
            Tinta8_Id = d.Tinta8_Id,
            Impresion_Observacion = d.Impresion_Observacion,
        };

        /// <summary>Crea la entidad de laminado de la OT.</summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="otId">Id interno de la OT.</param>
        /// <returns>El registro de laminado con la llave en 0.</returns>
        public static OT_Laminado Laminado(OtLaminadoDto d, long otId) => new()
        {
            OT_Id = otId,
            Capa_Id1 = d.Capa_Id1,
            Capa_Id2 = d.Capa_Id2,
            Capa_Id3 = d.Capa_Id3,
            LamCapa_Calibre1 = d.LamCapa_Calibre1,
            LamCapa_Calibre2 = d.LamCapa_Calibre2,
            LamCapa_Calibre3 = d.LamCapa_Calibre3,
            LamCapa_Cantidad1 = d.LamCapa_Cantidad1,
            LamCapa_Cantidad2 = d.LamCapa_Cantidad2,
            LamCapa_Cantidad3 = d.LamCapa_Cantidad3,
            Laminado_Observacion = d.Laminado_Observacion,
        };

        /// <summary>Crea la entidad de sellado/corte de la OT.</summary>
        /// <param name="d">Datos recibidos.</param>
        /// <param name="otId">Id interno de la OT.</param>
        /// <returns>El registro de sellado/corte con la llave en 0.</returns>
        public static OT_Sellado_Corte SelladoCorte(OtSelladoCorteDto d, long otId) => new()
        {
            Ot_Id = otId,
            Corte = d.Corte,
            Sellado = d.Sellado,
            Formato_Id = d.Formato_Id,
            SelladoCorte_Ancho = d.SelladoCorte_Ancho,
            SelladoCorte_Largo = d.SelladoCorte_Largo,
            SelladoCorte_Fuelle = d.SelladoCorte_Fuelle,
            SelladoCorte_Solapa = d.SelladoCorte_Solapa,
            SelladoCorte_Ancho1 = d.SelladoCorte_Ancho1,
            SelladoCorte_Ancho2 = d.SelladoCorte_Ancho2,
            SelladoCorte_Ancho3 = d.SelladoCorte_Ancho3,
            SelladoCorte_PesoMillar = d.SelladoCorte_PesoMillar,
            TpSellado_Id = d.TpSellado_Id,
            SelladoCorte_PrecioSelladoDia = d.SelladoCorte_PrecioSelladoDia,
            SelladoCorte_PrecioSelladoNoche = d.SelladoCorte_PrecioSelladoNoche,
            SelladoCorte_CantBolsasPaquete = d.SelladoCorte_CantBolsasPaquete,
            SelladoCorte_CantBolsasBulto = d.SelladoCorte_CantBolsasBulto,
            SelladoCorte_PesoPaquete = d.SelladoCorte_PesoPaquete,
            SelladoCorte_PesoBulto = d.SelladoCorte_PesoBulto,
            SelladoCorte_PesoProducto = d.SelladoCorte_PesoProducto,
            SelladoCorte_Etiqueta_Ancho = d.SelladoCorte_Etiqueta_Ancho,
            SelladoCorte_Etiqueta_Largo = d.SelladoCorte_Etiqueta_Largo,
            SelladoCorte_Etiqueta_Fuelle = d.SelladoCorte_Etiqueta_Fuelle,
            SelladoCorte_PrecioDia_Wik_Mq50 = d.SelladoCorte_PrecioDia_Wik_Mq50,
            SelladoCorte_PrecioNoche_Wik_Mq50 = d.SelladoCorte_PrecioNoche_Wik_Mq50,
            SelladoCorte_PrecioDia_Wik_Mq9 = d.SelladoCorte_PrecioDia_Wik_Mq9,
            SelladoCorte_PrecioNoche_Wik_Mq9 = d.SelladoCorte_PrecioNoche_Wik_Mq9,
            SelladoCorte_Observacion = d.SelladoCorte_Observacion,
            SelladoCorte_Calibre = d.SelladoCorte_Calibre,
        };
    }
}
