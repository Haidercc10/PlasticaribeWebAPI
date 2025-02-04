using Microsoft.AspNetCore.Mvc;
using PlasticaribeAPI.Controllers;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Interfaces
{
    public interface IPlanillas_Despacho
    {
        Task<IActionResult> putHeaderSpreadSheet(int id, int old_Id, decimal totalValue, decimal totalCounting, decimal weight, [FromBody] List<long> codes);
    }
}
