using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.LTS
{
    /// <summary>
    /// Simple class to implement DataContext singleton.
    /// </summary>
    public sealed class BoPDataContextManager
    {
        private BoPDataContext db;

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  
        /// See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static BoPDataContextManager Instance {
            get {
                return Nested.BoPDataContextManager;
            }
        }

        /// <summary>
        /// Initializes BoPDataContext.
        /// </summary>
        private BoPDataContextManager() {
            InitDB();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly BoPDataContextManager BoPDataContextManager = 
                new BoPDataContextManager();
        }

        #endregion

        private void InitDB() {
            
            db = new BoPDataContext();
            DataLoadOptions dlo = new DataLoadOptions();
            dlo.LoadWith<StakeHolder>(r => r.Roles);
            dlo.LoadWith<StakeHolder>(s => s.Accounts);
            //dlo.LoadWith<Role>(s => s.StakeHolder);
            //dlo.AssociateWith<StakeHolder>(s=>from o in s.Roles where o.StakeHolderId == s.StakeHolderId select s);
            db.LoadOptions = dlo;
        }

        public BoPDataContext GetContext()
        {
            return db;
        
        }

        ///// <summary>
        ///// Allows you to register an interceptor on a new session.  This may not be called if there is already
        ///// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        ///// the HttpModule to call this before calling BeginTransaction().
        ///// </summary>
        //public void RegisterInterceptor(IInterceptor interceptor) {
        //    ISession session = ContextSession;

        //    if (session != null && session.IsOpen) {
        //        throw new CacheException("You cannot register an interceptor once a session has already been opened");
        //    }

        //    GetSession(interceptor);
        //}

        //public ISession GetSession() {
        //    return GetSession(null);
        //}

        ///// <summary>
        ///// Gets a session with or without an interceptor.  This method is not called directly; instead,
        ///// it gets invoked from other public methods.
        ///// </summary>
        //private ISession GetSession(IInterceptor interceptor) {
        //    ISession session = ContextSession;

        //    if (session == null) {
        //        if (interceptor != null) {
        //            session = sessionFactory.OpenSession(interceptor);
        //        }
        //        else {
        //            session = sessionFactory.OpenSession();
        //        }

        //        ContextSession = session;
        //    }

        //    Check.Ensure(session != null, "session was null");

        //    return session;
        //}

        ///// <summary>
        ///// Flushes anything left in the session and closes the connection.
        ///// </summary>
        //public void CloseSession() {
        //    ISession session = ContextSession;

        //    if (session != null && session.IsOpen) {
        //        session.Flush();
        //        session.Close();
        //    }

        //    ContextSession = null;
        //}

        //public void BeginTransaction() {
        //    ITransaction transaction = ContextTransaction;

        //    if (transaction == null) {
        //        transaction = GetSession().BeginTransaction();
        //        ContextTransaction = transaction;
        //    }
        //}

        //public void CommitTransaction() {
        //    ITransaction transaction = ContextTransaction;

        //    try {
        //        if (HasOpenTransaction()) {
        //            transaction.Commit();
        //            ContextTransaction = null;
        //        }
        //    }
        //    catch (HibernateException) {
        //        RollbackTransaction();
        //        throw;
        //    }
        //}

        //public bool HasOpenTransaction() {
        //    ITransaction transaction = ContextTransaction;

        //    return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        //}

        //public void RollbackTransaction() {
        //    ITransaction transaction = ContextTransaction;

        //    try {
        //        if (HasOpenTransaction()) {
        //            transaction.Rollback();
        //        }

        //        ContextTransaction = null;
        //    }
        //    finally {
        //        CloseSession();
        //    }
        //}

        ///// <summary>
        ///// If within a web context, this uses <see cref="HttpContext" /> instead of the WinForms 
        ///// specific <see cref="CallContext" />.  Discussion concerning this found at 
        ///// http://forum.springframework.net/showthread.php?t=572.
        ///// </summary>
        //private ITransaction ContextTransaction {
        //    get {
        //        if (IsInWebContext()) {
        //            return (ITransaction)HttpContext.Current.Items[TRANSACTION_KEY];
        //        }
        //        else {
        //            return (ITransaction)CallContext.GetData(TRANSACTION_KEY);
        //        }
        //    }
        //    set {
        //        if (IsInWebContext()) {
        //            HttpContext.Current.Items[TRANSACTION_KEY] = value;
        //        }
        //        else {
        //            CallContext.SetData(TRANSACTION_KEY, value);
        //        }
        //    }
        //}

        ///// <summary>
        ///// If within a web context, this uses <see cref="HttpContext" /> instead of the WinForms 
        ///// specific <see cref="CallContext" />.  Discussion concerning this found at 
        ///// http://forum.springframework.net/showthread.php?t=572.
        ///// </summary>
        //private ISession ContextSession {
        //    get {
        //        if (IsInWebContext()) {
        //            return (ISession)HttpContext.Current.Items[SESSION_KEY];
        //        }
        //        else {
        //            return (ISession)CallContext.GetData(SESSION_KEY); 
        //        }
        //    }
        //    set {
        //        if (IsInWebContext()) {
        //            HttpContext.Current.Items[SESSION_KEY] = value;
        //        }
        //        else {
        //            CallContext.SetData(SESSION_KEY, value);
        //        }
        //    }
        //}

        //private bool IsInWebContext() {
        //    return HttpContext.Current != null;
        //}

        //private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        //private const string SESSION_KEY = "CONTEXT_SESSION";
        //private ISessionFactory sessionFactory;
    }


    }


