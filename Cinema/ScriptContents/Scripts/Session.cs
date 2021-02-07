using System;

namespace Scripts
{
    public class Session
    {
        #region Variables

        public string Film { protected set; get; }

        public DateTime SessionData { protected set; get; }

        public DateTime SessionTime { protected set; get; }

        public string Hall { protected set; get; }

        public float Price { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Session()
        {
            Input(0, "", DateTime.MinValue, DateTime.MinValue, "", 0);
        }

        public Session(uint id, string film, DateTime sessionData, DateTime sessionTime, string hall, float price)
        {
            Input(id, film, sessionData, sessionTime, hall, price);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string film, DateTime sessionData, DateTime sessionTime, string hall, float price)
        {
            Id = id;
            Film = film;
            SessionData = sessionData;
            SessionTime = sessionTime;
            Hall = hall;
            Price = price;
        }

        #endregion
    }
}
