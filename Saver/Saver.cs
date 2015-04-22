using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saver
{
    class Saver:ISaver
    {

         
        OleDbConnection connection;

       /// private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; data source=C:/Users/Ирина Рыжова/Documents/MironovaBase.accdb";

        public Saver(OleDbConnection connection)
        {
            this.connection = connection;
        }

        public int Save(string name, string value)
        {
            String sql="INSERT INTO vars ( name_var, [value_var] )"+
            " VALUES ('" + name + "', '" + value + "');";
            OleDbCommand command = new OleDbCommand(sql, connection);
             int n=0;
            try
            {
                n= command.ExecuteNonQuery();
            }catch(OleDbException)
            {
                sql = "UPDATE vars SET value_var = '" + value+"'" +
                     " WHERE name_var = '" + name+"';";
                command = new OleDbCommand(sql, connection);
                n = command.ExecuteNonQuery();
            }
           

            if(n==0)
            {
                
            }

            return n;
        }

        public string Get(string name, String defaullValue)
        {
            String sql = "SELECT value_var" +
            "FROM vars"+
            "WHERE name_var=" + name + ";";

            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader= command.ExecuteReader();
           
            int i = 0;
            String value = "";
            while (reader.Read())
            {
                value = reader.GetString(0);
                i++;
            }
            reader.Close();

            if (value != "")
                return value;

            return defaullValue;
        }

        public int Delete(string name)
        {
            String sql = "DELETE FROM vars"+
            "WHERE name_var=" + name;
            OleDbCommand command = new OleDbCommand(sql, connection);
            return command.ExecuteNonQuery();
        }
    }
}
