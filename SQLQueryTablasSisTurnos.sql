Create DATABASE DBSISTEMA_TURNOS

go

use DBSISTEMA_TURNOS

go


create table ROL(
IdRol int primary key identity,
Descripcion varchar(50),
FechaRegistro datetime default getdate()
)

go

create table PERMISO(
IdPermiso int primary key identity,
IdRol int references ROL(IdRol),
NombreMenu varchar(100),
FechaRegistro datetime default getdate()
)

go

create table PACIENTE(
IdPaciente int primary key identity,
Documento varchar(50),
NombreCompleto varchar(50),
Correo varchar(50),
Direccion varchar(50),
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getdate()
)

GO
create table ESPECIALIDAD(
IdEspecialidad int primary key identity,
Descripcion varchar(100),
Estado bit,
FechaRegistro datetime default getdate()
)

GO
create table MEDICO(
IdMedico int primary key identity,
Documento varchar(50),
NombreCompleto varchar(50),
IdEspecialidad int references ESPECIALIDAD(IdEspecialidad),
Correo varchar(50),
Direccion varchar(50),
Telefono varchar(50),
NumeroMatricula varchar(50),
Estado bit,
FechaRegistro datetime default getdate()
)

GO

create table USUARIO (
IdUsuario int primary key identity,
Documento varchar(50),
NombreCompleto varchar(50),
Correo varchar(50),
Clave varchar(50),
IdRol int references ROL(IdRol),
Estado bit,
FechaRegistro datetime default getdate()
)

GO

create table TURNO (
IdTurno int primary key identity,
IdUsuario int references USUARIO(IdUsuario),
IdPaciente int references PACIENTE(IdPaciente),
NumeroDocumento varchar(50),
FechaRegistro datetime default getdate()
)

Go

Create TABLE DETALLE_TURNO(
IdDetalleTurno int primary key identity,
IdTurno int references TURNO(IdTurno),
IdUsuario int references USUARIO(IdUsuario),
IdPaciente int references PACIENTE(IdPaciente),
IdMedico int references MEDICO(IdMedico),
NumeroDocumento varchar(50),
FechaCreacion datetime default getdate()
)



create table HISTORIA_CLINICA(
IdEvento int primary key identity,
Fecha varchar(50),
Hora varchar(50),
IdPaciente varchar(50),
Paciente varchar(50),
Recordatorio varchar(50),
Evolucion varchar(50),
Prescripcion varchar(50),
IdUsuario varchar(50),
Usuario varchar(50),
Evento varchar(50),
Detalle varchar(50),
Origen varchar(50)
)


go