﻿using System;
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

        public String importRootReference(int rootReferenceNum, int childReferenceNum)
        {
            String result;

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT importRootReference FROM importRootReference({0},{1})", rootReferenceNum, childReferenceNum), connection))
            {
                result = (String)cmd.ExecuteScalar();
            }

            return result;
        }
    }
}
