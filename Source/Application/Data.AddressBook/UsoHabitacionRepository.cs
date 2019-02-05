using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class UsoHabitacionRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion



        #region Manipulacion

        public void Agregar(UsoHabitacion usoHabitacion)
        {
            _context.UsoHabitacion.Add(usoHabitacion);
            
            Suite suite = _context.Suite.SingleOrDefault(s => s.SuiteId == usoHabitacion.SuiteId);
            suite.Estado = SuiteEstado.Ocupado;
            _context.Suite.Attach(suite);
            _context.Entry(suite).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }
        public void Editar(UsoHabitacion usoHabitacion)
        {
            _context.UsoHabitacion.Attach(usoHabitacion);
            _context.Entry(usoHabitacion).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        
        #endregion



        #region Consultas

        public IQueryable<UsoHabitacion> ObtenerTodo()
        {
            return _context.UsoHabitacion;

        }
        public UsoHabitacion ObtenerPorId(Guid? usoHabitacionId)
        {
            return _context.UsoHabitacion.SingleOrDefault(s => s.UsoHabitacionId == usoHabitacionId);
        }

        public UsoHabitacion ObtenerPorSuiteId(Guid suiteId)
        {
            return _context.UsoHabitacion.OrderByDescending(o => o.Ingreso).SingleOrDefault(s => s.Suite.SuiteId == suiteId);
        }

        #endregion
    }
}
