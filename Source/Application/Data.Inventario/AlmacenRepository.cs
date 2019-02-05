using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Inventarios
{
    public class AlmacenRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(Almacen almacen)
        {
            _context.Almacen.Add(almacen);
            _context.SaveChanges();

        }
        public void Editar(Almacen almacen)
        {
            Almacen almancenaActualizar = _context.Almacen.SingleOrDefault(s => s.AlmacenId == almacen.AlmacenId);
            almancenaActualizar.Nombre = almacen.Nombre;
            almancenaActualizar.Descripcion = almacen.Descripcion;

            _context.Almacen.Attach(almancenaActualizar);
            _context.Entry(almancenaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void Eliminar(Guid almacenId)
        {
            Almacen almacen = ObtenerPorId(almacenId);
            _context.Almacen.Remove(almacen);
            _context.SaveChanges();
        }
        #endregion






        #region Consultas

        public IQueryable<Almacen> ObtenerTodo()
        {
            return _context.Almacen;

        }
        public Almacen ObtenerPorId(Guid? almacenId)
        {
            return _context.Almacen.SingleOrDefault(s => s.AlmacenId == almacenId);
        }

        #endregion
    }
}
