using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts
{
    public class Hall
    {
        #region Variables

        public string Name { protected set; get; }

        public uint Rows { protected set; get; }

        public uint Seats { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Hall()
        {
            Input(id: 0, name: "", rows: 0, seats: 0);
        }

        public Hall(uint id, string name, uint rows, uint seats)
        {
            Input(id: id, name: name, rows: rows, seats: seats);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string name, uint rows, uint seats)
        {
            Id = id;
            Name = name;
            Rows = rows;
            Seats = seats;
        }

        #endregion
    }
}
