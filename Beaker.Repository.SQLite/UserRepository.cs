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
using Beaker.Authorize;

namespace Beaker.Repository.SQLite
{
    public class UserRepository : SQLiteRepository<User, UserTable> , IUserRepository
    {
        public UserRepository(ICan can, IAuthor author)
            : base(can, author)
        {
        }

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IUserRepository, User>(this);
        }

        #region implemented abstract members of SQLiteRepository

        protected override void InitializeAutoMapper(IConfiguration autoMapper)
        {
            this.CreateTwoWayMap<User,UserTable>(autoMapper);
        }

        #endregion

        #region implemented abstract members of Repository

        protected override User Find(Guid domainObjectID, DateTime onDateTime)
        {
            UserTable t = this.Connection.Find<UserTable>(domainObjectID, onDateTime);
            if (null == t)
            {
                return default(User);
            }

            return Mapper.Map<User>(t);
        }

        /// <summary>
        /// Insert the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected override void Insert(User persistable)
        {
            UserTable t = Mapper.Map<UserTable>(persistable);
            this.Connection.Insert(t);
        }

        /// <summary>
        /// Update the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected override void Update(User persistable)
        {
            UserTable t = Mapper.Map<UserTable>(persistable);
            this.Connection.Update(t);
        }

        /// <summary>
        /// Retrieve the object with the given ID from persistence.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override User Get(Guid id)
        {
            UserTable t = base.Connection.Get<UserTable>(id);
            if (t == null)
            {
                return default(User);
            }

            return Mapper.Map<User>(t);
        }

        /// <summary>
        /// Finds the user by username.
        /// </summary>
        /// <returns>The by username.</returns>
        /// <param name="username">Username.</param>
        User IUserRepository.FindByUsername(string username)
        {
            var userTable = this.Connection.Table<UserTable>().SingleOrDefault<UserTable>(u => u.Username.Equals(username));
            if (null == userTable)
            {
                return default(User);
            }

            return this.Find(userTable.DomainObjectID, Dates.Infinity);
        }

        #endregion
    }
}

