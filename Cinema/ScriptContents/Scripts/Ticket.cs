using System;

namespace Scripts
{
    public class Ticket
    {
        #region Variables

        public DateTime DataCreate { protected set; get; }

        public Session Session { protected set; get; }

        public uint Row { protected set; get; }

        public uint Seat { protected set; get; }

        public bool IsPaid { protected set; get; }

        public bool IsToBook { protected set; get; }

        public uint Id { protected set; get; }

        #endregion

        #region Constructors

        public Ticket()
        {
            Input(id: 0, dataCreate: DateTime.MinValue, session: new Session(), row: 0, seat: 0);
        }

        public Ticket(uint id, DateTime dataCreate, Session session, uint row, uint seat, bool isPaid = false, bool isToBook = false)
        {
            Input(id: id, dataCreate: dataCreate, session: session, row: row, seat: seat, isPaid, isToBook);
        }

        #endregion

        #region Public Methods

        public void Input(uint id, DateTime dataCreate, Session session, uint row, uint seat, bool isPaid = false, bool isToBook = false)
        {
            Id = id;
            DataCreate = dataCreate;
            Session = session;
            Row = row;
            Seat = seat;
            IsPaid = isPaid;
            IsToBook = isToBook;
        }

        #endregion
    }
}
