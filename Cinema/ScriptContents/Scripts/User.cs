using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts
{
    public class User
    {
        #region Variables

        public string Name { protected set; get; }

        public Job Job { protected set; get; }

        public string Login { protected set; get; }

        public string Password { protected set; get; }

        public bool IsActive { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public User()
        {
            Input(id: 0, name: "", job: new Job(), login: "", password: "", isActive: false);
        }

        public User(uint id, string name, Job job, string login, string password, bool isActive)
        {
            Input(id: id, name: name, job: job, login: login, password: password, isActive: isActive);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string name, Job job, string login, string password, bool isActive)
        {
            Id = id;
            Name = name;
            Job = job;
            Login = login;
            Password = password;
            IsActive = isActive;
        }

        #endregion
    }
}
