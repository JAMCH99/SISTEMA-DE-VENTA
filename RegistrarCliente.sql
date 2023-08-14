USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarCliente]    Script Date: 14/08/2023 16:43:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[RegistrarCliente]
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
as
begin
	set @Resultado=0
	if not exists(select * from CLIENTE where Documento=@Documento)
	begin
	insert into CLIENTE(Documento,Nombrecompleto,Correo,Telefono,Estado)values
	(@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)
	set @Resultado= SCOPE_IDENTITY()
	end
	else

	set @Mensaje= 'No se pueede repetir el Documento'
end
GO

