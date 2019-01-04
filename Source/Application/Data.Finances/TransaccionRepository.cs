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
            Transaccion transaccionAActualizar = _context.Transaccion.SingleOrDefault(s => s.TransaccionId == transaccion.TransaccionId);
            transaccionAActualizar.CajaBancoId = transaccion.CajaBancoId;
            transaccionAActualizar.Tipo = transaccion.Tipo;
            transaccionAActualizar.Fecha_Ini = transaccion.Fecha_Ini;
            transaccionAActualizar.Fecha_Fin = transaccion.Fecha_Fin;
            transaccionAActualizar.Fecha_Transaccion = transaccion.Fecha_Transaccion;
            transaccionAActualizar.Concepto = transaccion.Concepto;
            transaccionAActualizar.Deposito = transaccion.Deposito;
            transaccionAActualizar.Retiro = transaccion.Retiro;

            _context.Transaccion.Attach(transaccionAActualizar);
            _context.Entry(transaccionAActualizar).State = System.Data.Entity.EntityState.Modified;
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
