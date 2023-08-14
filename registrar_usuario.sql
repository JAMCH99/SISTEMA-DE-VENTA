USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 14/08/2023 16:35:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RegistrarUsuario](
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado int,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado=0
	set @Mensaje=''
	if not exists(select * from USUARIO where Documento=@Documento)
	begin

	insert into USUARIO(Documento,IdRol,NombreCompleto,Correo,Clave,Estado)
	values(@Documento,@IdRol,@NombreCompleto,@Correo,@Clave,@Estado)
	set @Resultado= SCOPE_IDENTITY()
	end

	else
	set @mensaje ='El Documento ya existe'

end
GO

