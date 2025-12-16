using System.Data;
using Microsoft.Data.SqlClient;

public sealed class LibrosRepository
{
    private readonly string _connectionString = "Data Source=***.database.windows.net;Initial Catalog=***;User ID=***;Password=***;";
    public List<Libro> GetAll()
    {
        List<Libro> result = new List<Libro>();
        const string sql = @"
            SELECT [Id],[Titulo],[Autor],[Editorial],[ISBN] /* comandos sql */
            FROM [YMCA261Libro] 
            ORDER BY [Titulo] ASC";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Libro
            {
                Id = reader.GetGuid(0),
                Titulo = reader.GetString(1),
                Autor = reader.GetString(2),
                Editorial = reader.GetString(3),
                ISBN = reader.GetString(4)
            });
        }
        return result;
    }
    public Libro GetById(Guid id)
    {
        Libro libro = new Libro();
        const string sql = @"
            SELECT [Id],[Titulo],[Autor],[Editorial],[ISBN]
            FROM [YMCA261Libro]
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            libro = new Libro
            {
                Id = reader.GetGuid(0),
                Titulo = reader.GetString(1),
                Autor = reader.GetString(2),
                Editorial = reader.GetString(3),
                ISBN = reader.GetString(4)
            };
        }
        return libro;
    }
    public void Insert(Libro libro)
    {
        const string sql = @"
            INSERT INTO [YMCA261Libro] ([Id],[Titulo],[Autor],[Editorial],[ISBN])
            VALUES (@Id,@Titulo,@Autor,@Editorial,@ISBN)";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = libro.Id });
        cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar, 256) { Value = libro.Titulo });
        cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar, 128) { Value = libro.Autor });
        cmd.Parameters.Add(new SqlParameter("@Editorial", SqlDbType.NVarChar, 128) { Value = libro.Editorial });
        cmd.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar, 64) { Value = libro.ISBN });
        cmd.ExecuteNonQuery();
    }
    public void Update(Libro libro)
    {
        const string sql = @"
            UPDATE [YMCA261Libro] SET [Titulo] = @Titulo, [Autor] = @Autor, [Editorial] = @Editorial, [ISBN] = @ISBN
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = libro.Id });
        cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar, 256) { Value = libro.Titulo });
        cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar, 128) { Value = libro.Autor });
        cmd.Parameters.Add(new SqlParameter("@Editorial", SqlDbType.NVarChar, 128) { Value = libro.Editorial });
        cmd.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.NVarChar, 64) { Value = libro.ISBN });
        cmd.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        const string sql = @"DELETE FROM [YMCA261Libro] WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        cmd.ExecuteNonQuery();
    }
}