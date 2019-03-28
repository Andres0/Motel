using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class TrabajoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Trabajo trabajo)
        {
            _context.Trabajo.Add(trabajo);
            _context.SaveChanges();

        }
        public void Editar(Trabajo trabajo)
        {
            Trabajo trabajoAActualizar = _context.Trabajo.SingleOrDefault(s => s.TrabajoId == trabajo.TrabajoId);
            trabajoAActualizar.PersonalId = trabajo.PersonalId;
            trabajoAActualizar.Ingreso = trabajo.Ingreso;
            trabajoAActualizar.Egreso = trabajo.Egreso;

            _context.Trabajo.Attach(trabajoAActualizar);
            _context.Entry(trabajoAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid trabajoId)
        {
            Trabajo trabajo = ObtenerPorId(trabajoId);
            _context.Trabajo.Remove(trabajo);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Trabajo> ObtenerTodo()
        {
            return _context.Trabajo;

        }
        public Trabajo ObtenerPorId(Guid? trabajoId)
        {
            return _context.Trabajo.SingleOrDefault(s => s.TrabajoId == trabajoId);
        }

        #endregion
    }
}
