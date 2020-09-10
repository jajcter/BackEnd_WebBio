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
    [EnableCors(origins: "https://localhost:4200/", headers: "*", methods: "*")]

    public class NegociosController : ApiController
    {
        //private Entities_Bio db = new Entities_Bio();

        // GET: api/Negocios
        public IHttpActionResult Get()
        {
            try
            {
                List<Negocio> todos = NegocioBLL.GetList();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Negocios/5
        [ResponseType(typeof(Negocio))]
        public IHttpActionResult GetNegocio(int id)
        {
            try
            {
                Negocio result = NegocioBLL.Get(id);
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

        // POST: api/Negocios
        [ResponseType(typeof(Negocio))]
        public IHttpActionResult Post(Negocio negocio)
        {
            try
            {
                NegocioBLL.Create(negocio);
                return Content(HttpStatusCode.Created, "Artículo creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Negocios/5
        [ResponseType(typeof(Negocio))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                NegocioBLL.Delete(id);
                return Ok("Negocio eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}