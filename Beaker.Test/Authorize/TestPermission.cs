//
// TestPermission.cs
//
// Author:
//       peter <pcresswell@gmail.com>
//
// Copyright (c) 2015 peter
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using NUnit;
using NUnit.Framework;
using Beaker.Core.Authorize;
using System.Collections.Generic;
using Newtonsoft.Json;
using Beaker.Core;

namespace Beaker.Test.Authorize
{
    [TestFixture]
    public class TestPermission
    {
        public TestPermission()
        {
        }

        [Test]
        public void UncertainWithoutAnyIndication()
        {
            Permission user = new Permission();
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsNull(canCreate);
        }

        [Test]
        public void TestCanCreateAnything()
        {
            Permission user = new Permission();
            user.AddAuthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
        }

        [Test]
        public void TestUserCanDoAnything()
        {
            Permission user = new Permission();
            user.AddAuthorization(Actions.Manage);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
            canCreate = user.Can(Actions.Delete, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
        }

        [Test]
        public void TestCannotCreateAnything()
        {
            Permission user = new Permission();
            user.AddUnauthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == false);
        }

        [Test]
        public void TestNotCertainIfCanCreate()
        {
            Permission user = new Permission();
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == null);
        }

        [Test]
        public void UnauthorizedDominatesOverAuthorized()
        {
            Permission user = new Permission();
            user.AddUnauthorization(Actions.Create);
            user.AddAuthorization(Actions.Create);
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == false);
        }

        [Test]
        public void CanAuthorizeAgainstAType()
        {
            Permission user = new Permission();
            Create createAction = new Create();
            createAction.AddSubject(typeof(AddressModel));
            user.AddAuthorization(createAction);
            Assert.IsTrue(createAction.AppliesTo(typeof(AddressModel)));
            bool? canCreate = user.Can(Actions.Create, typeof(AddressModel));
            Assert.IsTrue(canCreate == true);
        }

        [Test]
        public void CanAuthorizedAgainstAnInstance()
        {
            Permission user = new Permission();
            AddressModel address = new AddressModel();
            Beaker.Core.Authorize.Update updateAction = new Beaker.Core.Authorize.Update(address);
            user.AddAuthorization(updateAction);

            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);

            AddressModel differentAddress = new AddressModel();
            canUpdate = user.Can(Actions.Update, differentAddress);
            Assert.IsTrue(canUpdate == false);
            canUpdate = user.Can(Actions.Update, typeof(AddressModel));
            Assert.IsTrue(canUpdate == false);
        }

        [Test]
        public void CanAuthorizedAgainstAnInstanceButNotAgainstAnotherInstance()
        {
            Permission user = new Permission();
            AddressModel address = new AddressModel();
            Create createAction = new Create();
            createAction.AddSubject(address);

            user.AddAuthorization(createAction);
           
            bool? canUpdate = user.Can(Actions.Create, address);
            Assert.IsTrue(canUpdate == true);

            AddressModel secondAddress = new AddressModel();
            canUpdate = user.Can(Actions.Create, secondAddress);
            Assert.IsTrue(canUpdate == false);
        }

        [Test]
        public void AuthorizingAgainstTheTypeGivesAuthorityForAllInstances()
        {
            Permission user = new Permission();
            Beaker.Core.Authorize.Update updateAction = new Beaker.Core.Authorize.Update();
            AddressModel address = new AddressModel();

            updateAction.AddSubject(typeof(AddressModel));
            user.AddAuthorization(updateAction);
            Assert.IsTrue(updateAction.AppliesTo(address));
            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);
        }

        [Test]
        public void UnauthorizedAgainstTheTypePreventsAuthorizationForAllInstances()
        {

            // If we are unauthorized against a type,
            // then even if we authorize against an instance
            // we are not authorized
            Permission user = new Permission();
            Beaker.Core.Authorize.Update updateAction = new Beaker.Core.Authorize.Update(typeof(AddressModel));
            AddressModel address = new AddressModel();

            user.AddUnauthorization(updateAction);
            Assert.IsTrue(updateAction.AppliesTo(address));
            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == false);

            // now authorize against an instance

            Beaker.Core.Authorize.Update authorizeInstanceUpdateAction = new Beaker.Core.Authorize.Update();

            authorizeInstanceUpdateAction.AddSubject(address);
            user.AddAuthorization(authorizeInstanceUpdateAction);
            canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == false);
        }

        [Test]
        public void ManagingActionIsUnionOfAllActions()
        {
            // When we have a Manage Action,
            // we can do all actions
            Permission user = new Permission();
            Manage manageAction = new Manage();
            AddressModel address = new AddressModel();

            user.AddAuthorization(manageAction);
            Assert.IsTrue(manageAction.AppliesTo(address));

            bool? canUpdate = user.Can(Actions.Update, address);
            Assert.IsTrue(canUpdate == true);
        }

        [Test]
        public void GetAuthorizationsForPersistance()
        {
            Permission user = new Permission();
            Create createAction = new Create(typeof(AddressModel));
            user.AddAuthorization(createAction);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented 
            };
            
            string serialized = JsonConvert.SerializeObject(user, settings);
            Permission deserializedUser = JsonConvert.DeserializeObject<Permission>(serialized, settings);

            // Now make sure deserialized object has same behaviour
            Assert.IsTrue(user.Can(Actions.Create, typeof(AddressModel)) == true);
            Assert.IsTrue(deserializedUser.Can(Actions.Create, typeof(AddressModel)) == true);
        }

        [Test]
        public void ADoctorCanBeAuthorizedToManageAPatient()
        {
            Provider doctor = new Provider();
            Patient patient = new Patient();
            Permission permission = new Permission();
            Manage manage = new Manage(patient);
            permission.AddAuthorization(manage);
            Assert.IsTrue(permission.Can(Actions.Update, patient) == true);
            Assert.IsFalse(permission.Can(Actions.Update, typeof(Patient)) == true);
        }
    }
}

