using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Items
{
    public class TarjetaDescuentoRepository
    {

        #region Fields & Properties

        private DsContext _context = new DsContext();

        public object TarjetaDescuentoActualizar { get; private set; }

        #endregion






        #region Manipulacion

        public void Agregar(TarjetaDescuento tarjetaId)
        {
            _context.TarjetaDescuento.Add(tarjetaId);
            _context.SaveChanges();

        }
        public void Editar(TarjetaDescuento tarjetaDescuento)
        {
            TarjetaDescuento tarjetaDescuentoActualizar = _context.TarjetaDescuento.SingleOrDefault(s => s.TarjetaDescuentoId == tarjetaDescuento.TarjetaDescuentoId);
            tarjetaDescuentoActualizar.Codigo = tarjetaDescuento.Codigo;
            tarjetaDescuentoActualizar.NroUsos = tarjetaDescuento.NroUsos;
            tarjetaDescuentoActualizar.Porcentaje = tarjetaDescuento.Porcentaje;
            tarjetaDescuentoActualizar.Costo = tarjetaDescuento.Costo;
            tarjetaDescuentoActualizar.Activado = tarjetaDescuento.Activado;

            _context.TarjetaDescuento.Attach(tarjetaDescuentoActualizar);
            _context.Entry(tarjetaDescuentoActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid tarjetaDescuentoId)
        {
            TarjetaDescuento tarjetaDescuento = ObtenerPorId(tarjetaDescuentoId);
           
            _context.TarjetaDescuento.Remove(tarjetaDescuento);
            _context.SaveChanges();
        }

        public void Vender(Guid tarjetaDescuentoId)
        {
            CajaBanco cajaBanco = _context.CajaBanco.FirstOrDefault(f => f.CajaBancoId == new Guid("11111111-2222-3333-4444-555555555555"));
            TarjetaDescuento tarjetaDescuentoActualizar = _context.TarjetaDescuento.SingleOrDefault(s => s.TarjetaDescuentoId == tarjetaDescuentoId);
            Transaccion _transaccion = _context.Transaccion.Where(w => w.Cuenta.Tipo == CajaBancoTipo.Caja).OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            decimal saldo = _transaccion == null ? 0 : _transaccion.Saldo;
            DateTime _dateNow = DateTime.Now;

            Transaccion transaccion = new Transaccion();
            transaccion.CajaBancoId = new Guid("11111111-2222-3333-4444-555555555555");
            transaccion.Tipo = TransaccionTipo.Deposito;
            transaccion.Fecha_Ini = _dateNow;
            transaccion.Fecha_Fin = _dateNow;
            transaccion.Fecha_Transaccion = _dateNow;
            transaccion.Concepto = "Venta de tarjeta de descuento: " + tarjetaDescuentoActualizar.Codigo;
            transaccion.Deposito = tarjetaDescuentoActualizar.Costo;
            transaccion.Retiro = 0;
            transaccion.Saldo = tarjetaDescuentoActualizar.Costo + saldo;

            _context.Transaccion.Add(transaccion);
            
            tarjetaDescuentoActualizar.TransaccionId = transaccion.TransaccionId;

            _context.TarjetaDescuento.Attach(tarjetaDescuentoActualizar);
            _context.Entry(tarjetaDescuentoActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion






        #region Consultas

        public IQueryable<TarjetaDescuento> ObtenerTodo()
        {
            return _context.TarjetaDescuento;

        }

        
        public TarjetaDescuento ObtenerPorId(Guid? TarjetaId)
        {
            return _context.TarjetaDescuento.SingleOrDefault(s => s.TarjetaDescuentoId == TarjetaId);
        }

        #endregion
    }
}
