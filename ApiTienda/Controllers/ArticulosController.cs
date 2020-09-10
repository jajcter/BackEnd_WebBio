using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BEUBIO;
using BEUBIO.modelos_;
using BEUBIO.Transaction;

namespace ApiTienda.Controllers
{
    public class ArticulosController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        // GET: api/Articulos
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult Get(string criterio)
        {
            try
            {
                List<Articulo> todos = ArticuloBLL.GetList(criterio);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Articulo result = ArticuloBLL.Get(id);
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

        
        //public IHttpActionResult Gets(int id)
        //{
        //    try
        //    {
        //        List<Articulo> result = ArticuloBLL.Get_id(id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Content(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(HttpStatusCode.BadRequest, ex);
        //    }
        //}
        public IHttpActionResult GetL(int id,string criterio)
        {
            try
            {
                List<Articulo> result = ArticuloBLL.Get_id_Login(id,criterio);
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

        public IHttpActionResult ListId(int id,string criterio)
        {
            try
            {
                List<Articulo> result = ArticuloBLL.Get_id(id,criterio);
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

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpDate(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                if (ArticuloBLL.Update(articulo))
                {
                    return Content(HttpStatusCode.OK, "UpDate correcto");
                }
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return StatusCode(HttpStatusCode.NotFound);
        }





        // POST: api/Articulos
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult Post(Articulo articulo)
        {
            try
            {
                ArticuloBLL.Create(articulo);
                return Content(HttpStatusCode.Created, "Artículo creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(Articulo articulo)
        {
            try
            {
                ArticuloBLL.Update(articulo);
                return Content(HttpStatusCode.OK, "Usuario actualizado correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult Delete(SegurityViewModel token, int id)
        {
            try
            {
                return Content(HttpStatusCode.OK, ArticuloBLL.Delete(token, id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

       
    }
}