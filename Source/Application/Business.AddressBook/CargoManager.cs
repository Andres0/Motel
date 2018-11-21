using DS.Motel.Business.AddressBook.Entities;
using DS.Motel.Business.AddressBook.Repositories;
using DS.Motel.Data.AddressBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.AddressBook
{
    public class CargoManager: IcargoRepositorio
    {
        #region fields & properties
        CargoRepository _CargoRepository { get; set; }


        #endregion
        #region Constructor
        public CargoManager(CargoRepository cargoRepository)
        {
            _CargoRepository = cargoRepository;
        }

        #endregion
        #region Consultas
        public IQueryable<Cargo_ADB> ObtenerTodos()
        {
            return _CargoRepository.ObtenerTodo();
        }

        #endregion
    }

}
