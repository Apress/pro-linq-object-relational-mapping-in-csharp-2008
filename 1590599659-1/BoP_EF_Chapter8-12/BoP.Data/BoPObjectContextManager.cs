using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using BoP.Core.Domain;
using BoP.Core.DataInterfaces;

namespace BoP.Data.EF
{
    /// <summary>
    /// Simple class to implement DataContext singleton.
    /// </summary>
    public sealed class BoPObjectContextManager
    {
        private BoPObjectContext db;

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, lazy singleton.  
        /// See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static BoPObjectContextManager Instance
        {
            get
            {
                return Nested.BoPObjectContextManager;
            }
        }

        /// <summary>
        /// Initializes BoPDataContext.
        /// </summary>
        private BoPObjectContextManager()
        {
            InitDB();
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly BoPObjectContextManager BoPObjectContextManager =
                new BoPObjectContextManager();
        }

        #endregion

        private void InitDB()
        {

            db = new BoPObjectContext();

        }

        public BoPObjectContext GetContext()
        {
            return db;

        }

    }


}


