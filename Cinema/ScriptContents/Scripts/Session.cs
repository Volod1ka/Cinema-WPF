using System;

namespace Scripts
{
    public class Session
    {
        #region Variables

        public Film Film { protected set; get; }

        public DateTime SessionData { protected set; get; }

        public TimeSpan SessionTime { protected set; get; }

        public Hall Hall { protected set; get; }

        public float Price { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Session()
        {
            Input(id: 0, film: new Film(), sessionData: DateTime.MinValue, sessionTime: TimeSpan.MinValue, hall: new Hall(), price: 0);
        }

        public Session(uint id, Film film, DateTime sessionData, TimeSpan sessionTime, Hall hall, float price)
        {
            Input(id: id, film: film, sessionData: sessionData, sessionTime: sessionTime, hall: hall, price: price);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, Film film, DateTime sessionData, TimeSpan sessionTime, Hall hall, float price)
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
