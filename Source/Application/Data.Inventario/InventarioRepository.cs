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
            #region Actualizar Stock
            if (inventario.Tipo == InventarioTipo.Ingreso)
            {
                foreach (InventarioDetalle inventarioDetalle in inventario.Detalle)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock + inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else if (inventario.Tipo == InventarioTipo.Egreso)
            {
                foreach (InventarioDetalle inventarioDetalle in inventario.Detalle)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock - inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
            }
            #endregion
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
            foreach (InventarioDetalle inventarioDetalle in inventario.Detalle)
            {
                if (!detalleAEliminar.Exists(e => e == inventarioDetalle.ItemId))
                {
                    inventarioaActualizar.Detalle.Add(inventarioDetalle);

                    #region Actualizar Stock
                    if (inventario.Tipo == InventarioTipo.Ingreso)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock + inventarioDetalle.Cantidad;

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (inventario.Tipo == InventarioTipo.Egreso)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock - inventarioDetalle.Cantidad;

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    #endregion
                }
                else
                {
                    InventarioDetalle _detalle = inventarioaActualizar.Detalle.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);

                    #region Actualizar Stock
                    if (inventario.Tipo == InventarioTipo.Ingreso)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock + inventarioDetalle.Cantidad - _detalle.Cantidad;

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (inventario.Tipo == InventarioTipo.Egreso)
                    {
                        Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                        item.Cantidad_Stock = item.Cantidad_Stock - inventarioDetalle.Cantidad + _detalle.Cantidad;

                        _context.Item.Attach(item);
                        _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    #endregion

                    _detalle.Cantidad = inventarioDetalle.Cantidad;
                    _detalle.Costo = inventarioDetalle.Costo;
                    _detalle.Indice = inventarioDetalle.Indice;
                    detalleAEliminar.RemoveAll(r => r == _detalle.ItemId);
                }
            }
            foreach (var itemIdEliminar in detalleAEliminar)
            {
                InventarioDetalle inventarioDetalle = _context.InventarioDetalle.SingleOrDefault(s => s.InventarioId == inventario.InventarioId && s.ItemId == itemIdEliminar);

                #region Actualizar Stock
                if (inventario.Tipo == InventarioTipo.Ingreso)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock - inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else if (inventario.Tipo == InventarioTipo.Egreso)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock + inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                #endregion

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

                #region Actualizar Stock
                if (inventario.Tipo == InventarioTipo.Ingreso)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock - inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else if (inventario.Tipo == InventarioTipo.Egreso)
                {
                    Item item = _context.Item.SingleOrDefault(s => s.ItemId == inventarioDetalle.ItemId);
                    item.Cantidad_Stock = item.Cantidad_Stock + inventarioDetalle.Cantidad;

                    _context.Item.Attach(item);
                    _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                #endregion

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

        public bool Existe(Guid itemId)
        {
            return _context.InventarioDetalle.Where(w => w.ItemId == itemId).Count() > 0 ? true : false;
        }

        #endregion
    }
}
