using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data
{
    public class DsContext : DbContext
    {
        #region Fields & Properties

        public DbSet<Almacen> Almacen { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CajaBanco> CajaBanco { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Consumo> Consumo { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<InventarioDetalle> InventarioDetalle { get; set; }
        public DbSet<InventarioProducto> InventarioProducto { get; set; }
        public DbSet<InventarioProductoDetalle> InventarioProductoDetalle { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemCategoria> ItemCategoria { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoDetalle> ProductoDetalle { get; set; }
        public DbSet<Suite> Suite { get; set; }
        public DbSet<Trabajo> Trabajo { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<UsoHabitacion> UsoHabitacion { get; set; }

        public DbSet<TarjetaDescuento> TarjetaDescuento { get; set; }

        #endregion






        #region Constructors

        public DsContext() : base("MotelDB")
        {
        }

        #endregion






        #region Events

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new Configurations.AlmacenConfiguration());
            modelBuilder.Configurations.Add(new Configurations.CargoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.CajaBancoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ClienteConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ConsumoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.InventarioConfiguration());
            modelBuilder.Configurations.Add(new Configurations.InventarioDetalleConfiguration());
            modelBuilder.Configurations.Add(new Configurations.InventarioProductoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.InventarioProductoDetalleConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ItemConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ItemCategoriaConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ParametrosConfiguration());
            modelBuilder.Configurations.Add(new Configurations.PersonalConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ProductoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ProductoDetalleConfiguration());
            modelBuilder.Configurations.Add(new Configurations.SuiteConfiguration());
            modelBuilder.Configurations.Add(new Configurations.TrabajoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.TransaccionConfiguration());
            modelBuilder.Configurations.Add(new Configurations.UserTypeConfiguration());
            modelBuilder.Configurations.Add(new Configurations.TarjetaDescuentoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.UsoHabitacionConfiguration());
        }

        #endregion
    }
}
