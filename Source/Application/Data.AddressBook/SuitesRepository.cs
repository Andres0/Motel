using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class SuitesRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Suite suite)
        {
            _context.Suite.Add(suite);
            _context.SaveChanges();

        }
        public void Editar(Suite suite)
        {
            Suite suiteActualizar = _context.Suite.SingleOrDefault(s => s.SuiteId == suite.SuiteId);
            suiteActualizar.Nombre = suite.Nombre;
            suiteActualizar.Estado = suite.Estado;
            suiteActualizar.Tipo = suite.Tipo;
            suiteActualizar.ParametroId = suite.ParametroId;

            _context.Suite.Attach(suiteActualizar);
            _context.Entry(suiteActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid suiteId)
        {
            Suite suite = ObtenerPorId(suiteId);
            _context.Suite.Remove(suite);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Suite> ObtenerTodo()
        {
            return _context.Suite;

        }
        public Suite ObtenerPorId(Guid? SuiteId)
        {
            return _context.Suite.SingleOrDefault(s => s.SuiteId == SuiteId);
        }

        #endregion
    }
}
