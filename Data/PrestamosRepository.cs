using System.Data;
using Microsoft.Data.SqlClient;

public sealed class PrestamosRepository
{
    private readonly string _connectionString = "Data Source=***.database.windows.net;Initial Catalog=***;User ID=***;Password=***;";
    
    public List<Prestamo> GetAll()
    {
        List<Prestamo> result = new List<Prestamo>();
        const string sql = @"
            SELECT [Id],[UsuarioId],[LibroId],[FechaPrestamo],[FechaDevolucion]
            FROM [YMCA261Prestamo] 
            ORDER BY [FechaPrestamo] DESC";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Prestamo
            {
                Id = reader.GetGuid(0),
                UsuarioId = reader.GetGuid(1),
                LibroId = reader.GetGuid(2),
                FechaPrestamo = reader.GetDateTime(3),
                FechaDevolucion = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
            });
        }
        return result;
    }

    public Prestamo GetById(Guid id)
    {
        Prestamo prestamo = new Prestamo();
        const string sql = @"
            SELECT [Id],[UsuarioId],[LibroId],[FechaPrestamo],[FechaDevolucion]
            FROM [YMCA261Prestamo]
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            prestamo = new Prestamo
            {
                Id = reader.GetGuid(0),
                UsuarioId = reader.GetGuid(1),
                LibroId = reader.GetGuid(2),
                FechaPrestamo = reader.GetDateTime(3),
                FechaDevolucion = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
            };
        }
        return prestamo;
    }

    public void Insert(Prestamo prestamo)
    {
        const string sql = @"
            INSERT INTO [YMCA261Prestamo] ([Id],[UsuarioId],[LibroId],[FechaPrestamo],[FechaDevolucion])
            VALUES (@Id,@UsuarioId,@LibroId,@FechaPrestamo,@FechaDevolucion)";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = prestamo.Id });
        cmd.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.UniqueIdentifier) { Value = prestamo.UsuarioId });
        cmd.Parameters.Add(new SqlParameter("@LibroId", SqlDbType.UniqueIdentifier) { Value = prestamo.LibroId });
        cmd.Parameters.Add(new SqlParameter("@FechaPrestamo", SqlDbType.DateTime) { Value = prestamo.FechaPrestamo });
        cmd.Parameters.Add(new SqlParameter("@FechaDevolucion", SqlDbType.DateTime) { Value = prestamo.FechaDevolucion == DateTime.MinValue ? (object)DBNull.Value : prestamo.FechaDevolucion });
        cmd.ExecuteNonQuery();
    }

    public void Update(Prestamo prestamo)
    {
        const string sql = @"
            UPDATE [YMCA261Prestamo] 
            SET [UsuarioId] = @UsuarioId, [LibroId] = @LibroId, [FechaPrestamo] = @FechaPrestamo, [FechaDevolucion] = @FechaDevolucion
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = prestamo.Id });
        cmd.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.UniqueIdentifier) { Value = prestamo.UsuarioId });
        cmd.Parameters.Add(new SqlParameter("@LibroId", SqlDbType.UniqueIdentifier) { Value = prestamo.LibroId });
        cmd.Parameters.Add(new SqlParameter("@FechaPrestamo", SqlDbType.DateTime) { Value = prestamo.FechaPrestamo });
        cmd.Parameters.Add(new SqlParameter("@FechaDevolucion", SqlDbType.DateTime) { Value = prestamo.FechaDevolucion == DateTime.MinValue ? (object)DBNull.Value : prestamo.FechaDevolucion });
        cmd.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        const string sql = @"DELETE FROM [YMCA261Prestamo] WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        cmd.ExecuteNonQuery();
    }
}