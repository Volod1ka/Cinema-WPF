using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts
{
    public class Job
    {
        #region Variables

        public string Name { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Job()
        {
            Input(id: 0, name: "");
        }

        public Job(uint id, string name)
        {
            Input(id: id, name: name);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
