using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Finances
{
    public class Cuenta_BancoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Cuenta_Banco cuenta_Banco)
        {
            _context.Cuenta_Banco.Add(cuenta_Banco);
            _context.SaveChanges();

        }
        public void Editar(Cuenta_Banco cuenta_Banco)
        {
            Cuenta_Banco cuentaaActualizar = _context.Cuenta_Banco.SingleOrDefault(s => s.CuentaId == cuenta_Banco.CuentaId);
            cuentaaActualizar.Numero = cuenta_Banco.Numero;
            cuentaaActualizar.Titular = cuenta_Banco.Titular;
            cuentaaActualizar.Banco = cuenta_Banco.Banco;

            _context.Cuenta_Banco.Attach(cuentaaActualizar);
            _context.Entry(cuentaaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid CuentaId)
        {
            Cuenta_Banco cuenta_Banco = ObtenerPorId(CuentaId);
            _context.Cuenta_Banco.Remove(cuenta_Banco);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Cuenta_Banco> ObtenerTodo()
        {
            return _context.Cuenta_Banco;

        }
        public Cuenta_Banco ObtenerPorId(Guid? cuentaId)
        {
            return _context.Cuenta_Banco.SingleOrDefault(s => s.CuentaId == cuentaId);
        }

        #endregion
    }
}
