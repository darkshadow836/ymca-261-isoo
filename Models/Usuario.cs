/*
CREATE TABLE [YMCA261Usuario] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [Nombre] NVARCHAR(128) NOT NULL,
    [Correo] NVARCHAR(128) NOT NULL
);
*/
public class Usuario
{
    public Guid Id;
    public string Nombre;
    public string Correo;
}