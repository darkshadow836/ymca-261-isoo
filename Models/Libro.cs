/*
CREATE TABLE [YMCA261Libro](
	[Id] [uniqueidentifier] PRIMARY KEY NOT NULL,
	[Titulo] [nvarchar](256) NOT NULL,
	[Autor] [nvarchar](128) NOT NULL,
	[Editorial] [nvarchar](128) NOT NULL,
	[ISBN] [nvarchar](64) NOT NULL
);
*/
public class Libro
{
    public Guid Id;
    public string Titulo;
    public string Autor;
    public string Editorial;
    public string ISBN;
}