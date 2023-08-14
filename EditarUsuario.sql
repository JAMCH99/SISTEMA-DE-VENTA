USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EditarUsuario]    Script Date: 14/08/2023 16:40:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[EditarUsuario] (
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as 
begin
	set @Respuesta=0 
	set @Mensaje=''
	if not exists(select * from USUARIO where Documento=@Documento and IdUsuario!=@IdUsuario)
	begin

	update USUARIO set
	Documento=@Documento,
	IdRol=@IdRol,
	NombreCompleto=@NombreCompleto,
	Correo=@Correo,
	Clave=@Clave,
	Estado=@Estado
	where IdUsuario=@IdUsuario
	set @Respuesta=1
	end

	else
	set @mensaje ='No se puede repetir el documento para mas de un usuario'

end
GO

