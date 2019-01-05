using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Items
{
    public class ItemRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Item item)
        {
            _context.Item.Add(item);
            _context.SaveChanges();

        }
        public void Editar(Item item)
        {
            Item itemAActualizar = _context.Item.SingleOrDefault(s => s.ItemId == item.ItemId);
            itemAActualizar.Codigo = item.Codigo;
            itemAActualizar.Nombre = item.Nombre;
            itemAActualizar.Descripcion = item.Descripcion;
            itemAActualizar.Cantidad_Stock = item.Cantidad_Stock;
            itemAActualizar.Costo_Total = item.Costo_Total;
            itemAActualizar.Costo_Unitario = item.Costo_Unitario;
            itemAActualizar.Proporcion = item.Proporcion;
            itemAActualizar.Stock_Minimo = item.Stock_Minimo;
            itemAActualizar.EsVendible = item.EsVendible;
            itemAActualizar.ItemCategoriaId = item.ItemCategoriaId;

            _context.Item.Attach(itemAActualizar);
            _context.Entry(itemAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid itemId)
        {
            Item Item = ObtenerPorId(itemId);
            _context.Item.Remove(Item);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Item> ObtenerTodo()
        {
            return _context.Item;

        }
        public Item ObtenerPorId(Guid? itemId)
        {
            return _context.Item.SingleOrDefault(s => s.ItemId == itemId);
        }

        #endregion
    }
}
