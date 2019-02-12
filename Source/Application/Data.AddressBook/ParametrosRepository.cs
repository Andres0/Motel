using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
     public class ParametrosRepository
    {

        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion



        #region Manipulacion

        public void Agregar(Parametros parametros)
        {
            _context.Parametros.Add(parametros);
            _context.SaveChanges();

        }
        public void Editar(Parametros parametros)
        {
            Parametros parametrosActualizar = _context.Parametros.SingleOrDefault(m => m.ParametroId == parametros.ParametroId);
            parametrosActualizar.Nombre = parametros.Nombre;
            parametrosActualizar.Costo_Habitacion = parametros.Costo_Habitacion;
            parametrosActualizar.Costo_Adicional1 = parametros.Costo_Adicional1;
            parametrosActualizar.Costo_Adicional2 = parametros.Costo_Adicional2;
            parametrosActualizar.Tiempo_Hora = parametros.Tiempo_Hora;
            parametrosActualizar.Tiempo_Incremento = parametros.Tiempo_Incremento;
            parametrosActualizar.Costo_Tv = parametros.Costo_Tv;
            parametrosActualizar.Tolerancia = parametros.Tolerancia;
            parametrosActualizar.Tiempo_Anular = parametros.Tiempo_Anular;
            parametrosActualizar.Tiempo_Salir = parametros.Tiempo_Salir;
            parametrosActualizar.Tiempo_Limpieza = parametros.Tiempo_Limpieza;
            parametrosActualizar.Tiempo_Revision = parametros.Tiempo_Revision;
            parametrosActualizar.Numero_Inicio_Boleta = parametros.Numero_Inicio_Boleta;
            parametrosActualizar.Numero_Fin_Boleta = parametros.Numero_Fin_Boleta;
            parametrosActualizar.Fecha_Modificado = parametros.Fecha_Modificado;

            _context.Parametros.Attach(parametrosActualizar);
            _context.Entry(parametrosActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid ParametroID)
        {
            Parametros parametro = ObtenerPorId(ParametroID);
            _context.Parametros.Remove(parametro);
            _context.SaveChanges();
        }

        public void CambiarEstado(Guid parametroId)
        {
            
            Parametros parametrosActualizar = _context.Parametros.SingleOrDefault(m => m.ParametroId == parametroId);
            parametrosActualizar.Activado = !parametrosActualizar.Activado;
           


            _context.Parametros.Attach(parametrosActualizar);
            _context.Entry(parametrosActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion



        #region Consultas

        public IQueryable<Parametros> ObtenerTodo()
        {
            return _context.Parametros;

        }
        public Parametros ObtenerPorId(Guid? ParametroID)
        {
            return _context.Parametros.SingleOrDefault(s => s.ParametroId == ParametroID);
        }

       

        #endregion

    }
}
