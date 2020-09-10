using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BEUBIO;
using BEUBIO.Transaction;

namespace ApiTienda.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class VentaController : ApiController
    {

        // GET: api/Venta
        public IHttpActionResult GetVentas(int id)
        {
            try
            {
                List<Venta> todos = VentaBLL.GetList(id);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Venta/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetVenta(int id)
        {
            try
            {
                Venta result = VentaBLL.Get(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Content(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetUltimaVenta()
        {
            try
            {
                int result = VentaBLL.Get();
                if (result == 0)
                {
                    return NotFound();
                }
                return Content(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Venta
        [ResponseType(typeof(Venta))]
        public IHttpActionResult Post(Venta venta)
        {
            try
            {
                VentaBLL.Create(venta);
                return Content(HttpStatusCode.Created, "Venta creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Venta/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                VentaBLL.Delete(id);
                return Ok("Venta eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}