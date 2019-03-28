using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Inventarios
{
    public class InventarioProductoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(InventarioProducto inventarioProducto)
        {
            _context.InventarioProducto.Add(inventarioProducto);
            #region Actualizar Stock
            if (inventarioProducto.Tipo == InventarioTipo.Ingreso)
            {
                foreach (InventarioProductoDetalle inventarioProductoDetalle in inventarioProducto.Detalle)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock + inventarioProductoDetalle.Cantidad;


                    foreach (var productoDetalle in producto.Detalle)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == productoDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock - (productoDetalle.Cantidad * inventarioProductoDetalle.Cantidad);

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else if (inventarioProducto.Tipo == InventarioTipo.Egreso)
            {
                foreach (InventarioProductoDetalle inventarioProductoDetalle in inventarioProducto.Detalle)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock - inventarioProductoDetalle.Cantidad;

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
            }
            #endregion
            _context.SaveChanges();
        }
        public void Editar(InventarioProducto inventarioProducto)
        {
            InventarioProducto inventarioProductoaActualizar = _context.InventarioProducto.SingleOrDefault(s => s.InventarioProductoId == inventarioProducto.InventarioProductoId);
            inventarioProductoaActualizar.Fecha = inventarioProducto.Fecha;
            inventarioProductoaActualizar.Descripcion = inventarioProducto.Descripcion;
            inventarioProductoaActualizar.Total = inventarioProducto.Total;
            inventarioProductoaActualizar.DeAlmacenId = inventarioProducto.DeAlmacenId;
            inventarioProductoaActualizar.AAlmacenId = inventarioProducto.AAlmacenId;

            List<Guid> detalleAEliminar = inventarioProductoaActualizar.Detalle.Select(s => s.ProductoId).ToList();
            foreach (InventarioProductoDetalle inventarioProductoDetalle in inventarioProducto.Detalle)
            {
                if (!detalleAEliminar.Exists(e => e == inventarioProductoDetalle.ProductoId))
                {
                    inventarioProductoaActualizar.Detalle.Add(inventarioProductoDetalle);

                    #region Actualizar Stock
                    if (inventarioProducto.Tipo == InventarioTipo.Ingreso)
                    {
                        Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                        producto.Cantidad_Stock = producto.Cantidad_Stock + inventarioProductoDetalle.Cantidad;

                        _context.Producto.Attach(producto);
                        _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (inventarioProducto.Tipo == InventarioTipo.Egreso)
                    {
                        Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                        producto.Cantidad_Stock = producto.Cantidad_Stock - inventarioProductoDetalle.Cantidad;

                        _context.Producto.Attach(producto);
                        _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    }
                    #endregion
                }
                else
                {
                    InventarioProductoDetalle _detalle = inventarioProductoaActualizar.Detalle.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);

                    #region Actualizar Stock
                    if (inventarioProducto.Tipo == InventarioTipo.Ingreso)
                    {
                        Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                        producto.Cantidad_Stock = producto.Cantidad_Stock + inventarioProductoDetalle.Cantidad - _detalle.Cantidad;

                        _context.Producto.Attach(producto);
                        _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (inventarioProducto.Tipo == InventarioTipo.Egreso)
                    {
                        Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                        producto.Cantidad_Stock = producto.Cantidad_Stock - inventarioProductoDetalle.Cantidad + _detalle.Cantidad;

                        _context.Producto.Attach(producto);
                        _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                    }
                    #endregion

                    _detalle.Cantidad = inventarioProductoDetalle.Cantidad;
                    _detalle.Costo = inventarioProductoDetalle.Costo;
                    _detalle.Indice = inventarioProductoDetalle.Indice;
                    detalleAEliminar.RemoveAll(r => r == _detalle.ProductoId);
                }
            }
            foreach (var itemIdEliminar in detalleAEliminar)
            {
                InventarioProductoDetalle inventarioProductoDetalle = _context.InventarioProductoDetalle.SingleOrDefault(s => s.InventarioProductoId == inventarioProducto.InventarioProductoId && s.ProductoId == itemIdEliminar);

                #region Actualizar Stock
                if (inventarioProducto.Tipo == InventarioTipo.Ingreso)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock - inventarioProductoDetalle.Cantidad;

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
                else if (inventarioProducto.Tipo == InventarioTipo.Egreso)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock + inventarioProductoDetalle.Cantidad;

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
                #endregion

                _context.InventarioProductoDetalle.Remove(inventarioProductoDetalle);
            }

            _context.InventarioProducto.Attach(inventarioProductoaActualizar);
            _context.Entry(inventarioProductoaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid inventarioProductoId)
        {
            InventarioProducto inventarioProducto = ObtenerPorId(inventarioProductoId);
            List<Guid> detalleIds = inventarioProducto.Detalle.Select(s => s.InventarioProductoDetalleId).ToList();
            foreach (var detalle in detalleIds)
            {
                InventarioProductoDetalle inventarioProductoDetalle = _context.InventarioProductoDetalle.SingleOrDefault(s => s.InventarioProductoDetalleId == detalle);

                #region Actualizar Stock
                if (inventarioProducto.Tipo == InventarioTipo.Ingreso)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock - inventarioProductoDetalle.Cantidad;

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;

                    foreach (var productoDetalle in producto.Detalle)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == productoDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock + (productoDetalle.Cantidad * inventarioProductoDetalle.Cantidad);

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else if (inventarioProducto.Tipo == InventarioTipo.Egreso)
                {
                    Producto producto = _context.Producto.SingleOrDefault(s => s.ProductoId == inventarioProductoDetalle.ProductoId);
                    producto.Cantidad_Stock = producto.Cantidad_Stock + inventarioProductoDetalle.Cantidad;

                    _context.Producto.Attach(producto);
                    _context.Entry(producto).State = System.Data.Entity.EntityState.Modified;
                }
                #endregion

                _context.InventarioProductoDetalle.Remove(inventarioProductoDetalle);
            }
            _context.InventarioProducto.Remove(inventarioProducto);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<InventarioProducto> ObtenerTodo()
        {
            return _context.InventarioProducto;

        }
        public InventarioProducto ObtenerPorId(Guid? inventarioProductoId)
        {
            return _context.InventarioProducto.SingleOrDefault(s => s.InventarioProductoId == inventarioProductoId);
        }

        public bool Existe(Guid productoId)
        {
            return _context.InventarioProductoDetalle.Where(w => w.ProductoId == productoId).Count() > 0 ? true : false;
        }

        #endregion
    }
}
