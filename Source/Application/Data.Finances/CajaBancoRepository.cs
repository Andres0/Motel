using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Finances
{
    public class CajaBancoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(CajaBanco cajaBanco)
        {
            _context.CajaBanco.Add(cajaBanco);
            _context.SaveChanges();

        }
        public void Editar(CajaBanco cajaBanco)
        {
            CajaBanco cajaBancoaAActualizar = _context.CajaBanco.SingleOrDefault(s => s.CajaBancoId == cajaBanco.CajaBancoId);
            cajaBancoaAActualizar.Nombre = cajaBanco.Nombre;
            cajaBancoaAActualizar.Descripcion = cajaBanco.Descripcion;

            _context.CajaBanco.Attach(cajaBancoaAActualizar);
            _context.Entry(cajaBancoaAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid cajaBancoId)
        {
            CajaBanco cajaBanco = ObtenerPorId(cajaBancoId);
            _context.CajaBanco.Remove(cajaBanco);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<CajaBanco> ObtenerTodo()
        {
            return _context.CajaBanco;

        }
        public CajaBanco ObtenerPorId(Guid? cajaBancoId)
        {
            return _context.CajaBanco.SingleOrDefault(s => s.CajaBancoId == cajaBancoId);
        }

        #endregion
    }
}
