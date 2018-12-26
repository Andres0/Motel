using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Finances
{
    public class TransaccionRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion



        #region Manipulacion

        public void Agregar(Transaccion transaccion)
        {
            _context.Transaccion.Add(transaccion);
            _context.SaveChanges();

        }
        public void Editar(Transaccion transaccion)
        {
            _context.Transaccion.Attach(transaccion);
            _context.Entry(transaccion).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid transaccionId)
        {
            Transaccion transaccion = ObtenerPorId(transaccionId);
            _context.Transaccion.Remove(transaccion);
            _context.SaveChanges();
        }
        #endregion



        #region Consultas

        public IQueryable<Transaccion> ObtenerTodo()
        {
            return _context.Transaccion;

        }
        public Transaccion ObtenerPorId(Guid? TransaccionId)
        {
            return _context.Transaccion.SingleOrDefault(s => s.TransaccionId == TransaccionId);
        }

        #endregion
    }
}
