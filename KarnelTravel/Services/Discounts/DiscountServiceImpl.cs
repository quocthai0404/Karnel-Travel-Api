using KarnelTravel.Models;
using Microsoft.Data.SqlClient;

namespace KarnelTravel.Services.Discounts;

public class DiscountServiceImpl : IDiscountService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public DiscountServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public double getPercent(string code)
    {
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

        string query = "SELECT discount_percent FROM discount WHERE discount_code = @code";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@code", code);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToDouble(result);
                }
                else
                {

                    return 0;
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error retrieving discount percent", ex);
            }
        }


    }
}
