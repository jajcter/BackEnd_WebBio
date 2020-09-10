using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEUBIO.modelos_;

namespace BEUBIO.Transaction
{
    public class ArticuloBLL
    {
        //BLL Bussiness Logic Layer
        //Capa de Logica del Negocio

        public static void Create(Articulo a)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Articuloes.Add(a);
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
        public static string Add(ArticuloViewModel modelo)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                if (!UsuarioBLL.Verify(modelo.token))//No autorizado
                {
                    return "No autorizado";
                }
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        Articulo a = new Articulo();
                        a.nombre = modelo.nombre;
                        a.precio = modelo.precio;
                        a.categoria = modelo.categoria;
                        a.detalle = modelo.detalle;
                        a.estado = modelo.estado;
                        a.idUsuario = modelo.idUsuario;
                        a.fecha_ingreso = modelo.fecha_ingreso;

                        a.imagen = modelo.imagen;




                        db.Articuloes.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                        return "Articulo creado exitosamente";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }//end Create

        public static Articulo Get(int? id)
        {
            Entities_Bio db = new Entities_Bio();
            return db.Articuloes.Find(id);
        }

        public static List<Articulo> Get_id(int? id)
        {
            Entities_Bio db = new Entities_Bio();
            return db.Articuloes.Where(x => x.idUsuario == id).ToList();
        }

        public static bool Update(Articulo articulo)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Articuloes.Attach(articulo);
                        db.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                        //throw ex;
                    }
                }
            }
        }

        public static string Delete(SegurityViewModel modelo, int id)
        {
            if (!UsuarioBLL.Verify(modelo.token))//No autorizado
            {
                return "No autorizado";
            }
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Articulo articulo = db.Articuloes.Find(id);
                        if (articulo == null)
                        {
                            return "Articulo no existe";
                        }
                        db.Entry(articulo).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                        return "Eliminación correcta";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }


        public static List<Articulo> GetList(string criterio)
        {
            Entities_Bio db = new Entities_Bio();            
            return db.Articuloes.Where(x => x.categoria.ToLower().Contains(criterio)).ToList();
            
        }

        public static List<Articulo> ListToNames()
        {
            Entities_Bio db = new Entities_Bio();
            List<Articulo> result = new List<Articulo>();
            db.Articuloes.ToList().ForEach(x =>
                result.Add(
                    new Articulo
                    {
                        nombre = x.nombre,
                        idArticulo = x.idArticulo
                    }));
            return result;
        }

        public static List<Articulo> GetArticulos(string criterio)
        {
            Entities_Bio db = new Entities_Bio();
            return db.Articuloes.Where(x => x.nombre.ToLower().Contains(criterio)).ToList();
        }
        
    }
}
