using System.Data;
using System.Data.SqlClient;

public static class DB
{
    public static string baglanti = @"Server=localhost\SQLEXPRESS;Database=ceyda.db;Trusted_Connection=True;";

    
    public static DataTable calistir(string sql)
    {
        using (SqlConnection con = new SqlConnection(baglanti))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public static int komutCalistir(string sql, params SqlParameter[] p)
    {
        using (SqlConnection con = new SqlConnection(baglanti))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (p != null && p.Length > 0)
                    cmd.Parameters.AddRange(p);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
