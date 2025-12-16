using System.Data;
using Microsoft.Data.SqlClient;

public sealed class UsuariosRepository
{
    private readonly string _connectionString = "Data Source=***.database.windows.net;Initial Catalog=***;User ID=***;Password=***;";

    public List<Usuario> GetAll()
    {
        List<Usuario> result = new List<Usuario>();
        const string sql = @"
            SELECT [Id],[Nombre],[Correo]
            FROM [YMCA261Usuario]
            ORDER BY [Nombre] ASC";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Usuario
            {
                Id = reader.GetGuid(0),
                Nombre = reader.GetString(1),
                Correo = reader.GetString(2)
            });
        }
        return result;
    }

    public Usuario GetById(Guid id)
    {
        Usuario usuario = new Usuario();
        const string sql = @"
            SELECT [Id],[Nombre],[Correo]
            FROM [YMCA261Usuario]
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            usuario = new Usuario
            {
                Id = reader.GetGuid(0),
                Nombre = reader.GetString(1),
                Correo = reader.GetString(2)
            };
        }
        return usuario;
    }

    public void Insert(Usuario usuario)
    {
        const string sql = @"
            INSERT INTO [YMCA261Usuario] ([Id],[Nombre],[Correo])
            VALUES (@Id,@Nombre,@Correo)";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = usuario.Id });
        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 128) { Value = usuario.Nombre });
        cmd.Parameters.Add(new SqlParameter("@Correo", SqlDbType.NVarChar, 128) { Value = usuario.Correo });
        cmd.ExecuteNonQuery();
    }

    public void Update(Usuario usuario)
    {
        const string sql = @"
            UPDATE [YMCA261Usuario] SET [Nombre] = @Nombre, [Correo] = @Correo
            WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = usuario.Id });
        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 128) { Value = usuario.Nombre });
        cmd.Parameters.Add(new SqlParameter("@Correo", SqlDbType.NVarChar, 128) { Value = usuario.Correo });
        cmd.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        const string sql = @"DELETE FROM [YMCA261Usuario] WHERE [Id] = @Id";
        using var cn = new SqlConnection(_connectionString);
        cn.Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        cmd.ExecuteNonQuery();
    }
}