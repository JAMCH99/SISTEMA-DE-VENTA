USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EditarCliente]    Script Date: 14/08/2023 16:39:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[EditarCliente]

@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
as 
begin
	set @Resultado=0 
	set @Mensaje=''
	if not exists(select * from CLIENTE where Documento=@Documento and IdCliente!=@IdCliente)
	begin

	update CLIENTE set
	Documento=@Documento,
	NombreCompleto=@NombreCompleto,
	Correo=@Correo,
	Telefono=@Telefono,
	Estado=@Estado
	where IdCliente=@IdCliente
	set @Resultado=1
	end

	else
	set @mensaje ='No se puede repetir el documento para mas de un Cliente'

end
GO

