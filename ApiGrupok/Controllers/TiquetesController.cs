
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiGrupok.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TiquetesController : ApiController
    {
        private GrupoKEntities db = new GrupoKEntities();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Tiquetes/Nuevo")]
        public Respuesta NuevoTiqute([FromBody] Tiquetes_prof tk)
        {
            Respuesta R = new Respuesta();
            try
            {
                tk.Fecha = DateTime.Now;
                tk.Estado = "PEN";
                db.Tiquetes_prof.Add(tk);
                db.SaveChanges();
                R.Codigo = tk.Codigo_tiquete;
                R.Mensaje = "Tiquete guardado exitosamente, código: " + tk.Codigo_tiquete.ToString();
            }
            catch (Exception Er)
            {
                R.Codigo = -1;
                R.Mensaje = "Error: " + Er.Message;
            }
            return R;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Tiquetes/ListaClientes")]
        public object ListaClientes()
        {
            var R = from Cls in db.Clientes select new { Cls.codigo_cliente, Cls.nombre };
            return R;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Tiquetes/ListaUsuarios")]
        public object ListaUsuarios()
        {
            var R = from Us in db.Usuarios select new { Us.Usuario, Us.Nombre };
            return R;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Tiquetes/CasosPendiente")]
        public object CasosPendiente(DateTime Inicio, DateTime Fin)
        {
            return db.Reporte_Estados(Inicio, Fin);
        }
    }

    public class Respuesta
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
