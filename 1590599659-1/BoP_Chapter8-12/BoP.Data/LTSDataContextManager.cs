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
            db.LoadOptions = dlo;
        }

        public BoPDataContext GetContext()
        {
            return db;
        
        }

    }


    }


