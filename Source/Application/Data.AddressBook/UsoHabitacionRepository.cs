using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.AddressBook
{
    public class UsoHabitacionRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion



        #region Manipulacion

        public void Agregar(UsoHabitacion usoHabitacion)
        {
            _context.UsoHabitacion.Add(usoHabitacion);
            
            Suite suite = _context.Suite.SingleOrDefault(s => s.SuiteId == usoHabitacion.SuiteId);
            suite.Estado = SuiteEstado.Ocupado;
            _context.Suite.Attach(suite);
            _context.Entry(suite).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }
        public void Editar(UsoHabitacion usoHabitacion)
        {
            UsoHabitacion usoHabitacionAActualizar = _context.UsoHabitacion.SingleOrDefault(s => s.UsoHabitacionId == usoHabitacion.UsoHabitacionId);
            //usoHabitacionAActualizar.SuiteId = usoHabitacion.SuiteId;
            usoHabitacionAActualizar.ClienteId = usoHabitacion.ClienteId;
            //usoHabitacionAActualizar.Numero = usoHabitacion.Numero;
            //usoHabitacionAActualizar.TipoIngreso = usoHabitacion.TipoIngreso;
            //usoHabitacionAActualizar.Ingreso = usoHabitacion.Ingreso;
            //usoHabitacionAActualizar.Salida = usoHabitacion.Salida;
            //usoHabitacionAActualizar.Cobro = usoHabitacion.Cobro;
            //usoHabitacionAActualizar.Tiempo_Uso = usoHabitacion.Tiempo_Uso;
            //usoHabitacionAActualizar.Tiempo_Total = usoHabitacion.Tiempo_Total;
            //usoHabitacionAActualizar.TipoUso = usoHabitacion.TipoUso;
            //usoHabitacionAActualizar.Costo = usoHabitacion.Costo;
            //usoHabitacionAActualizar.CostoEspecial = usoHabitacion.CostoEspecial;
            //usoHabitacionAActualizar.Observacion = usoHabitacion.Observacion;
            //usoHabitacionAActualizar.Costo_Habitacion = usoHabitacion.Costo_Habitacion;
            usoHabitacionAActualizar.Costo_Insumos = usoHabitacion.Costo_Insumos;
            usoHabitacionAActualizar.Costo_Insumos_Externo = usoHabitacion.Costo_Insumos_Externo;
            usoHabitacionAActualizar.Costo_tv = usoHabitacion.Costo_tv;
            usoHabitacionAActualizar.Costo_Total = usoHabitacion.Costo_Total;

            _context.UsoHabitacion.Attach(usoHabitacionAActualizar);
            _context.Entry(usoHabitacionAActualizar).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Anular(Guid suiteId, string observacion)
        {
            UsoHabitacion usoHabitacion = ObtenerPorSuiteId(suiteId);
            usoHabitacion.Salida = DateTime.Now;
            usoHabitacion.TipoUso = TipoUso.Interrumpido;
            usoHabitacion.Observacion = "Anulación: " + observacion;

            _context.UsoHabitacion.Attach(usoHabitacion);
            _context.Entry(usoHabitacion).State = System.Data.Entity.EntityState.Modified;


            Suite suite = _context.Suite.SingleOrDefault(s => s.SuiteId == usoHabitacion.SuiteId);
            suite.Estado = SuiteEstado.Libre;
            _context.Suite.Attach(suite);
            _context.Entry(suite).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }

        #endregion



        #region Consultas

        public IQueryable<UsoHabitacion> ObtenerTodo()
        {
            return _context.UsoHabitacion;

        }
        public UsoHabitacion ObtenerPorId(Guid? usoHabitacionId)
        {
            return _context.UsoHabitacion.SingleOrDefault(s => s.UsoHabitacionId == usoHabitacionId);
        }

        public UsoHabitacion ObtenerPorSuiteId(Guid suiteId)
        {
            return _context.UsoHabitacion.OrderByDescending(o => o.Ingreso).FirstOrDefault(s => s.Suite.SuiteId == suiteId);
        }

        #endregion
    }
}
