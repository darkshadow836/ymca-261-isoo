/*
CREATE TABLE [YMCA261Prestamo] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL,
    [LibroId] UNIQUEIDENTIFIER NOT NULL,
    [FechaPrestamo] DATETIME NOT NULL,
    [FechaDevolucion] DATETIME NULL,
    CONSTRAINT FK_Prestamo_Usuario FOREIGN KEY (UsuarioId) REFERENCES YMCA261Usuario(Id),
    CONSTRAINT FK_Prestamo_Libro FOREIGN KEY (LibroId) REFERENCES YMCA261Libro(Id)
);
*/
public class Prestamo
{
    public Guid Id;
    public Guid UsuarioId; // Relación con Usuario
    public Guid LibroId;   // Relación con Libro
    public DateTime FechaPrestamo;
    public DateTime FechaDevolucion; // Puede ser nullable si aún no se devuelve
}