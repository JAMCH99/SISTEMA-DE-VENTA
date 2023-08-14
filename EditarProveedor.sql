USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EditarProveedor]    Script Date: 14/08/2023 16:40:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EditarProveedor] (
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(100),
@Correo varchar(100),
@Telefono varchar(100),
@Estado int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado=0 
	set @Mensaje=''
	if not exists(select * from PROVEEDOR where Documento=@Documento and IdProveedor!=@IdProveedor)
	begin

	update PROVEEDOR set
	Documento=@Documento,
	RazonSocial=@RazonSocial,
	Correo=@Correo,
	Telefono=@Telefono,
	Estado=@Estado
	where IdProveedor=@IdProveedor
	set @Resultado=1
	end

	else
	set @mensaje ='No se puede repetir el documento para mas de un Proveedor'

end
GO

