using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class InventarioInicialXDia_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public InventarioInicialXDia_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/InventarioInicialXDia_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioInicialXDia_MatPrima>>> GetInventarioInicialXDias_MatPrima()
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return NotFound();
            }
            return await _context.InventarioInicialXDias_MatPrima.ToListAsync();
        }

        // GET: api/InventarioInicialXDia_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> GetInventarioInicialXDia_MatPrima(long id)
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return NotFound();
            }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);

            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            return inventarioInicialXDia_MatPrima;
        }

        [HttpGet("get_Cantidad_Material_Meses")]
        public ActionResult Get_Cantidad_Material_Meses()
        {
            var enero = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Enero * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Enero * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 1 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var febrero = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                           join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                           select inv.Febrero * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                           join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                           select inv.Febrero * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                          where bp.BOPP_FechaIngreso.Month == 2 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                          select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var marzo = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Marzo * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Marzo * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 3 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var abril = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Abril * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Abril * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 4 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var mayo = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                        join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                        select inv.Mayo * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                     join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                     select inv.Mayo * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                 where bp.BOPP_FechaIngreso.Month == 5 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                 select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var junio = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Junio * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Junio * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 6 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var julio = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                         join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                         select inv.Julio * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                       join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                       select inv.Julio * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                    where bp.BOPP_FechaIngreso.Month == 7 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                    select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var agosto = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                          join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                          select inv.Agosto * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                         join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                         select inv.Agosto * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                       where bp.BOPP_FechaIngreso.Month == 8 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                       select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var septiembre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                              join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                              select inv.Septiembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                                 join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                                 select inv.Septiembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                                   where bp.BOPP_FechaIngreso.Month == 9 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                                   select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var octubre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                           join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                           select inv.Octubre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                           join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                           select inv.Octubre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                          where bp.BOPP_FechaIngreso.Month == 10 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                          select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var novimebre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                             join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                             select inv.Noviembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                               join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                               select inv.Noviembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                                where bp.BOPP_FechaIngreso.Month == 11 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                                select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var diciembre = (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                             join mp in _context.Set<Materia_Prima>() on inv.MatPri_Id equals mp.MatPri_Id
                             select inv.Diciembre * mp.MatPri_Precio).Sum() + (from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                                                               join tt in _context.Set<Tinta>() on inv.MatPri_Id equals tt.Tinta_Id
                                                                               select inv.Diciembre * tt.Tinta_Precio).Sum() + (from bp in _context.Set<BOPP>()
                                                                                                                                where bp.BOPP_FechaIngreso.Month == 12 && bp.BOPP_FechaIngreso.Year == DateTime.Today.Year
                                                                                                                                select bp.BOPP_Precio * bp.BOPP_CantidadInicialKg).Sum();

            var result = new List<object>();
            result.Add($"'Enero': '{enero}'," +
                      $"'Febrero': '{febrero}'," +
                      $"'Marzo': '{marzo}'," +
                      $"'Abril': '{abril}'," +
                      $"'Mayo': '{mayo}'," +
                      $"'Junio': '{junio}'," +
                      $"'Julio': '{julio}'," +
                      $"'Agosto': '{agosto}'," +
                      $"'Septiembre': '{septiembre}'," +
                      $"'Octubre': '{octubre}'," +
                      $"'Noviembre': '{novimebre}'," +
                      $"'Diciembre': '{diciembre}'");

            return Ok(result);
        }

        // Consulta que devolverá el costo en cada uno de los meses del inventario de polietilenos, tintas 
        [HttpGet("getCostoInventarioMateriasPrimas")]
        public ActionResult GetCostoInventarioMateriasPrimas()
        {
            int anio = DateTime.Now.Year;

            var polietilenos = from i in _context.Set<InventarioInicialXDia_MatPrima>()
                               join mp in _context.Set<Materia_Prima>() on i.MatPri_Id equals mp.MatPri_Id
                               select new
                               {
                                   Enero = i.Enero * mp.MatPri_Precio,
                                   Febrero = i.Febrero * mp.MatPri_Precio,
                                   Marzo = i.Marzo * mp.MatPri_Precio,
                                   Abril = i.Abril * mp.MatPri_Precio,
                                   Mayo = i.Mayo * mp.MatPri_Precio,
                                   Junio = i.Junio * mp.MatPri_Precio,
                                   Julio = i.Julio * mp.MatPri_Precio,
                                   Agosto = i.Agosto * mp.MatPri_Precio,
                                   Septiembre = i.Septiembre * mp.MatPri_Precio,
                                   Octubre = i.Octubre * mp.MatPri_Precio,
                                   Noviembre = i.Noviembre * mp.MatPri_Precio,
                                   Diciembre = i.Diciembre * mp.MatPri_Precio,
                               };

            var tintas = from i in _context.Set<InventarioInicialXDia_MatPrima>()
                         join t in _context.Set<Tinta>() on i.Tinta_Id equals t.Tinta_Id
                         select new
                         {
                             Enero = i.Enero * t.Tinta_Precio,
                             Febrero = i.Febrero * t.Tinta_Precio,
                             Marzo = i.Marzo * t.Tinta_Precio,
                             Abril = i.Abril * t.Tinta_Precio,
                             Mayo = i.Mayo * t.Tinta_Precio,
                             Junio = i.Junio * t.Tinta_Precio,
                             Julio = i.Julio * t.Tinta_Precio,
                             Agosto = i.Agosto * t.Tinta_Precio,
                             Septiembre = i.Septiembre * t.Tinta_Precio,
                             Octubre = i.Octubre * t.Tinta_Precio,
                             Noviembre = i.Noviembre * t.Tinta_Precio,
                             Diciembre = i.Diciembre * t.Tinta_Precio,
                         };

            var result = new List<object>();

            string datosPolietilenos = $"'Nombre': 'Polietilenos'," +
                    $"'Anio': '{anio}'," +
                    $"'Enero': '{polietilenos.Sum(x => x.Enero)}', " +
                    $"'Febrero': '{polietilenos.Sum(x => x.Febrero)}'," +
                    $"'Marzo': '{polietilenos.Sum(x => x.Marzo)}', " +
                    $"'Abril': '{polietilenos.Sum(x => x.Abril)}', " +
                    $"'Mayo': '{polietilenos.Sum(x => x.Mayo)}', " +
                    $"'Junio': '{polietilenos.Sum(x => x.Junio)}', " +
                    $"'Julio': '{polietilenos.Sum(x => x.Julio)}', " +
                    $"'Agosto': '{polietilenos.Sum(x => x.Agosto)}', " +
                    $"'Septiembre': '{polietilenos.Sum(x => x.Septiembre)}', " +
                    $"'Octubre': '{polietilenos.Sum(x => x.Octubre)}', " +
                    $"'Noviembre': '{polietilenos.Sum(x => x.Noviembre)}', " +
                    $"'Diciembre': '{polietilenos.Sum(x => x.Diciembre)}'";

            string datosTintas = $"'Nombre': 'Tintas'," +
                    $"'Anio': '{anio}'," +
                    $"'Enero': '{tintas.Sum(x => x.Enero)}', " +
                    $"'Febrero': '{tintas.Sum(x => x.Febrero)}'," +
                    $"'Marzo': '{tintas.Sum(x => x.Marzo)}', " +
                    $"'Abril': '{tintas.Sum(x => x.Abril)}', " +
                    $"'Mayo': '{tintas.Sum(x => x.Mayo)}', " +
                    $"'Junio': '{tintas.Sum(x => x.Junio)}', " +
                    $"'Julio': '{tintas.Sum(x => x.Julio)}', " +
                    $"'Agosto': '{tintas.Sum(x => x.Agosto)}', " +
                    $"'Septiembre': '{tintas.Sum(x => x.Septiembre)}', " +
                    $"'Octubre': '{tintas.Sum(x => x.Octubre)}', " +
                    $"'Noviembre': '{tintas.Sum(x => x.Noviembre)}', " +
                    $"'Diciembre': '{tintas.Sum(x => x.Diciembre)}'";

            /*string datosBiorientado = $"'Nombre': 'Biorientado'," +
                    $"'Enero': '{GetCostoInventarioBiorientado(1, 2023)}', " +
                    $"'Febrero': '{GetCostoInventarioBiorientado(2, 2023)}'," +
                    $"'Marzo': '{GetCostoInventarioBiorientado(3, 2023)}', " +
                    $"'Abril': '{GetCostoInventarioBiorientado(4, 2023)}', " +
                    $"'Mayo': '{GetCostoInventarioBiorientado(5, 2023)}', " +
                    $"'Junio': '{GetCostoInventarioBiorientado(6, 2023)}', " +
                    $"'Julio': '{GetCostoInventarioBiorientado(7, 2023)}', " +
                    $"'Agosto': '{GetCostoInventarioBiorientado(8, 2023)}', " +
                    $"'Septiembre': '{GetCostoInventarioBiorientado(9, 2023)}', " +
                    $"'Octubre': '{GetCostoInventarioBiorientado(10, 2023)}', " +
                    $"'Noviembre': '{GetCostoInventarioBiorientado(11, 2023)}', " +
                    $"'Diciembre': '{GetCostoInventarioBiorientado(12, 2023)}'";*/

            result.Add(datosPolietilenos);
            result.Add(datosTintas);
            //result.Add(datosBiorientado);

            return Ok(result);
        }

        //Consulta que devolverá el costo en cada uno de los meses del inventario de biorientados
        [HttpGet("getCostoInventarioBiorientado/{mes}/{anio}")]
        public ActionResult GetCostoInventarioBiorientado(int mes, int anio)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var biorientado = (from b in _context.Set<BOPP>()
                               where b.BOPP_FechaIngreso.Year <= anio &&
                                     (b.BOPP_FechaIngreso.Year == anio ? b.BOPP_FechaIngreso.Month <= mes : true)
                               orderby b.BOPP_Serial descending
                               select b.BOPP_Precio * b.BOPP_CantidadInicialKg).Sum();

            var asignado = (from asg in _context.Set<DetalleAsignacion_BOPP>()
                            join b in _context.Set<BOPP>() on asg.BOPP_Id equals b.BOPP_Id
                            where asg.AsigBOPP.AsigBOPP_FechaEntrega.Year <= anio &&
                                  (asg.AsigBOPP.AsigBOPP_FechaEntrega.Year == anio ? asg.AsigBOPP.AsigBOPP_FechaEntrega.Month <= mes : true)
                            select (asg.DtAsigBOPP_Cantidad * b.BOPP_Precio)).Sum();

            var devuelto = (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                            join b in _context.Set<BOPP>() on dev.BOPP_Id equals b.BOPP_Id
                            where dev.DevMatPri.DevMatPri_Fecha.Year <= anio &&
                                  (dev.DevMatPri.DevMatPri_Fecha.Year == anio ? dev.DevMatPri.DevMatPri_Fecha.Month <= mes : true)
                            select (dev.DtDevMatPri_CantidadDevuelta * b.BOPP_Precio)).Sum();

            return Ok(biorientado - asignado + devuelto);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("getMatPrimasInicioMes/{fechaFin}")]
        public ActionResult GetMatPrimasInicioMes(DateTime fechaFin)
        {
            var mp = from i in _context.Set<InventarioInicialXDia_MatPrima>()
                     join m in _context.Set<Materia_Prima>()
                     on i.MatPri_Id equals m.MatPri_Id
                     where i.MatPri_Id != 0
                     select new {
                         Fecha_Inventario = new DateTime(fechaFin.Year, fechaFin.Month, 1),
                         Ot = "",
                         Item = i.MatPri_Id,
                         Referencia = m.MatPri_Nombre,
                         Stock = fechaFin.Month == 1 ? i.Enero :
                                 fechaFin.Month == 2 ? i.Febrero :
                                 fechaFin.Month == 3 ? i.Marzo :
                                 fechaFin.Month == 4 ? i.Abril :
                                 fechaFin.Month == 5 ? i.Mayo :
                                 fechaFin.Month == 6 ? i.Junio :
                                 fechaFin.Month == 7 ? i.Julio :
                                 fechaFin.Month == 8 ? i.Agosto :
                                 fechaFin.Month == 9 ? i.Septiembre :
                                 fechaFin.Month == 10 ? i.Octubre :
                                 fechaFin.Month == 11 ? i.Noviembre :
                                 fechaFin.Month == 13 ? i.Diciembre : i.Enero,
                         Precio = m.MatPri_Precio,
                         Subtotal = (fechaFin.Month == 1 ? i.Enero :
                                    fechaFin.Month == 2 ? i.Febrero :
                                    fechaFin.Month == 3 ? i.Marzo :
                                    fechaFin.Month == 4 ? i.Abril :
                                    fechaFin.Month == 5 ? i.Mayo :
                                    fechaFin.Month == 6 ? i.Junio :
                                    fechaFin.Month == 7 ? i.Julio :
                                    fechaFin.Month == 8 ? i.Agosto :
                                    fechaFin.Month == 9 ? i.Septiembre :
                                    fechaFin.Month == 10 ? i.Octubre :
                                    fechaFin.Month == 11 ? i.Noviembre :
                                    fechaFin.Month == 13 ? i.Diciembre : i.Enero) * m.MatPri_Precio,
                        IdCategoria = m.CatMP_Id,
                     };

            var tinta = from i in _context.Set<InventarioInicialXDia_MatPrima>()
                     join t in _context.Set<Tinta>()
                     on i.Tinta_Id equals t.Tinta_Id
                     where i.Tinta_Id != 0
                     select new
                     {
                         Fecha_Inventario = new DateTime(fechaFin.Year, fechaFin.Month, 1),
                         Ot = "",
                         Item = i.Tinta_Id,
                         Referencia = t.Tinta_Nombre,
                         Stock = fechaFin.Month == 1 ? i.Enero :
                                 fechaFin.Month == 2 ? i.Febrero :
                                 fechaFin.Month == 3 ? i.Marzo :
                                 fechaFin.Month == 4 ? i.Abril :
                                 fechaFin.Month == 5 ? i.Mayo :
                                 fechaFin.Month == 6 ? i.Junio :
                                 fechaFin.Month == 7 ? i.Julio :
                                 fechaFin.Month == 8 ? i.Agosto :
                                 fechaFin.Month == 9 ? i.Septiembre :
                                 fechaFin.Month == 10 ? i.Octubre :
                                 fechaFin.Month == 11 ? i.Noviembre :
                                 fechaFin.Month == 13 ? i.Diciembre : i.Enero,
                         Precio = t.Tinta_Precio,
                         Subtotal = (fechaFin.Month == 1 ? i.Enero :
                                    fechaFin.Month == 2 ? i.Febrero :
                                    fechaFin.Month == 3 ? i.Marzo :
                                    fechaFin.Month == 4 ? i.Abril :
                                    fechaFin.Month == 5 ? i.Mayo :
                                    fechaFin.Month == 6 ? i.Junio :
                                    fechaFin.Month == 7 ? i.Julio :
                                    fechaFin.Month == 8 ? i.Agosto :
                                    fechaFin.Month == 9 ? i.Septiembre :
                                    fechaFin.Month == 10 ? i.Octubre :
                                    fechaFin.Month == 11 ? i.Noviembre :
                                    fechaFin.Month == 13 ? i.Diciembre : i.Enero) * t.Tinta_Precio,
                         IdCategoria = t.CatMP_Id,
                     };

            if (mp == null && tinta == null) return BadRequest("No se encontraron Materias Primas/Reciclados en las fechas consultadas");
            else return Ok(mp.Concat(tinta));
        }

        // PUT: api/InventarioInicialXDia_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarioInicialXDia_MatPrima(long id, InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
            if (id != inventarioInicialXDia_MatPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(inventarioInicialXDia_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioInicialXDia_MatPrimaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InventarioInicialXDia_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventarioInicialXDia_MatPrima>> PostInventarioInicialXDia_MatPrima(InventarioInicialXDia_MatPrima inventarioInicialXDia_MatPrima)
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return Problem("Entity set 'dataContext.InventarioInicialXDias_MatPrima'  is null.");
            }
            _context.InventarioInicialXDias_MatPrima.Add(inventarioInicialXDia_MatPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventarioInicialXDia_MatPrimaExists(inventarioInicialXDia_MatPrima.MatPri_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventarioInicialXDia_MatPrima", new { id = inventarioInicialXDia_MatPrima.MatPri_Id }, inventarioInicialXDia_MatPrima);
        }

        // DELETE: api/InventarioInicialXDia_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarioInicialXDia_MatPrima(long id)
        {
            if (_context.InventarioInicialXDias_MatPrima == null)
            {
                return NotFound();
            }
            var inventarioInicialXDia_MatPrima = await _context.InventarioInicialXDias_MatPrima.FindAsync(id);
            if (inventarioInicialXDia_MatPrima == null)
            {
                return NotFound();
            }

            _context.InventarioInicialXDias_MatPrima.Remove(inventarioInicialXDia_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioInicialXDia_MatPrimaExists(long id)
        {
            return (_context.InventarioInicialXDias_MatPrima?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
