using System.Data.SqlClient;
using System.Web.Configuration;

namespace Adventurous_Contacts.Model.DAL
{
    /// <summary>
    /// Abstract base class for all DAL classes in "Adventurous Contacts".
    /// </summary>
    public abstract class DALBase
    {
        private static readonly string _connectionString;

        /// <summary>
        /// Creates an instance SqlConnection.
        /// </summary>
        /// <returns>An instance of SqlConnection.</returns>
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["1dv406_AdventureWorksAssignmentConnectionstring"].ConnectionString;
        }
    }
}