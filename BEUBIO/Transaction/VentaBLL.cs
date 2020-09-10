using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUBIO.Transaction
{
    public class VentaBLL
    {
        //BLL Bussiness Logic Layer
        //Capa de Logica del Negocio

        public static void Create(Venta a)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //DateTime thisDay = DateTime.Today;
                        a.fechaVenta = DateTime.Today;
                        db.Ventas.Add(a);
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

        public static int Get()
        {
            Entities_Bio db = new Entities_Bio();
            int value = int.Parse(db.Ventas
                               .OrderByDescending(p => p.idVenta)
                               .Select(r => r.idVenta)
                               .First().ToString());
            return value;
            //return db.Negocios.LastOrDefault();
        }

        public static Venta Get(int? id)
        {
            Entities_Bio db = new Entities_Bio();
            return db.Ventas.Find(id);
        }

        public static void Update(Venta Venta)
        {
            using (Entities_Bio db = new Entities_Bio())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Ventas.Attach(Venta);
                        db.Entry(Venta).State = System.Data.Entity.EntityState.Modified;
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
                        Venta venta = db.Ventas.Find(id);
                        db.Entry(venta).State = System.Data.Entity.EntityState.Deleted;
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

        

        public static List<Venta> GetList(int id)
        {
            Entities_Bio db = new Entities_Bio();
           
            return db.Ventas.Where(x=>x.idUsuario==id).ToList();

        }

        public static List<Venta> ListToNames()
        {
            Entities_Bio db = new Entities_Bio();
            List<Venta> result = new List<Venta>();
            db.Ventas.ToList().ForEach(x =>
                result.Add(
                    new Venta
                    {
                        idUsuario = x.idUsuario
                    }));
            return result;
        }

        
    }
}
