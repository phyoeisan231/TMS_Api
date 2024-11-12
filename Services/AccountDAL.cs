
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
namespace TMS_Api.Services
{
    public class AccountDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }

        public AccountDAL(TMSDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public static DateTime GetLocalStdDT()
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Myanmar Standard Time");
            return localTime;
        }
       
        public Task<DataTable> GetDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var newCon = new SqlConnection(_conStr))
                using (var adapt = new SqlDataAdapter(sSQL, newCon))
                {
                    newCon.Open();
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    if (para != null)
                        adapt.SelectCommand.Parameters.AddRange(para);

                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    newCon.Close();
                    return dt;
                }
            });
        }

        public async Task<DataTable> GetUserRoles()
        {
            string sql = @"SELECT * FROM AspNetRoles";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

		public async Task<DataTable> GetUsers()
		{
			string sql = @"SELECT u.Id,u.Email, r.Name as UserRole, u.UserName, u.PhoneNumber FROM AspNetUsers u
                            Inner JOIN AspNetUserRoles ar ON ar.UserId = u.Id                           
                            Inner JOIN AspNetRoles r ON r.Id = ar.RoleId";
			DataTable dt = await GetDataTableAsync(sql);
			return dt;
		}
	}
}
