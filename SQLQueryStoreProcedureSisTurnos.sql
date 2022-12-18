

select * from usuario


CREATE PROC SP_REGISTRARUSUARIO(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento)
	begin
		insert into usuario(Documento, NombreCompleto,Correo,Clave,IdRol,Estado) values
		(@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

--PRUEBAS

declare @idusuariogenerado int
declare @mensaje varchar(500)

exec SP_REGISTRARUSUARIO '123', 'pruebas','prueba@hotmail.com', '456', 2, 1, @idusuariogenerado output, @mensaje output

select @idusuariogenerado
select @Mensaje

go

CREATE PROC SP_EDITARUSUARIO(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento and Idusuario != @IdUsuario)
	begin
		update usuario set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo= @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'
end

go

CREATE PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	DECLARE @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM TURNO T
	INNER JOIN USUARIO U ON U.IdUsuario = T.IdUsuario
	where U.IdUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a un turno\n'

	END

	if(@pasoreglas = 1)
	begin
		delete from USUARIO where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end
end





--PRUEBAS
declare @respuesta bit
declare @mensaje varchar(500)

exec SP_EDITARUSUARIO  1003,'123','pruebas 2','test@gmail.com','456',2,1,@respuesta output,@mensaje output

select @respuesta

select @mensaje

select * from USUARIO

go

/* -------------------PROCEDIMIENTOS PARA ESPECIALIDAD----------------*/

--PROCEDIMIENTO PARA GUARDAR ESPECIALIDAD

CREATE PROC SP_RegistrarEspecialidad(
@Descripcion varchar(50),
@Estado bit,
@Resultado int output,
@mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM ESPECIALIDAD WHERE Descripcion = @Descripcion)
	begin
		insert into ESPECIALIDAD(Descripcion, Estado)values (@Descripcion, @Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion para mas de una especialidad'

end

go

--PROCEDIMIENTO PARA MODIFICAR ESPECIALIDAD

Create PROC SP_EditarEspecialidad(
@IdEspecialidad int,
@Descripcion varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM ESPECIALIDAD WHERE Descripcion = @Descripcion and IdEspecialidad != @IdEspecialidad)
		update ESPECIALIDAD set
		Descripcion = @Descripcion,
		Estado =  @Estado
		where IdEspecialidad = @IdEspecialidad
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una especialidad'
	end

end

go

--PROCEDIMIENTO PARA ELIMINAR ESPECIALIDAD

CREATE PROC SP_EliminarEspecialidad(
@IdEspecialidad int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	SELECT * FROM ESPECIALIDAD c
	inner join MEDICO m on m.IdEspecialidad = c.IdEspecialidad
	where c.IdEspecialidad = @IdEspecialidad
	)
	begin
		delete top (1) from ESPECIALIDAD where IdEspecialidad = @IdEspecialidad
	end
		
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede eliminar la especialidad, se encuentra relacionada a un medico'
	end

end

go


/* -------------------PROCEDIMIENTOS PARA MEDICOS----------------*/


--PROCEDIMIENTO PARA GUARDAR MEDICO
CREATE PROC sp_RegistrarMedico(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@NumeroMatricula varchar(50),
@IdEspecialidad int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM MEDICO where NumeroMatricula = @NumeroMatricula)
	begin
		insert into MEDICO(Documento,NombreCompleto,Correo,Direccion,Telefono,NumeroMatricula,IdEspecialidad,Estado) values (@Documento,@NombreCompleto,@Correo,@Direccion,@Telefono,@NumeroMatricula,@IdEspecialidad,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
	SET @Mensaje = 'Ya existe un medico con el mismo numero de matricula'

end

Go

--PROCEDIMIENTO PARA MODIFICAR MEDICO

CREATE PROC sp_ModificarMedico(
@IdMedico int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@NumeroMatricula varchar(50),
@IdEspecialidad int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM MEDICO WHERE NumeroMatricula = @NumeroMatricula and IdMedico != @IdMedico)

		UPDATE MEDICO set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Direccion = @Direccion,
		Telefono = @Telefono,
		NumeroMatricula = @NumeroMatricula,
		IdEspecialidad = @IdEspecialidad,
		Estado = @Estado
		WHERE IdMedico = @IdMedico
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un medico con el mismo numero de matricula'
	end
end

go

--PROCEDIMIENTO PARA ELIMINAR MEDICO

CREATE PROC SP_EliminarMedico(
@IdMedico int,
@Respuesta bit output,
@Mensaje varchar(500) output
)as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM DETALLE_TURNO dt
	INNER JOIN MEDICO m ON m.IdMedico = dt.IdMedico
	WHERE m.IdMedico = @IdMedico
	)
	begin
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado a un turno\n'
	END

	if(@pasoreglas = 1)
	begin
		delete from MEDICO where IdMedico = @IdMedico
		set @Respuesta = 1
	end

end


/* -------------------PROCEDIMIENTOS PARA PACIENTE----------------*/

--PROCEDIMIENTO PARA GUARDAR PACIENTE
CREATE PROC sp_RegistrarPaciente(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PACIENTE where Documento = @Documento)
	begin
		insert into PACIENTE(Documento,NombreCompleto,Correo,Direccion,Telefono,Estado) values (@Documento,@NombreCompleto,@Correo,@Direccion,@Telefono,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
	SET @Mensaje = 'Ya existe un paciente con ese documento'

end

Go

--PROCEDIMIENTO PARA MODIFICAR PACIENTE

CREATE PROC sp_ModificarPaciente(
@IdPaciente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Direccion varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PACIENTE WHERE Documento = @Documento and IdPaciente != @IdPaciente)

		UPDATE PACIENTE set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Direccion = @Direccion,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IdPaciente = @IdPaciente
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un paciente con ese documento'
	end
end

go

--PROCEDIMIENTO PARA ELIMINAR PACIENTE

CREATE PROC SP_EliminarPaciente(
@IdPaciente int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 SELECT * FROM PACIENTE p
	 INNER JOIN TURNO t ON p.IdPaciente = t.IdPaciente
	 WHERE p.IdPaciente = @IdPaciente
	)
	begin
		delete top(1) from PACIENTE where IdPaciente = @IdPaciente
	end
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'No se puede eliminar porque se encuentra relacionado a un turno'
	end
end

SELECT * FROM TURNO
/* -------------------PROCEDIMIENTOS PARA TURNOS----------------*/

CREATE TYPE [dbo].[EDetalle_Turno] as table(
	[IdMedico] int NULL
)

go

--00001
--00002
select * from turno
select COUNT(*) + 1 from TURNO

--PROCEDIMIENTO PARA GUARDAR TURNO

CREATE PROCEDURE sp_RegistrarTurno(
@IdUsuario int,
@IdPaciente int,
@NumeroDocumento varchar(500),
@DetalleTurno [EDetalle_Turno] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @IdTurno int = 0
		set @Resultado = 1
		set @Mensaje = ''
		begin transaction registro
		insert into TURNO(IdUsuario, IdPaciente, NumeroDocumento)
		values(@IdUsuario,@IdPaciente,@NumeroDocumento)
		
		set @IdTurno = SCOPE_IDENTITY()

		insert into DETALLE_TURNO(IdTurno,IdMedico)
		select @IdTurno, IdMedico from @DetalleTurno

		select * from Medico p
		inner join @DetalleTurno dt on dt.IdMedico = p.IdMedico


		commit transaction registro
	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch

end



--PROCEDIMIENTO PARA VER EL REPORTE DE TURNOS
CREATE proc sp_HistoriaMedica(
@FechaInicio varchar(10),
@FechaFin varchar(10),
@IdMedico int
)
as
begin

	set dateformat mdy;
	 select
	CONVERT(char(10),t.fecharegistro,103)[FechaRegistro],t.NumeroDocumento,
	u.NombreCompleto[UsuarioRegistro],
	pa.Documento[DocumentoPaciente], pa.NombreCompleto,
	m.NombreCompleto[NombreMedico],m.Documento[DocumentoMedico],m.NumeroMatricula,
	es.Descripcion[Especialidad]
	from TURNO t
	inner join usuario u on u.IdUsuario = t.IdUsuario
	inner join paciente pa on pa.IdPaciente = t.Idpaciente
	inner join detalle_turno dt on dt.IdTurno = t.IdTurno
	inner join medico m on m.IdMedico = dt.IdMedico
	inner join especialidad es on es.IdEspecialidad = m.IdMedico
	where CONVERT(date,t.FechaRegistro) between @FechaInicio and @FechaFin
	and m.IdMedico = IIF(@IdMedico=0,m.IdMedico,@IdMedico)
end


exec sp_HistoriaMedica '12/12/2022','12/14/2022',3