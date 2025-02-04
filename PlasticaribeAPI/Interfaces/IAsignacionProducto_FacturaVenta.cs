using Microsoft.AspNetCore.Mvc;

namespace PlasticaribeAPI.Interfaces
{
    public interface IAsignacionProducto_FacturaVenta
    {
        Task<IActionResult> putMovementsDispatch(int id, bool spreadsheet, [FromBody] List<long> codes);
    }
}
