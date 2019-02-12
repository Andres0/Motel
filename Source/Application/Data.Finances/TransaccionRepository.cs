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
            CajaBanco cajaBanco = _context.CajaBanco.FirstOrDefault(f => f.CajaBancoId == transaccion.CajaBancoId);

            Transaccion _transaccion = _context.Transaccion.Where(w => w.Cuenta.Tipo == cajaBanco.Tipo).OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            if (_transaccion == null)
                transaccion.Saldo = transaccion.Deposito - transaccion.Retiro;
            else
                transaccion.Saldo = _transaccion.Saldo + (transaccion.Deposito - transaccion.Retiro);

            _context.Transaccion.Add(transaccion);
            _context.SaveChanges();
        }
        public void Editar(Transaccion transaccion)
        {
            Transaccion transaccionAActualizar = _context.Transaccion.SingleOrDefault(s => s.TransaccionId == transaccion.TransaccionId);
            DateTime fechaRegistro = transaccionAActualizar.Fecha_Transaccion;

            Transaccion _anterior = _context.Transaccion.Where(w => w.Fecha_Transaccion < fechaRegistro && w.Cuenta.Tipo == transaccionAActualizar.Cuenta.Tipo)
                .OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            decimal saldoAnterior = _anterior != null ? _anterior.Saldo : 0;

            foreach (Transaccion _transaccion in _context.Transaccion.Where(w => w.Fecha_Transaccion > fechaRegistro && w.Cuenta.Tipo == transaccionAActualizar.Cuenta.Tipo)
                .OrderBy(o => o.Fecha_Transaccion))
            {
                _transaccion.Saldo = saldoAnterior + _transaccion.Deposito - _transaccion.Retiro;
                saldoAnterior = _transaccion.Saldo;

                _context.Transaccion.Attach(_transaccion);
                _context.Entry(_transaccion).State = System.Data.Entity.EntityState.Modified;
            }

            transaccionAActualizar.CajaBancoId = transaccion.CajaBancoId;
            transaccionAActualizar.Tipo = transaccion.Tipo;
            transaccionAActualizar.Fecha_Ini = transaccion.Fecha_Ini;
            transaccionAActualizar.Fecha_Fin = transaccion.Fecha_Fin;
            transaccionAActualizar.Fecha_Transaccion = transaccion.Fecha_Transaccion;
            transaccionAActualizar.Concepto = transaccion.Concepto;
            transaccionAActualizar.Deposito = transaccion.Deposito;
            transaccionAActualizar.Retiro = transaccion.Retiro;
            transaccionAActualizar.Saldo = saldoAnterior + transaccion.Deposito - transaccion.Retiro;

            _context.Transaccion.Attach(transaccionAActualizar);
            _context.Entry(transaccionAActualizar).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }
        public void Eliminar(Guid transaccionId)
        {
            Transaccion transaccion = ObtenerPorId(transaccionId);

            Transaccion _anterior = _context.Transaccion.Where(w => w.Fecha_Transaccion < transaccion.Fecha_Transaccion && w.Cuenta.Tipo == transaccion.Cuenta.Tipo)
                .OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            decimal saldoAnterior = _anterior != null ? _anterior.Saldo : 0;

            foreach (Transaccion _transaccion in _context.Transaccion.Where(w => w.Fecha_Transaccion > transaccion.Fecha_Transaccion && w.Cuenta.Tipo == transaccion.Cuenta.Tipo)
                .OrderBy(o => o.Fecha_Transaccion))
            {
                _transaccion.Saldo = saldoAnterior + _transaccion.Deposito - _transaccion.Retiro;
                saldoAnterior = _transaccion.Saldo;

                _context.Transaccion.Attach(_transaccion);
                _context.Entry(_transaccion).State = System.Data.Entity.EntityState.Modified;
            }

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
