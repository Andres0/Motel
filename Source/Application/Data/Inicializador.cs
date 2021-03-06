﻿using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data
{
    //public class Inicializador : DropCreateDatabaseIfModelChanges<DsContext>
    public class Inicializador : DropCreateDatabaseIfModelChanges<DsContext>
    {
        public override void InitializeDatabase(DsContext context)
        {
            if (!context.Database.Exists())
            {
                using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString.Replace("MotelDB", "master")))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CREATE DATABASE MotelDB COLLATE SQL_Latin1_General_CP1_CI_AI", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                SqlConnection.ClearAllPools();
            }
            base.InitializeDatabase(context);
        }

        protected override void Seed(DsContext context)
        {
            var Cargo = new List<Cargo>
            {
                new Cargo { CargoId = Guid.NewGuid(), Nombre = "Admin", Descripcion = "Es cargo administrador" }
            };
            Cargo.ForEach(s => context.Cargo.Add(s));

            var TipoUsuarios = new List<UserType>
            {
                new UserType { UserTypeId = Guid.NewGuid(), Name = "Admin", Descripcion = "Es tipo administrador" }
            };
            TipoUsuarios.ForEach(s => context.UserType.Add(s));

            var Personas = new List<Personal>
            {
                new Personal { PersonalId = Guid.NewGuid(), Nombre = "Admin", Apellido = "Admin", CI = "0", Direccion="", Email="", Login="Admin", Password="Admin", Observacion="", Estado=PersonalEstado.Activado,
                 CargoId = Cargo.FirstOrDefault().CargoId, UserTypeId = TipoUsuarios.FirstOrDefault().UserTypeId }
            };
            Personas.ForEach(s => context.Personal.Add(s));

            var CajaBancos = new List<CajaBanco>
            {
                new CajaBanco { CajaBancoId = new Guid("11111111-2222-3333-4444-555555555555"), Nombre = "Caja chica", Descripcion = null, Tipo = CajaBancoTipo.Caja }
            };
            CajaBancos.ForEach(s => context.CajaBanco.Add(s));
            base.Seed(context);

            //context.Database.ExecuteSqlCommand("ALTER DATABASE MotelDB COLLATE SQL_Latin1_General_CP1_CI_AI");  no funciona esta linea

            //var students = new List<Student>
            //{
            //new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            //new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            //new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            //new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            //new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            //new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            //};

            //students.ForEach(s => context.Students.Add(s));
            //context.SaveChanges();
        }
    }

    //public class Inicializador : IDatabaseInitializer<DsContext>
    //{
    //public void InitializeDatabase(DsContext context)
    //{
    //    if (!context.Database.Exists() || !context.Database.CompatibleWithModel(true))
    //    {
    //        context.Database.Delete();
    //        //context.Database.Create();

    //        using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString.Replace("MotelDB", "master")))
    //        {
    //            connection.Open();
    //            using (SqlCommand command = new SqlCommand("Create DATABASE MotelDB COLLATE SQL_Latin1_General_CP1_CI_AI", connection))
    //            {
    //                command.ExecuteNonQuery();
    //            }
    //        }
    //        SqlConnection.ClearAllPools();
    //        //context.Database.ExecuteSqlCommand("ALTER DATABASE MotelDB COLLATE SQL_Latin1_General_CP1_CI_AI");

    //        //Agregar caja bancos
    //        var CajaBancos = new List<CajaBanco>
    //        {
    //            new CajaBanco{ Nombre = "Caja chica", Descripcion = null, Tipo = CajaBancoTipo.Caja }
    //        };
    //        CajaBancos.ForEach(s => context.CajaBanco.Add(s));
    //        context.SaveChanges();
    //    }
    //    //context.Database.ExecuteSqlCommand("Custom SQL Command here");
    //}
    //}
}
