using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Scripts.Data
{
    static public class DataBaseManager
    {
        #region Variables

        public enum Status
        {
            OK = 200,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404
        }

        public static Status IsActiveUser
        {
            get
            {
                Status result = Status.NotFound;

                try
                {
                    if (DataBaseConnection.ConnectionOpen())
                    {
                        string query = "SELECT `is_active` FROM `employees` WHERE `login`=@uL";

                        MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                        sqlCommand.Parameters.AddWithValue("uL", Account.Login);

                        MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                        while (dataReader.Read() && result.Equals(Status.NotFound))
                        {
                            result = bool.Parse(dataReader[0].ToString()) ? Status.OK : Status.Unauthorized;
                        }

                        dataReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    LogFile.Log($"{ex}", "Error");

                    result = Status.Forbidden;
                }
                finally
                {
                    DataBaseConnection.ConnectionClose();
                }

                return result;
            }
        }

        #endregion

        #region Public Methods

        public static Status SingIn(string userLogin, string userPassword)
        {
            Status result = Status.NotFound;

            try
            {
                if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "SELECT* FROM `employees` WHERE `login`=@uL AND `password`=@uP";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("uL", userLogin);
                    sqlCommand.Parameters.AddWithValue("uP", userPassword);

                    MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read() && result.Equals(Status.NotFound))
                    {
                        bool isAdmin = uint.Parse(dataReader[0].ToString()) == 1;

                        Account.Input(id: uint.Parse(dataReader[0].ToString()), userName: dataReader[1].ToString(), login: dataReader[3].ToString(), password: dataReader[4].ToString(), isActive: bool.Parse(dataReader[5].ToString()), isAdmin: isAdmin);

                        result = Account.IsActive ? Status.OK : Status.Unauthorized;
                    }

                    dataReader.Close();
                }
            }
            catch(Exception ex)
            {
                LogFile.Log($"{ex}", "Error");

                result = Status.Forbidden;
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return result;
        }

        public static bool LoginExists(string userLogin)
        {
            bool result = false;

            try
            {
                if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "SELECT COUNT(*) FROM `employees` WHERE `login`=@uL";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("uL", userLogin);

                    MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read() && result.Equals(false))
                    {
                        result = int.Parse(dataReader[0].ToString()) > 0;
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return result;
        }

        public static void Registration(string userName, int userJob, string userLogin, string userPassword, bool isActive = false)
        {
            try
            {
                if (DataBaseConnection.ConnectionOpen())
                {
                    if (LoginExists(userLogin))
                    {
                        return;
                    }

                    string query = "INSERT INTO `employees`(`name_employee`, `job_post`, `login`, `password`, `is_active`) VALUES (@uN, @uJ, @uL, @uP, @uAct)";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("uN", userName);
                    sqlCommand.Parameters.AddWithValue("uJ", userJob);
                    sqlCommand.Parameters.AddWithValue("uL", userLogin);
                    sqlCommand.Parameters.AddWithValue("uP", userPassword);
                    sqlCommand.Parameters.AddWithValue("uAct", isActive);

                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }
        }
    
        public static List<Ticket> GetListTickets()
        {
            List<Ticket> tickets = new List<Ticket>();

            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query =
                    @"  SELECT F.film_name, S.session_data, S.session_time, H.hall_name, St.row, St.seat, S.price, T.is_paid, T.is_to_book, T.id_ticket
                        FROM tickets AS T
                        INNER JOIN sessions AS S ON T.id_session = S.id_session
                        INNER JOIN seats AS St ON T.id_seat = St.id_place
                        INNER JOIN films AS F ON S.id_film = F.id_film
                        INNER JOIN halls AS H ON S.id_hall = H.id_hall";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                    MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        tickets.Add(new Ticket
                            (
                                id: uint.Parse(dataReader[9].ToString()),
                                film: dataReader[0].ToString(),
                                sessionData: DateTime.Parse(dataReader[1].ToString()),
                                sessionTime: DateTime.Parse(dataReader[2].ToString()),
                                hall: dataReader[3].ToString(),
                                row: uint.Parse(dataReader[4].ToString()),
                                seat: uint.Parse(dataReader[5].ToString()),
                                price: float.Parse(dataReader[6].ToString()),
                                isPaid: bool.Parse(dataReader[7].ToString()),
                                isToBook: bool.Parse(dataReader[8].ToString())
                            ));
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return tickets;
        }

        public static List<Session> GetListSessions()
        {
            List<Session> sessions = new List<Session>();

            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query =
                    @"  SELECT F.film_name, S.session_data, S.session_time, H.hall_name, S.price, S.id_session FROM `sessions` AS S
                        INNER JOIN `halls` AS H ON S.id_hall = H.id_hall
                        INNER JOIN `films` AS F ON S.id_film = F.id_film";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                    MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        sessions.Add(new Session
                            (
                                id: uint.Parse(dataReader[5].ToString()),
                                film: dataReader[0].ToString(),
                                sessionData: DateTime.Parse(dataReader[1].ToString()),
                                sessionTime: DateTime.Parse(dataReader[2].ToString()),
                                hall: dataReader[3].ToString(),
                                price: float.Parse(dataReader[4].ToString())
                            ));
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return sessions;
        }

        public static bool UpdateTicket(uint ticket, bool isPaid = false, bool isToBook = false)
        {
            bool result = false;

            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "UPDATE tickets SET is_paid = @p, is_to_book = @b WHERE id_ticket = @idT";

                    if (isPaid)
                    {
                        query = $"{query} AND is_paid NOT IN(@p)";
                    }

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("p", isPaid);
                    sqlCommand.Parameters.AddWithValue("b", isToBook);
                    sqlCommand.Parameters.AddWithValue("idT", ticket);

                    result = sqlCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return result;
        }

        public static bool SellTicket(uint ticket)
        {
            bool result = false;

            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "INSERT INTO tickets_sale (id_ticket, data_sell, id_employee) VALUES(@idT, @dS, @idE)";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("idT", ticket);
                    sqlCommand.Parameters.AddWithValue("dS", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("idE", Account.Id);

                    result = sqlCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return result;
        }

        public static bool BackTicket(uint ticket)
        {
            bool result = false;

            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "DELETE FROM tickets_sale WHERE id_ticket = @idT";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("idT", ticket);

                    result = sqlCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
            finally
            {
                DataBaseConnection.ConnectionClose();
            }

            return result;
        }

        #endregion
    }
}
