using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Cinema.Scripts.DBase
{
    static class DataBaseConnection
    {
        #region Variables
        
        private static string connectString;
        
        public static MySqlConnection DBSqlConnection;

        public static bool isConnectionCorrect
        {
            get
            {
                bool isCorrect = ConnectionOpen();
                ConnectionClose();

                return isCorrect;
            }
        }

        #endregion

        #region Constructors

        static DataBaseConnection()
        {
            try
            {
                connectString = Properties.Resources.ConnectString;
                DBSqlConnection = new MySqlConnection(connectString);
            }
            catch (Exception ex)
            {
                LogFile.Log($"{ex}", "Error");
            }
        }

        #endregion

        #region Public Methods

        public static void SetConnectingString(string ConnectString)
        {
            connectString = ConnectString;
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
