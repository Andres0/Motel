using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class ClienteRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();

        }
        public void Editar(Cliente cliente)
        {
            Cliente clienteaActualizar = _context.Cliente.SingleOrDefault(s => s.ClienteId == cliente.ClienteId);
            clienteaActualizar.Nombre = cliente.Nombre;
            clienteaActualizar.Color = cliente.Color;
            clienteaActualizar.TipoVehiculo = cliente.TipoVehiculo;
            clienteaActualizar.Descripcion = cliente.Descripcion;

            _context.Cliente.Attach(clienteaActualizar);
            _context.Entry(clienteaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid clienteId)
        {
            Cliente cliente = ObtenerPorId(clienteId);
            _context.Cliente.Remove(cliente);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Cliente> ObtenerTodo()
        {
            return _context.Cliente;

        }
        public Cliente ObtenerPorId(Guid? clienteId)
        {
            return _context.Cliente.SingleOrDefault(s => s.ClienteId == clienteId);
        }

        #endregion
    }
}
