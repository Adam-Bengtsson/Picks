using Microsoft.Extensions.Configuration;
using Picks.Infrastructure.DataAccess;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Picks.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private ApplicationDbContext ctx;

        public PictureRepository(ApplicationDbContext context)
        {
            ctx = context;
        }

        public IEnumerable<Picture> Pictures => ctx.Pictures;
        public IEnumerable<Category> Categories => ctx.Categories;

        public void SaveCategory(Category c)
        {
            c.Id = Guid.NewGuid();
            c.CreatedDate = DateTime.Now;
            ctx.Categories.Add(c);
            ctx.SaveChanges();
        }

        public void SavePicture(Picture p)
        {
            p.UploadDate = DateTime.Now;
            ctx.Pictures.Add(p);
            ctx.SaveChanges();
        }

        public IEnumerable<Picture> GetAllPictures()
        {
            return ctx.Pictures;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return ctx.Categories;
        }

        //private IDbConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(_config.GetConnectionString("Picks"));
        //    }
        //}

        //public List<string> GetAll()
        //{
        //    var conn = _config.GetConnectionString("Picks");

        //    List<string> Result = new List<string>();
        //    string Command = "SELECT FileName FROM Pictures ORDER BY UploadDate DESC;";
        //    using (SqlConnection mConnection = new SqlConnection(conn))
        //    {
        //        mConnection.Open();
        //        using (SqlCommand cmd = new SqlCommand(Command, mConnection))
        //        {
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Result.Add((string)reader[0]);
        //                }
        //            }
        //        }
        //    }

        //    string Command2 = "SELECT * FROM Pictures ORDER BY UploadDate DESC;";
        //    using (SqlConnection myConnection = new SqlConnection(conn))
        //    {
        //        using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command2, myConnection))
        //        {
        //            DataTable dtResult = new DataTable();
        //            myDataAdapter.Fill(dtResult);
        //        }
        //    }

        //    return Result;
        //}

        //public void GetAll()
        //{
        //    var conn = _config.GetConnectionString("Picks");

        //    string Command = "SELECT FileName FROM Pictures ORDER BY UploadDate DESC;";
        //    using (SqlConnection myConnection = new SqlConnection(conn))
        //    {
        //        using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, myConnection))
        //        {
        //            DataTable dtResult = new DataTable();
        //            myDataAdapter.Fill(dtResult);
        //        }
        //    }
        //}

        //public async Task<IEnumerable<Picture>> GetAll()
        //{
        //    using (IDbConnection conn = Connection)
        //    {
        //        string query = "SELECT * FROM Pictures ORDER BY UploadDate DESC";
        //        conn.Open();
        //        var result = await conn.QueryAsync<Picture>(query);
        //        return result;
        //    }
        //}

        //public IEnumerable<Picture> GetAll()
        //{
        //    using (IDbConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand command = new SqlCommand(
        //            "SELECT * FROM Pictures ORDER BY UploadDate DESC"))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        Console.WriteLine(reader.GetValue(i));
        //                    }
        //                    Console.WriteLine();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}