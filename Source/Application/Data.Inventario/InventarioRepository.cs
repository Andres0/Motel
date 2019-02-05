using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Inventarios
{
    public class InventarioRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Inventario inventario)
        {
            _context.Inventario.Add(inventario);
            _context.SaveChanges();

        }
        public void Editar(Inventario inventario)
        {
            Inventario inventarioaActualizar = _context.Inventario.SingleOrDefault(s => s.InventarioId == inventario.InventarioId);
            inventarioaActualizar.Fecha = inventario.Fecha;
            inventarioaActualizar.Descripcion = inventario.Descripcion;
            inventarioaActualizar.Total = inventario.Total;
            inventarioaActualizar.DeAlmacenId = inventario.DeAlmacenId;
            inventarioaActualizar.AAlmacenId = inventario.AAlmacenId;

            List<Guid> detalleAEliminar = inventarioaActualizar.Detalle.Select(s => s.ItemId).ToList();
            foreach (var item in inventario.Detalle)
            {
                if (!detalleAEliminar.Exists(e => e == item.ItemId))
                {
                    inventarioaActualizar.Detalle.Add(item);
                }
                else
                {
                    InventarioDetalle _detalle = inventarioaActualizar.Detalle.SingleOrDefault(s => s.ItemId == item.ItemId);
                    _detalle.Cantidad = item.Cantidad;
                    _detalle.Costo = item.Costo;
                    _detalle.Indice = item.Indice;
                    detalleAEliminar.RemoveAll(r => r == _detalle.ItemId);
                }
            }
            foreach (var item in detalleAEliminar)
            {
                InventarioDetalle inventarioDetalle = _context.InventarioDetalle.SingleOrDefault(s => s.ItemId == item);
                _context.InventarioDetalle.Remove(inventarioDetalle);
            }

            _context.Inventario.Attach(inventarioaActualizar);
            _context.Entry(inventarioaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid inventarioId)
        {
            Inventario inventario = ObtenerPorId(inventarioId);
            List<Guid> detalleIds = inventario.Detalle.Select(s => s.InventarioDetalleId).ToList();
            foreach (var detalle in detalleIds)
            {
                InventarioDetalle inventarioDetalle = _context.InventarioDetalle.SingleOrDefault(s => s.InventarioDetalleId == detalle);
                _context.InventarioDetalle.Remove(inventarioDetalle);
            }
            _context.Inventario.Remove(inventario);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Inventario> ObtenerTodo()
        {
            return _context.Inventario;

        }
        public Inventario ObtenerPorId(Guid? inventarioId)
        {
            return _context.Inventario.SingleOrDefault(s => s.InventarioId == inventarioId);
        }

        #endregion
    }
}
