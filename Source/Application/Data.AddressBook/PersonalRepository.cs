using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class PersonalRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion



        #region Manipulacion

        public void Agregar(Personal personal)
        {
            _context.Personal.Add(personal);
            _context.SaveChanges();

        }
        public void Editar(Personal personal)
        {
            _context.Personal.Attach(personal);
            _context.Entry(personal).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid PersonalId)
        {
            Personal personal = ObtenerPorId(PersonalId);
            _context.Personal.Remove(personal);
            _context.SaveChanges();
        }
        #endregion



        #region Consultas

        public IQueryable<Personal> ObtenerTodo()
        {
            return _context.Personal;

        }
        public Personal ObtenerPorId(Guid? PersonalID)
        {
            return _context.Personal.SingleOrDefault(s => s.PersonalId == PersonalID);
        }

        #endregion
    }
}
