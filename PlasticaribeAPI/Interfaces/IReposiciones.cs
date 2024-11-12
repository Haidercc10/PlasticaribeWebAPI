using Microsoft.AspNetCore.Mvc;
using PlasticaribeAPI.Controllers;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Interfaces
{
    public interface IReposiciones
    {
        Task<IActionResult> putRepositionAnulled(long id, long user);
    }
}
