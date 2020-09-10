using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUBIO.modelos_
{
    public class ArticuloViewModel : SegurityViewModel
    {
        public int idArticulo { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public string categoria { get; set; }
        public string detalle { get; set; }
        public string estado { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public byte[] imagen { get; set; }
    }
}
