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



        #region Manipulation

        public void Add(Cargo_ADB cargo)
        {
            try
            {
                _CargoRepository.Agregar(cargo);
            }
            catch (Exception ex)
            {
            }
        }

        public void Edit(Cargo_ADB cargo)
        {
            try
            {
                //Get original entity
                Cargo_ADB cargoToUpdate = _CargoRepository.ObtenerPorId(cargo.CargoId);
                cargoToUpdate.Nombre = cargo.Nombre;
                cargoToUpdate.Descripcion = cargo.Descripcion;

                _CargoRepository.Editar(cargoToUpdate);
            }
            catch (Exception ex)
            {
            }
        }

        public void Delete(Guid cargoId)
        {
            try
            {
                //Get original entity
                Cargo_ADB cargoToDelete = _CargoRepository.ObtenerPorId(cargoId);

                _CargoRepository.Eliminar(cargoToDelete);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }

}
