﻿using DS.Motel.Business.Security.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations.Security
{
    public class UserTypeConfiguration : EntityTypeConfiguration<UserType_SEC>
    {
        #region Others

        public UserTypeConfiguration()
        {
            HasKey(k => new { k.UserTypeId });
            ToTable("Security_UserType");
        }

        #endregion
    }
}