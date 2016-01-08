// The MIT License (MIT)
//
// Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
using System;
using System.Collections.Generic;
using Beaker.Core.Authorize;
using Beaker.Core;
using Beaker.Repository.SQLite.Tables;
using Newtonsoft.Json;

namespace Beaker.Repository.SQLite
{
    /// <summary>
    /// Permission repository.
    /// </summary>
    public class PermissionRepository : SQLiteRepository<Permission, PermissionTable>, IPermissionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Repository.SQLite.PermissionRepository"/> class.
        /// </summary>
        public PermissionRepository()
            : base()
        {
        }

        #region implemented abstract members of Repository

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IPermissionRepository, Permission>(this);
        }

        #endregion

        #region implemented abstract members of SQLiteRepository

        /// <summary>
        /// Initializes the auto mapper.
        /// </summary>
        /// <param name="autoMapper">Auto mapper.</param>
        protected override void InitializeAutoMapper(AutoMapper.IConfiguration autoMapper)
        {
            autoMapper.CreateMap<PermissionTable, Permission>();
            autoMapper.CreateMap<Permission, PermissionTable>().ForMember(x => x.Content, opt => opt.Ignore());
        }

        protected override void CustomMappingToPersistable(Permission persistable, PermissionTable table)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None,

            };

            Permission permission = JsonConvert.DeserializeObject<Permission>(table.Content, settings);
            foreach (var authorizedAction in permission.AuthorizedActions)
            {
                persistable.AddAuthorization(authorizedAction);
            }

            foreach (var unauthorizedAction in permission.UnauthorizedActions)
            {
                persistable.AddUnauthorization(unauthorizedAction);
            }
        }

        protected override void CustomMappingToTable(PermissionTable table, Permission persistable)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None,

            };

            table.Content = JsonConvert.SerializeObject(persistable, settings);
        }

        #endregion
    }
}

