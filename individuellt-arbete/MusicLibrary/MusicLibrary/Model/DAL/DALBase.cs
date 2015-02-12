using System.Data.SqlClient;
using System.Web.Configuration;

namespace MusicLibrary.Model.DAL
{
    /// <summary>
    /// Base class for Data Access Logic classes.
    /// </summary>
    public abstract class DALBase
    {
        private static readonly string _connectionString;

        /// <summary>
        /// Creates and return a new SqlConnection instance.
        /// </summary>
        /// <returns>A new SqlConnection instance.</returns>
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["WP13_ba222ec_MusicLibraryConnectionString"].ConnectionString;
        }
    }
}