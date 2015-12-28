using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using Beaker.Services;
using System.Runtime.CompilerServices;
using Beaker.Authorize;

[assembly: InternalsVisibleTo("Beaker.Initialization.Test")]
namespace Beaker.Initialization
{
    
    /// <summary>
    /// Adds the initial root user to the system. Reserved for initialization only.
    /// </summary>
    internal class AddSuperUserCommand
    {
        private string Username { get; set; }
        private string EmailAddress { get; set; }
        private string Password { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        internal AddSuperUserCommand(string emailAddress, string password, IUnitOfWork unitOfWork)
        {
            if (string.IsNullOrEmpty(emailAddress)) throw new ArgumentNullException("emailAddress");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            
            this.Username = "root";
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.UnitOfWork = unitOfWork;
        }

        public void Run()
        {
            User user = UnitOfWork.Create<User>();
            user.Username = this.Username;
            user.EmailAddress = this.EmailAddress;
            user.Password = this.Password;
            UnitOfWork.Save<User>(user);
            UserPermission userPermission = UnitOfWork.Create<UserPermission>();
            userPermission.AddAuthorization(Actions.Manage);
            UnitOfWork.Save<UserPermission>(userPermission);
        }
    }
}
