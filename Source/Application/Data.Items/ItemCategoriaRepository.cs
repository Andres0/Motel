﻿using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Items
{
    public class ItemCategoriaRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(ItemCategoria itemCategoria)
        {
            _context.ItemCategoria.Add(itemCategoria);
            _context.SaveChanges();

        }
        public void Editar(ItemCategoria itemCategoria)
        {
            ItemCategoria itemCategoriaAActualizar = _context.ItemCategoria.SingleOrDefault(s => s.ItemCategoriaId == itemCategoria.ItemCategoriaId);
            itemCategoriaAActualizar.Nombre = itemCategoria.Nombre;
            itemCategoriaAActualizar.Descripcion = itemCategoria.Descripcion;
            itemCategoriaAActualizar.PadreId = itemCategoria.PadreId;

            _context.ItemCategoria.Attach(itemCategoriaAActualizar);
            _context.Entry(itemCategoriaAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid itemCategoriaId)
        {
            ItemCategoria itemCategoria = ObtenerPorId(itemCategoriaId);
            _context.ItemCategoria.Remove(itemCategoria);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<ItemCategoria> ObtenerTodo()
        {
            return _context.ItemCategoria;

        }
        public ItemCategoria ObtenerPorId(Guid? itemCategoriaId)
        {
            return _context.ItemCategoria.SingleOrDefault(s => s.ItemCategoriaId == itemCategoriaId);
        }

        #endregion
    }
}
