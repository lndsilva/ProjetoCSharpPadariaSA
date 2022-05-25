using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace PadariaSA
{
    class Conexao
    {
        private static string connString = "Server=localhost;Database=dbPadariaSA;Uid=senacl13;Pwd='senacl13'";

        private static MySqlConnection conn = null;


        public static MySqlConnection obterConexao()
        {
            conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (MySqlException)
            {
                conn = null;
            }
            return conn;
        }

        public static void fecharConexao()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

    }
}
