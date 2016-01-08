// /*
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
// */
using System;
using Beaker.Repository.SQLite;
using Beaker.Services;
using Beaker.Core.Authorize;
using Beaker.Repository;

namespace Beaker.Initialization
{
    internal class InitializeSQLiteDatabase
    {
        private SQLiteDatabase Database { get; set;}
        private ICan UserPermission {get;set;}
        private IAuthor Author { get; set; }

        public InitializeSQLiteDatabase(SQLiteDatabase database, ICan userPermission, IAuthor author)
        {
            this.Database = database;
            this.UserPermission = userPermission;
            this.Author = author;
        }

        public void Run()
        {
            SQLiteRepositoryFactory factory = new SQLiteRepositoryFactory();
            factory.RegisterRepositoriesWithDatabase(this.Database);
        }
    }
}

