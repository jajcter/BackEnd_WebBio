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
using BEUBIO.modelos_;
using BEUBIO.Transaction;

namespace ApiTienda.Controllers
{
    public class UsuariosController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        // GET: api/Articulos
        public IHttpActionResult Get()
        {
            try
            {
                List<Usuario> todos = UsuarioBLL.GetList();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Usuario result = UsuarioBLL.Get(id);
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


        // POST: api/Articulos
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Post(Usuario Usuario)
        {
            try
            {
                Console.Write("Hola Mundo");
                UsuarioBLL.Create(Usuario);
                return Content(HttpStatusCode.Created, "Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(Usuario usuario)
        {
            try
            {
                UsuarioBLL.Update(usuario);
                return Content(HttpStatusCode.OK, "Usuario actualizado correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UsuarioBLL.Delete(id);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [ResponseType(typeof(UsuarioLoginModel))]
        public IHttpActionResult Login(UsuarioLoginModel model)
        {
            try
            {
                if (model != null)
                {
                    SegurityViewModel token = new SegurityViewModel();
                    token = UsuarioBLL.Login(model.email, model.password);
                    if (token.token != null)
                    {
                        return Content(HttpStatusCode.OK, token);
                    }
                }
                return Content(HttpStatusCode.BadRequest, "Datos incorrectos");

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex);
                throw;
            }
        }//end

        [ResponseType(typeof(Usuario))]
        public IHttpActionResult LogOut(int id)
        {
            try
            {
                if (UsuarioBLL.LogOut(id))
                {
                    return Content(HttpStatusCode.OK, "Sesion cerrada satisfactoriamente");
                }
                return Content(HttpStatusCode.Conflict, "Error al eliminar el token");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }//end


    }
}