using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Scripts.Data
{
    static public class DataBaseConnection
    {
        #region Variables
        
        private static string connectString;
        
        public static MySqlConnection DBSqlConnection { private set; get; }

        public static bool isConnectionCorrect
        {
            get
            {
                bool isCorrect = false;

                try
                {
                    isCorrect = ConnectionOpen();
                    ConnectionClose();
                }
                catch (Exception ex)
                {
                    isCorrect = false;
                    LogFile.Log($"{ex}", "Error");
                }

                return isCorrect;
            }
        }

        #endregion

        #region Constructors

        static DataBaseConnection()
        {
        }

        #endregion

        #region Public Methods

        public static void SetConnectingString(string ConnectString)
        {
            try
            {
                connectString = ConnectString;
                DBSqlConnection = new MySqlConnection(connectString);
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
        }

        public static bool ConnectionOpen()
        {
            bool successful = true;

            try
            {
                DBSqlConnection.Open();

                successful = DBSqlConnection.State.Equals(ConnectionState.Open);
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
                successful = false;
            }

            return successful;
        }

        public static bool ConnectionClose()
        {
            bool successful = true;

            try
            {
                DBSqlConnection.Close();

                successful = DBSqlConnection.State.Equals(ConnectionState.Closed);
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
                successful = false;
            }

            return successful;
        }

        #endregion
    }
}
