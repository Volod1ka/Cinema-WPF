using System;

namespace Cinema.Scripts
{
    public class Ticket
    {
        #region Variables

        public string Film {protected set; get; }

        public DateTime SessionData { protected set; get; }

        public DateTime SessionTime { protected set; get; }

        public string Hall { protected set; get; }

        public uint Row { protected set; get; }

        public uint Seat { protected set; get; }

        public float Price { protected set; get; }

        public bool IsPaid { protected set; get; }

        public bool IsToBook { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Ticket()
        {
            Input(0, "", DateTime.MinValue, DateTime.MinValue, "", 0, 0, 0);
        }

        public Ticket(uint id, string film, DateTime sessionData, DateTime sessionTime, string hall, uint row, uint seat, float price, bool isPaid = false, bool isToBook = false)
        {
            Input(id, film, sessionData, sessionTime, hall, row, seat, price, isPaid, isToBook);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, string film, DateTime sessionData, DateTime sessionTime, string hall, uint row, uint seat, float price, bool isPaid = false, bool isToBook = false)
        {
            Id = id;
            Film = film;
            SessionData = sessionData;
            SessionTime = sessionTime;
            Hall = hall;
            Row = row;
            Seat = seat;
            Price = price;
            IsPaid = isPaid;
            IsToBook = isToBook;
        }

        #endregion
    }
}
