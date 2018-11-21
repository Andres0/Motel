using DS.Motel.Business.AddressBook.Entities;
using DS.Motel.Business.AddressBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class CargoRepository: IcargoRepositorio
    {
        #region fill and proterties
        private DsContext _context = new DsContext();

        #endregion

        #region Consultas

        public IQueryable<Cargo_ADB> ObtenerTodo()
        {
            return _context.AddressBook_Cargo;

        }
        public Cargo_ADB ObtenerPorId(Guid? CargoId)
        {
            return _context.AddressBook_Cargo.SingleOrDefault(s => s.CargoId == CargoId);
        }

        #endregion
        #region Manipulacion

        public void Agregar(Cargo_ADB cargo)
        {
            _context.AddressBook_Cargo.Add(cargo);
            _context.SaveChanges();

        }
        public void Editar(Cargo_ADB cargo)
        {
            _context.AddressBook_Cargo.Attach(cargo);
            _context.Entry(cargo).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Cargo_ADB cargo)
        {
            _context.AddressBook_Cargo.Remove(cargo);
            _context.SaveChanges();
        }
        #endregion
    }
}
