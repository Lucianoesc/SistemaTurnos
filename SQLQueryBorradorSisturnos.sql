


select * from USUARIO

SELECT * FROM ROL

--insert into rol (Descripcion)
--values ('ADMINISTRADOR')

--insert into rol (Descripcion)
--values ('DOCTOR')

--insert into rol (Descripcion)
--values ('MEDICO')

--DELETE FROM ROL WHERE Descripcion = 'EMPLEADO'

UPDATE ROL SET Descripcion = 'ADMINISTRATIVO' WHERE IdRol= 2

--insert into rol (Descripcion)

--values ('ADMINISTRADOR')

--insert into rol (Descripcion)
--values ('EMPLEADO')

--insert into USUARIO(Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
--values

--('101010', 'Admin', 'admin@gmail.com', '123',1,1)


--insert into PERMISO(IdRol, NombreMenu) values
--(1,'btnPacientes'),
--(1,'btnMedicos'),
--(1,'btnTurnos'),
--(1,'btnHistoriaClinica'),
--(1,'btnHelp')

insert into PERMISO(IdRol, NombreMenu) values
(4,'btnEfectuarTurno'),
(4,'btnReporteTurnos'),
(4,'btnHistoriaClinicaVER')

--insert into PERMISO(IdRol, NombreMenu) values
--(2,'btnPacientes'),
--(2,'btnMedicos'),
--(2,'btnTurnos'),
--(2,'btnHelp')

--select IdRol, NombreMenu from PERMISO

--insert into USUARIO(Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
--values

--('20', 'Empleado', 'empleado@gmail.com', '123',2,1)
select * from PERMISO
select * from ROL

select p.IdRol, p.NombreMenu from PERMISO p
inner join Rol r on r.IdRol = p.IdRol
inner join USUARIO u on u.IdRol = r.IdRol
where u.IdUsuario = 2

--select p.IdRol, p.NombreMenu from PERMISO p
--inner join Rol r on r.IdRol = p.IdRol
--inner join USUARIO u on u.IdRol = r.IdRol
--where u.IdUsuario = 2

--select * from PERMISO 
Select u.IdUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion from usuario u
inner join rol r on r.IdRol = u.IdRol

update usuario set estado = 0 where documento = 20

select * from usuario

select * from ESPECIALIDAD

Select IdEspecialidad, Descripcion, Estado from ESPECIALIDAD 

insert into ESPECIALIDAD(Descripcion, Estado) VALUES('Neurologia', 1)
insert into ESPECIALIDAD(Descripcion, Estado) VALUES('Hemoterapia', 1)


select IdMedico,Documento, NombreCompleto, e.IdEspecialidad, e.Descripcion[DescripcionEspecialidad] ,Correo, Direccion, Telefono, NumeroMatricula, m.Estado from Medico m
inner join ESPECIALIDAD e on e.IdEspecialidad = m.IdEspecialidad


insert into MEDICO(Documento,NombreCompleto,Correo,Direccion,Telefono,NumeroMatricula,IdEspecialidad) values ('43123456','Francisco Romero','Fran@hotmail.com','Su Casa 123','11045654','543212345323545',2)

select * from MEDICO

select * from USUARIO

select IdPaciente, Documento, NombreCompleto, Correo, Direccion, Telefono, Estado from PACIENTE
select * from PACIENTE

select * from TURNO

select * from DETALLE_TURNO

alter table detalle_turno drop column NumeroDocumento

select * from Turno where NumeroDocumento = '00001'
select * from DETALLE_TURNO where IdTurno = 1

-- obtener la informacion del turno
select t.IdTurno,
u.NombreCompleto,
pa.Documento,
pa.NombreCompleto[NombreCompletoPaciente],
t.NumeroDocumento,
CONVERT(char(10),t.FechaRegistro,103)[FechaRegistro]
from TURNO t
inner join USUARIO u on u.IdUsuario = t.IdUsuario
inner join PACIENTE pa on pa.IdPaciente = t.IdPaciente
where t.NumeroDocumento = '00001'

-- informacion del detalle de turno
select m.NombreCompleto, m.Documento, m.NumeroMatricula
from DETALLE_TURNO dt
inner join MEDICO m on m.IdMedico = dt.IdMedico
where dt.IdTurno = 1


select * from TURNO t
inner join USUARIO u on u.IdUsuario = t.IdUsuario
inner join PACIENTE pa on pa.IdPaciente = t.IdTurno
inner join DETALLE_TURNO dt on dt.IdTurno = t.IdTurno
inner join MEDICO m on m.IdMedico = dt.IdMedico
inner join ESPECIALIDAD es on es.IdEspecialidad = m.IdEspecialidad


select
CONVERT(char(10),t.FechaRegistro,103)[FechaRegistro],t.NumeroDocumento,
u.NombreCompleto[UsuarioRegistro],
pa.Documento[DocumentoPaciente],pa.NombreCompleto,
m.NombreCompleto[NombreMedico],m.NumeroMatricula,es.Descripcion[Especialidad]
from TURNO t
inner join USUARIO u on u.IdUsuario = t.IdUsuario
inner join PACIENTE pa on pa.IdPaciente = t.IdTurno
inner join DETALLE_TURNO dt on dt.IdTurno = t.IdTurno
inner join MEDICO m on m.IdMedico = dt.IdMedico
inner join ESPECIALIDAD es on es.IdEspecialidad = m.IdEspecialidad
where CONVERT(date, t.FechaRegistro) between '12/10/2022' and '12/12/2022'
and pa.IdPaciente = 2

select * from PERMISO

select * from DETALLE_TURNO


update MEDICO set Telefono = '+54 9 11 6522-9625' where IdMedico = 3


--public CD_HistoriaClinica(string idpaciente,string paciente,string recordatorio,string evolucion,string prescripcion,string usuario, string idusuario, string evento, string detalle, string origen)
--        {
--            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
--            string hora = DateTime.Now.ToString("HH:mm");

--            string cadena = "Insert into HISTORIA_CLINICA (Fecha, Hora, IdUsuario, Usuario, Evento, Detalle, Origen) " +
--            "values ('" + fecha + "', '" + hora + "', '" + idusuario + "', '" + usuario + "', '" + evento + "', '" + detalle + "', '" + origen + "')";


delete from historia_Clinica
select * from historia_clinica
insert into Historia_Clinica (Fecha, Hora, IdPaciente, Paciente, Recordatorio, Evolucion, Prescripcion, IdUsuario, Usuario, Evento, Detalle, Origen) " + " values('" + fecha + "', '" + hora + "', '" + idpaciente + "', '" + paciente + "',
'" + Recordatorio + "','" + Evolucion +"','"+Prescripcion+"','"+idusuario+"','"+usuario+ "', '" + evento + "', '" + detalle + "', '" + origen + "')";

update historia_clinica set Usuario = 'René Favaloro'

select * from rol

select * from turno

select * from detalle_turno
select *from PACIENTE


select
CONVERT(char(10),t.fecharegistro,103)[FechaRegistro],
u.NombreCompleto[UsuarioRegistro],
pa.Documento[DocumentoPaciente], pa.NombreCompleto,
m.NombreCompleto[NombreMedico],m.Documento[DocumentoMedico],
es.Descripcion[Especialidad]
from TURNO t
inner join usuario u on u.IdUsuario = t.IdUsuario
inner join paciente pa on pa.IdPaciente = t.Idpaciente
inner join detalle_turno dt on dt.IdTurno = t.IdTurno
inner join medico m on m.IdMedico = dt.IdMedico
inner join especialidad es on es.IdEspecialidad = m.IdMedico
where CONVERT(date,t.FechaRegistro) between '12/12/2022' and '12/14/2022'
and pa.IdPaciente = 1
