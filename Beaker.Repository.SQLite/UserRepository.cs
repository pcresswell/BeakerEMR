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
using Beaker.Repository;
using Beaker.Core;
using AutoMapper;
using System.Linq;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite
{
    public class UserRepository : SQLiteRepository<User, UserTable>, IUserRepository, IUserQueries
    {
        public UserRepository()
            : base()
        {
        }

        public IPermissionRepository PermissionRepository { get; set; }

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IUserRepository, User>(this);
        }

        #region implemented abstract members of SQLiteRepository

        /// <summary>
        /// Initializes the auto mapper.
        /// </summary>
        /// <param name="autoMapper">Auto mapper.</param>
        protected override void InitializeAutoMapper(IConfiguration autoMapper)
        {
            autoMapper.CreateMap<User, UserTable>().ForMember(x => x.PermissionID, opt => opt.Ignore());
            autoMapper.CreateMap<UserTable, User>().ForMember(x => x.Permission, opt => opt.Ignore());
        }

        #endregion

        #region implemented abstract members of Repository

        /// <summary>
        /// Finds the user by username.
        /// </summary>
        /// <returns>The by username.</returns>
        /// <param name="username">Username.</param>
        public User FindByUsername(string username)
        {
            var userTable = this.Connection.Table<UserTable>().FirstOrDefault<UserTable>(u => u.Username.Equals(username));
            if (null == userTable)
            {
                return default(User);
            }

            return this.Find(userTable.DomainObjectID);
        }

        #endregion

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToPersistable(User persistable, UserTable table)
        {
            if (!Guid.Empty.Equals(table.PermissionID))
            {
                persistable.Permission = ((IQuery<Permission>)this.PermissionRepository).Find(table.PermissionID, persistable.RecordEndDateTime);
            }
        }

        #endregion

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToTable(UserTable table, User persistable)
        {
            if (persistable.Permission != null)
            {
                if (!this.PermissionRepository.IsPersisted(persistable.Permission))
                {
                    this.PermissionRepository.Save(persistable.Permission);
                }

                table.PermissionID = persistable.Permission.DomainObjectID;
            }
        }

        #endregion
    }
}

