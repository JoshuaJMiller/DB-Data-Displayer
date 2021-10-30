using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public class DataAccess
    {
        public List<PersonModel> GetPeopleByLastName(string p_lastName)
        {
            // Makes a new IDbConnection based on the connection string that is returned, and closes the connection as soon as function returns
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.getConnectionString("PersonDB")))
            {
                // Dapper Query method that calls stored procedure "People_GetByLastName" from SQL Server which has a parameter "@LastName" p_lastName is passed via inline object initialization (new) 
                List<PersonModel> output = connection.Query<PersonModel>("dbo.People_GetByLastName @LastName", new { LastName = p_lastName }).ToList();
                return output;
            }
        }

        

    }
}
