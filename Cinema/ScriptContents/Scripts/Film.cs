using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts
{
    public class Film
    {
        #region Variables

        public string Name { protected set; get; }

        public DateTime StartData { protected set; get; }

        public DateTime EndData { protected set; get; }

        public DateTime Duration { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Film()
        {
            Input(id: 0, name: "", startData: DateTime.MinValue, endData: DateTime.MinValue, duration: DateTime.MinValue);
        }

        public Film(uint id, string filmName, DateTime startData, DateTime endData, DateTime duration)
        {
            Input(id: id, name: filmName, startData: startData, endData: endData, duration: duration);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string name, DateTime startData, DateTime endData, DateTime duration)
        {
            Id = id;
            Name = name;
            StartData = startData;
            EndData = endData;
            Duration = duration;
        }

        #endregion
    }
}
