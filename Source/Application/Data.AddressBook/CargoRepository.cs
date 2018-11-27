using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class CargoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Cargo cargo)
        {
            _context.AddressBook_Cargo.Add(cargo);
            _context.SaveChanges();

        }
        public void Editar(Cargo cargo)
        {
            _context.AddressBook_Cargo.Attach(cargo);
            _context.Entry(cargo).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid cargoId)
        {
            Cargo cargo = ObtenerPorId(cargoId);
            _context.AddressBook_Cargo.Remove(cargo);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Cargo> ObtenerTodo()
        {
            return _context.AddressBook_Cargo;

        }
        public Cargo ObtenerPorId(Guid? CargoId)
        {
            return _context.AddressBook_Cargo.SingleOrDefault(s => s.CargoId == CargoId);
        }

        #endregion
    }
}
