using System;
using System.Collections.Generic;
using System.Linq;
using BEUBIO.modelos_;
using System.Text;
using System.Threading.Tasks;

namespace BEUBIO.Transaction
{
    public class UsuarioBLL
    {
        //BLL Bussiness Logic Layer
        //Capa de Logica del Negocio

        public static void Create(Usuario a)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Usuarios.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static Usuario Get(int? id)
        {
            Entities_Bio db = new Entities_Bio();
            return db.Usuarios.Find(id);
        }

        public static void Update(Usuario Usuario)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Usuarios.Attach(Usuario);
                        db.Entry(Usuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static void Delete(int? id)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Usuario usuario = db.Usuarios.Find(id);
                        var articulos = usuario.Articuloes.Where(t => t.idUsuario == id);
                        Venta venta = db.Ventas.Find(usuario.idUsuario);
                        var ventas = usuario.Ventas.Where(t => t.idUsuario == id);
                        if (articulos != null)
                        {
                            db.Articuloes.RemoveRange(articulos);
                        }
                        if (venta != null)
                        {
                            db.Ventas.RemoveRange(ventas);
                        }
                        
                        db.Entry(usuario).State = System.Data.Entity.EntityState.Deleted;
                        
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        
        public static List<Usuario> GetList()
        {
            Entities_Bio db = new Entities_Bio();
            return db.Usuarios.ToList();
        }

        public static List<Usuario> ListToNames()
        {
            Entities_Bio db = new Entities_Bio();
            List<Usuario> result = new List<Usuario>();
            db.Usuarios.ToList().ForEach(x =>
                result.Add(
                    new Usuario
                    {
                        nombres = x.nombres,
                        idUsuario = x.idUsuario
                    }));
            return result;
        }

        public static List<Usuario> GetUsuario(string criterio)
        {
            
            Entities_Bio db = new Entities_Bio();
            return db.Usuarios.Where(x => x.nombres.ToLower().Contains(criterio)).ToList();
        }

        public static SegurityViewModel Login(string email, string password)
        {
            //se crea una instancia o un iniverso dentro de otro universo
            //esta instancia se termina aqui
            using (Entities_Bio db = new Entities_Bio())
            {
                SegurityViewModel token = new SegurityViewModel();
                try
                {
                    var lst = db.Usuarios.Where(d => d.email == email && d.contrasena == password);
                    //var lst = db.Usuarios.Where(d => d.email == email && d.contrasena == email && d.estado == "a");
                    if (lst.Count() > 0)
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            Usuario userLogin = lst.First();
                            if (userLogin.token_temp == null)
                                userLogin.token_temp = Guid.NewGuid().ToString();
                            db.Usuarios.Attach(userLogin);
                            db.Entry(userLogin).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            transaction.Commit();

                            token.token = userLogin.token_temp;
                            token.id_logueado = userLogin.idUsuario;
                            token.nombre = userLogin.nombres + " " + userLogin.apellidos;
                            //token.pathIMG = userLogin.imgPath;
                            return token;
                        }
                    }
                    else
                        return token;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }//end

        public static bool LogOut(int? id)
        {
            //se crea una instancia o un iniverso dentro de otro universo
            //esta instancia se termina aqui
            using (Entities_Bio db = new Entities_Bio())
            {
                try
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        Usuario userLogin = db.Usuarios.Find(id);
                        userLogin.token_temp = null;
                        db.Usuarios.Attach(userLogin);
                        db.Entry(userLogin).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }//end
        public static bool Verify(string token)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                try
                {
                    if (db.Usuarios.Where(x => x.token_temp.Equals(token)).Count() > 0)
                    {
                        return true;//existe
                    }
                    else
                        return false;//no exite

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }//end
    }
}
