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
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.getConnectionString("PersonDB")))
            {
                // Dapper Query method that calls stored procedure "People_GetByLastName" from SQL Server which has a parameter "@LastName" p_lastName is passed via inline object initialization (new) 
                List<PersonModel> output = connection.Query<PersonModel>("dbo.People_GetByLastName @LastName", new { LastName = p_lastName }).ToList();
                return output;
            }
        }

        public List<PersonModel> GetPeople()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.getConnectionString("PersonDB")))
            {
                List<PersonModel> output = connection.Query<PersonModel>("dbo.Get_All").ToList();
                return output;
            }
        }

        public List<PersonModel> GetPeopleByAnyValue(string p_value)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.getConnectionString("PersonDB")))
            {
                List<PersonModel> people = connection.Query<PersonModel>("dbo.People_GetByAnyValue @AnyValue", new { AnyValue = p_value }).ToList();
                return people;
            }
        }

        public void InsertPerson(string p_firstName = "", string p_lastName = "", string p_emailAddress = "", string p_phoneNumber = "")
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.getConnectionString("PersonDB")))
            {
                PersonModel person = new PersonModel { FirstName = p_firstName, LastName = p_lastName, EmailAddress = p_emailAddress, PhoneNumber = p_phoneNumber };
                connection.Execute("dbo.People_Insert @FirstName, @LastName, @EmailAddress, @PhoneNumber", person);
            }
        }
    }
}
