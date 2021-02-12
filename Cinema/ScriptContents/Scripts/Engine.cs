using System;
using System.Collections.Generic;
using System.Threading;

namespace Scripts
{
    public static class Engine
    {
        public enum Status
        {
            Create = 1,
            Update = 2
        }

        public static void RefreshDataTickets(out List<Ticket> tickets)
        {
            List<Ticket> New = new List<Ticket>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListTickets();
            });

            tickets = New;
        }

        public static void RefreshDataSessions(out List<Session> sessions)
        {
            List<Session> New = new List<Session>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListSessions();
            });

            sessions = New;
        }

        public static void RefreshDataFilms(out List<Film> films)
        {
            List<Film> New = new List<Film>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListFilms();
            });

            films = New;
        }

        public static void RefreshDataHalls(out List<Hall> halls)
        {
            List<Hall> New = new List<Hall>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListHalls();
            });

            halls = New;
        }

        public static void RefreshDataEmployees(out List<User> employees)
        {
            List<User> New = new List<User>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListUsers();
            });

            employees = New;
        }

        public static void RefreshDataJobs(out List<Job> jobs)
        {
            List<Job> New = new List<Job>();

            RefreshData(() =>
            {
                New = Scripts.Data.DataBaseManager.GetListJobs();
            });

            jobs = New;
        }

        public static void RefreshData(Action func)
        {
            Thread STAThread = new Thread(
                delegate ()
                {
                    func();
                });
            STAThread.Start();
            STAThread.Join();
        }

        public static T RefreshDataGrid<T>(Func<T> func)
        {
            T value = default;

            Thread STAThread = new Thread(
                delegate ()
                {
                    value = func();
                });
            STAThread.Start();
            STAThread.Join();

            return value;
        }
    }
}
