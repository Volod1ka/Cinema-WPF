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
            NotFound = 404,
            LoginExists = 405
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

        private static void CheckAccess(Action action)
        {
            try
            {
                if (IsActiveUser != Status.OK)
                {
                    Account.DiactivateAccount(message: true);
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    action();
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
                        bool isAdmin = uint.Parse(dataReader[2].ToString()) == 1;

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

        public static Status Registration(User user)
        {
            Status result = Status.Forbidden;

            try
            {
                if (LoginExists(user.Login))
                {
                    return Status.LoginExists;
                }
                else if (DataBaseConnection.ConnectionOpen())
                {
                    string query = "INSERT INTO `employees`(`name_employee`, `job_post`, `login`, `password`, `is_active`) VALUES (@n, @j, @l, @p, @a)";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("n", user.Name);
                    sqlCommand.Parameters.AddWithValue("j", user.Job.Id);
                    sqlCommand.Parameters.AddWithValue("l", user.Login);
                    sqlCommand.Parameters.AddWithValue("p", user.Password);
                    sqlCommand.Parameters.AddWithValue("a", user.IsActive);

                    result = sqlCommand.ExecuteNonQuery() > 0 ? Status.OK : Status.Forbidden;
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

        public static Status UpdateEmployee(User user)
        {
            Status result = Status.Forbidden;

            try
            {
                if (DataBaseConnection.ConnectionOpen())
                {
                    string query =
                    @"
                    UPDATE employees
                    SET name_employee = @n, job_post = @j, login = @l, password = @p, is_active = @a
                    WHERE id_employee = @id
                    ";

                    MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                    sqlCommand.Parameters.AddWithValue("n", user.Name);
                    sqlCommand.Parameters.AddWithValue("j", user.Job.Id);
                    sqlCommand.Parameters.AddWithValue("l", user.Login);
                    sqlCommand.Parameters.AddWithValue("p", user.Password);
                    sqlCommand.Parameters.AddWithValue("a", user.IsActive);
                    sqlCommand.Parameters.AddWithValue("id", user.Id);

                    result = sqlCommand.ExecuteNonQuery() > 0 ? Status.OK : Status.Forbidden;
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

        public static List<Ticket> GetListTickets()
        {
            List<Ticket> tickets = new List<Ticket>();

            CheckAccess(() =>
            {
                string query =
                @"
                SELECT T.id_ticket, T.data_create, S.id_session, H.id_hall, H.hall_name, H.count_rows, H.count_seats, S.session_data, S.session_time, F.id_film, F.film_name, F.duration, F.start_data, F.end_data, S.price, St.row, St.seat, T.is_paid, T.is_to_book
                FROM tickets AS T
                INNER JOIN sessions AS S ON T.id_session = S.id_session
                INNER JOIN seats AS St ON T.id_seat = St.id_place
                INNER JOIN films AS F ON S.id_film = F.id_film
                INNER JOIN halls AS H ON S.id_hall = H.id_hall
                ";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    tickets.Add(new Ticket
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            dataCreate: DateTime.Parse(dataReader[1].ToString()),
                            session: new Session
                            (
                                id: uint.Parse(dataReader[2].ToString()),
                                hall: new Hall
                                (
                                    id: uint.Parse(dataReader[3].ToString()),
                                    name: dataReader[4].ToString(),
                                    rows: uint.Parse(dataReader[5].ToString()),
                                    seats: uint.Parse(dataReader[6].ToString())
                                ),
                                sessionData: DateTime.Parse(dataReader[7].ToString()),
                                sessionTime: DateTime.Parse(dataReader[8].ToString()),
                                film: new Film
                                (
                                    id: uint.Parse(dataReader[9].ToString()),
                                    filmName: dataReader[10].ToString(),
                                    duration: DateTime.Parse(dataReader[11].ToString()),
                                    startData: DateTime.Parse(dataReader[12].ToString()),
                                    endData: DateTime.Parse(dataReader[13].ToString())
                                ),
                                price: float.Parse(dataReader[14].ToString())
                            ),
                            row: uint.Parse(dataReader[15].ToString()),
                            seat: uint.Parse(dataReader[16].ToString()),
                            isPaid: bool.Parse(dataReader[17].ToString()),
                            isToBook: bool.Parse(dataReader[18].ToString())
                        ));
                }

                dataReader.Close();
            });
            
            return tickets;
        }

        public static List<Session> GetListSessions()
        {
            List<Session> sessions = new List<Session>();

            CheckAccess(() =>
            {
                string query =
                @"
                SELECT S.id_session, S.session_data, S.session_time, h.id_hall, H.hall_name, H.count_rows, H.count_seats, F.id_film, F.film_name, F.duration, F.start_data, F.end_data, S.price
                FROM `sessions` AS S
                INNER JOIN `halls` AS H ON S.id_hall = H.id_hall
                INNER JOIN `films` AS F ON S.id_film = F.id_film
                ";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    sessions.Add(new Session
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            sessionData: DateTime.Parse(dataReader[1].ToString()),
                            sessionTime: DateTime.Parse(dataReader[2].ToString()),
                            hall: new Hall
                            (
                                id: uint.Parse(dataReader[3].ToString()),
                                name: dataReader[4].ToString(),
                                rows: uint.Parse(dataReader[5].ToString()),
                                seats: uint.Parse(dataReader[6].ToString())
                            ),
                            film: new Film
                            (
                                id: uint.Parse(dataReader[7].ToString()),
                                filmName: dataReader[8].ToString(),
                                duration: DateTime.Parse(dataReader[9].ToString()),
                                startData: DateTime.Parse(dataReader[10].ToString()),
                                endData: DateTime.Parse(dataReader[11].ToString())
                            ),
                            price: float.Parse(dataReader[12].ToString())
                        ));
                }

                dataReader.Close();
            });

            return sessions;
        }

        public static List<Film> GetListFilms()
        {
            List<Film> films = new List<Film>();

            CheckAccess(() =>
            {
                string query = @"SELECT * FROM `films`";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    films.Add(new Film
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            filmName: dataReader[1].ToString(),
                            duration: DateTime.Parse(dataReader[2].ToString()),
                            startData: DateTime.Parse(dataReader[3].ToString()),
                            endData: DateTime.Parse(dataReader[2].ToString())
                        ));
                }

                dataReader.Close();
            });

            return films;
        }

        public static List<Hall> GetListHalls()
        {
            List<Hall> halls = new List<Hall>();

            CheckAccess(() =>
            {
                string query = @"SELECT * FROM `halls`";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    halls.Add(new Hall
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            name: dataReader[1].ToString(),
                            rows: uint.Parse(dataReader[2].ToString()),
                            seats: uint.Parse(dataReader[3].ToString())
                        ));
                }

                dataReader.Close();
            });

            return halls;
        }

        public static List<User> GetListUsers()
        {
            List<User> users = new List<User>();

            CheckAccess(() =>
            {
                string query =
                @"
                SELECT E.`id_employee`, E.`name_employee`, E.`job_post`, J.name_job_post, E.`login`, E.`password`, E.`is_active`
                FROM `employees` AS E
                INNER JOIN `employees_job_post` AS J ON E.job_post = J.id_job_post
                ";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    users.Add(new User
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            name: dataReader[1].ToString(),
                            job: new Job
                            (
                                id: uint.Parse(dataReader[2].ToString()),
                                name: dataReader[3].ToString()
                            ),
                            login: dataReader[4].ToString(),
                            password: dataReader[5].ToString(),
                            isActive: bool.Parse(dataReader[6].ToString())
                        ));
                }

                dataReader.Close();
            });

            return users;
        }

        public static List<Job> GetListJobs()
        {
            List<Job> jobs = new List<Job>();

            CheckAccess(() => 
            {
                string query = @"SELECT `id_job_post`, `name_job_post` FROM `employees_job_post`";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    jobs.Add(new Job
                        (
                            id: uint.Parse(dataReader[0].ToString()),
                            name: dataReader[1].ToString()
                        ));
                }
            });

            return jobs;
        }

        public static bool UpdateTicket(uint ticket, bool isPaid = false, bool isToBook = false)
        {
            bool result = false;

            CheckAccess(() =>
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
            });

            return result;
        }

        public static bool SellTicket(uint ticket)
        {
            bool result = false;

            CheckAccess(() =>
            {
                string query = "INSERT INTO tickets_sale (id_ticket, data_sell, id_employee) VALUES(@idT, @dS, @idE)";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                sqlCommand.Parameters.AddWithValue("idT", ticket);
                sqlCommand.Parameters.AddWithValue("dS", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("idE", Account.Id);

                result = sqlCommand.ExecuteNonQuery() > 0;
            });

            return result;
        }

        public static bool BackTicket(uint ticket)
        {
            bool result = false;

            CheckAccess(() =>
            {
                string query = "DELETE FROM tickets_sale WHERE id_ticket = @idT";

                MySqlCommand sqlCommand = new MySqlCommand(query, DataBaseConnection.DBSqlConnection);

                sqlCommand.Parameters.AddWithValue("idT", ticket);

                result = sqlCommand.ExecuteNonQuery() > 0;
            });

            return result;
        }

        #endregion
    }
}