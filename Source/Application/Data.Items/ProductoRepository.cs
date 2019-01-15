using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Items
{
    public class ProductoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Producto producto)
        {
            _context.Producto.Add(producto);
            _context.SaveChanges();

        }
        public void Editar(Producto producto)
        {
            Producto productoAActualizar = _context.Producto.SingleOrDefault(s => s.ProductoId == producto.ProductoId);
            productoAActualizar.Nombre = producto.Nombre;
            productoAActualizar.Descripcion = producto.Descripcion;
            productoAActualizar.ItemCategoriaId = producto.ItemCategoriaId;
            productoAActualizar.Costo_venta = producto.Costo_venta;

            List<Guid> detalleAEliminar = productoAActualizar.Detalle.Select(s => s.ProductoDetalleId).ToList();
            foreach (var item in producto.Detalle)
            {
                if (item.ProductoDetalleId == Guid.Empty)
                {
                    productoAActualizar.Detalle.Add(new ProductoDetalle() { ItemId = item.ItemId, Cantidad = item.Cantidad });
                }
                else
                {
                    ProductoDetalle _detalle = productoAActualizar.Detalle.SingleOrDefault(s => s.ProductoDetalleId == item.ProductoDetalleId);
                    _detalle.Cantidad = item.Cantidad;
                    detalleAEliminar.RemoveAll(r => r == _detalle.ProductoDetalleId);
                }
            }
            foreach (var item in detalleAEliminar)
            {
                ProductoDetalle productoDetalle = _context.ProductoDetalle.SingleOrDefault(s => s.ProductoDetalleId == item);
                _context.ProductoDetalle.Remove(productoDetalle);
            }

            _context.Producto.Attach(productoAActualizar);
            _context.Entry(productoAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid productoId)
        {
            Producto producto = ObtenerPorId(productoId);
            List<Guid> detalleIds = producto.Detalle.Select(s => s.ProductoDetalleId).ToList();
            foreach (var detalle in detalleIds)
            {
                ProductoDetalle productoDetalle = _context.ProductoDetalle.SingleOrDefault(s => s.ProductoDetalleId == detalle);
                _context.ProductoDetalle.Remove(productoDetalle);
            }
            _context.Producto.Remove(producto);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Producto> ObtenerTodo()
        {
            return _context.Producto;

        }
        public Producto ObtenerPorId(Guid? productoId)
        {
            return _context.Producto.SingleOrDefault(s => s.ProductoId == productoId);
        }

        #endregion
    }
}
