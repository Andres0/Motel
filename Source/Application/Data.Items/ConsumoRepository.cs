using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Items
{
    public class ConsumoRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulacion

        public void Agregar(List<Consumo> consumo)
        {
            foreach (var item in consumo)
            {
                if (item.ConsumoId == Guid.Empty)
                {
                    item.ConsumoId = Guid.NewGuid();
                    _context.Consumo.Add(item);
                }
                else
                {
                    Consumo consumoaActualizar = _context.Consumo.SingleOrDefault(s => s.ConsumoId == item.ConsumoId);
                    consumoaActualizar.Cantidad = item.Cantidad;
                    consumoaActualizar.Costo = item.Costo;
                    consumoaActualizar.CostoTotal = consumoaActualizar.Cantidad * consumoaActualizar.Costo;

                    _context.Consumo.Attach(consumoaActualizar);
                    _context.Entry(consumoaActualizar).State = System.Data.Entity.EntityState.Modified;
                }
            }
            _context.SaveChanges();
        }
      
        public void Eliminar(Guid consumoId, string observacion)
        {
            Consumo consumoaActualizar = _context.Consumo.SingleOrDefault(s => s.ConsumoId == consumoId);
            consumoaActualizar.Observacion = observacion;
            consumoaActualizar.Estado = ConsumoEstado.Devuelto;

            _context.Consumo.Attach(consumoaActualizar);
            _context.Entry(consumoaActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion






        #region Consultas

        public IQueryable<Consumo> ObtenerTodo()
        {
            return _context.Consumo;

        }
        public Consumo ObtenerPorId(Guid consumoId)
        {
            return _context.Consumo.SingleOrDefault(s => s.ConsumoId == consumoId);
        }

        public IQueryable<Consumo> ObtenerPorUsoHabitacionId(Guid usoHabitacionId)
        {
            return _context.Consumo.Where(s => s.UsoHabitacionId == usoHabitacionId);
        }

        #endregion
    }
}
