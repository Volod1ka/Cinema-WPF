using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema
{
    internal static class Account
    {
        #region Variables
        
        public static uint Id { get; private set; }

        public static string UserName { get; private set; }
        
        public static string Login { get; private set; }
        
        public static string Password { get; private set; }

        public static bool IsActive { get; private set; }

        #endregion

        #region Events and Delegates

        public delegate void AccountHandler(bool message);

        public static event AccountHandler DiactivateMethod;

        #endregion

        #region Constructors
        
        static Account()
        {
            Clear();
        }

        #endregion

        #region Public Methods

        public static void Input(uint id, string userName, string login, string password, bool isActive)
        {
            Id = id;
            UserName = userName;
            Login = login;
            Password = password;
            IsActive = isActive;
        }

        public static void Clear()
        {
            Input(id: 0, userName: string.Empty, login: string.Empty, password: string.Empty, isActive: false);
        }

        public static void DiactivateAccount(bool message)
        {
            DiactivateMethod?.Invoke(message);
        }

        #endregion
    }
}
