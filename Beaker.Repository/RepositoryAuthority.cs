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
using Beaker.Authorize;

namespace Beaker.Repository
{
    internal class RepositoryAuthority
    {
        private ICan UserPermission { get; set; }

        internal RepositoryAuthority(ICan userPermission)
        {
            this.UserPermission = userPermission;
        }

        internal void CanSave<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            if (this.UserPermission.Can(Actions.Update,typeof(TPersistable)) != true)
            {
                throw new ActionNotPermittedException("Cannot update the requested object as the user does not have permissions to do this action");
            }
        }

        internal void CanDelete<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            if (this.UserPermission.Can(Actions.Delete,typeof(TPersistable)) != true)
            {
                throw new ActionNotPermittedException("Cannot delete the requested object as the user does not have permissions to do this action");
            }
        }

        internal void CanFind<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            if (this.UserPermission.Can(Actions.Read, persistable) != true)
            {
                throw new ActionNotPermittedException("Cannot find the requested object as the user does not have permissions to do this action");
            }
        }
    }
}

