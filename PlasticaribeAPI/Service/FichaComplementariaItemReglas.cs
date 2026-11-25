using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Service
{
    /// <summary>
    /// Reglas puras (sin base de datos) de la ficha complementaria del item: normalización de textos y validación
    /// de rangos y formatos antes del upsert.
    /// </summary>
    public static class FichaComplementariaItemReglas
    {
        /// <summary>Primer tipo de embobinado válido (exterior 1-8).</summary>
        public const int EmbobinadoMinimo = 1;

        /// <summary>Último tipo de embobinado válido (interior 9-16).</summary>
        public const int EmbobinadoMaximo = 16;

        /// <summary>Longitud máxima del código de barras (columna varchar(20)).</summary>
        public const int LargoCodigoBarras = 20;

        /// <summary>Longitud máxima de Repeticiones y PasoGuias (columnas varchar(50)).</summary>
        public const int LargoTextoCorto = 50;

        /// <summary>Longitud máxima de UbicacionGuia (columna varchar(100)).</summary>
        public const int LargoUbicacionGuia = 100;

        /// <summary>
        /// Recorta los textos y convierte los vacíos en null, sin modificar el resto de campos.
        /// </summary>
        /// <param name="dto">Datos recibidos; se modifican en el mismo objeto.</param>
        public static void Normalizar(FichaComplementariaItemDto dto)
        {
            dto.CodigoBarras = Limpiar(dto.CodigoBarras);
            dto.Repeticiones = Limpiar(dto.Repeticiones);
            dto.PasoGuias = Limpiar(dto.PasoGuias);
            dto.UbicacionGuia = Limpiar(dto.UbicacionGuia);
        }

        /// <summary>
        /// Valida la ficha ya normalizada contra la ruta y el usuario del token (respuesta 400).
        /// </summary>
        /// <param name="dto">Datos normalizados.</param>
        /// <param name="prodIdRuta">Prod_Id de la ruta.</param>
        /// <param name="usuarioToken">Usua_Id del token.</param>
        /// <returns>Errores por campo; vacío si los datos son válidos.</returns>
        public static Dictionary<string, string[]> Validar(FichaComplementariaItemDto dto, int prodIdRuta, long usuarioToken)
        {
            var errores = new Dictionary<string, string[]>();
            if (dto.Prod_Id.HasValue && dto.Prod_Id.Value != prodIdRuta)
                errores["Prod_Id"] = new[] { $"El item del cuerpo ({dto.Prod_Id}) no coincide con el de la ruta ({prodIdRuta})." };
            if (dto.Usua_Id.HasValue && dto.Usua_Id.Value != usuarioToken)
                errores["Usua_Id"] = new[] { "El usuario enviado no coincide con el usuario de la sesión." };
            if (dto.Embobinado_Id is int emb && (emb < EmbobinadoMinimo || emb > EmbobinadoMaximo))
                errores["Embobinado_Id"] = new[] { $"El tipo de embobinado debe estar entre {EmbobinadoMinimo} y {EmbobinadoMaximo}." };
            if (dto.CodigoBarras != null)
            {
                if (!dto.CodigoBarras.All(char.IsAsciiDigit))
                    errores["CodigoBarras"] = new[] { "El código de barras solo puede contener dígitos." };
                else if (dto.CodigoBarras.Length > LargoCodigoBarras)
                    errores["CodigoBarras"] = new[] { $"El código de barras no puede tener más de {LargoCodigoBarras} dígitos." };
            }
            if (dto.Repeticiones?.Length > LargoTextoCorto)
                errores["Repeticiones"] = new[] { $"Las repeticiones no pueden tener más de {LargoTextoCorto} caracteres." };
            if (dto.PasoGuias?.Length > LargoTextoCorto)
                errores["PasoGuias"] = new[] { $"El paso entre guías no puede tener más de {LargoTextoCorto} caracteres." };
            if (dto.UbicacionGuia?.Length > LargoUbicacionGuia)
                errores["UbicacionGuia"] = new[] { $"La ubicación de la guía no puede tener más de {LargoUbicacionGuia} caracteres." };
            return errores;
        }

        /// <summary>
        /// Aplica los datos editables del DTO a la entidad (no toca Prod_Id ni la auditoría).
        /// </summary>
        /// <param name="dto">Datos normalizados y validados.</param>
        /// <param name="ficha">Entidad nueva o existente.</param>
        public static void Aplicar(FichaComplementariaItemDto dto, Ficha_Complementaria_Item ficha)
        {
            ficha.Embobinado_Id = dto.Embobinado_Id;
            ficha.CodigoBarras = dto.CodigoBarras;
            ficha.Repeticiones = dto.Repeticiones;
            ficha.PasoGuias = dto.PasoGuias;
            ficha.UbicacionGuia = dto.UbicacionGuia;
            ficha.LargoContinuo = dto.LargoContinuo;
            ficha.MezclaAjustada = dto.MezclaAjustada;
        }

        /// <summary>
        /// Convierte la entidad en el DTO de respuesta.
        /// </summary>
        /// <param name="ficha">Ficha guardada.</param>
        /// <returns>DTO con todos los campos, incluida la auditoría.</returns>
        public static FichaComplementariaItemDto ADto(Ficha_Complementaria_Item ficha) => new()
        {
            Prod_Id = ficha.Prod_Id,
            Embobinado_Id = ficha.Embobinado_Id,
            CodigoBarras = ficha.CodigoBarras,
            Repeticiones = ficha.Repeticiones,
            PasoGuias = ficha.PasoGuias,
            UbicacionGuia = ficha.UbicacionGuia,
            LargoContinuo = ficha.LargoContinuo,
            MezclaAjustada = ficha.MezclaAjustada,
            Usua_Id = ficha.Usua_Id,
            Fecha_Creacion = ficha.Fecha_Creacion,
            Usua_Modifica = ficha.Usua_Modifica,
            Fecha_Modifica = ficha.Fecha_Modifica,
        };

        /// <summary>
        /// Recorta un texto y lo convierte en null si queda vacío.
        /// </summary>
        /// <param name="valor">Texto recibido.</param>
        /// <returns>El texto recortado, o null.</returns>
        private static string? Limpiar(string? valor) => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
    }
}
