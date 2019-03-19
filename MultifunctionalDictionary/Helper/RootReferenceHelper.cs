using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultifunctionalDictionary.Models;
using Npgsql;
using System.Diagnostics;

namespace MultifunctionalDictionary.Helper
{
    class RootReferenceHelper
    {
        private NpgsqlConnection connection;

        public RootReferenceHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
    }
}
