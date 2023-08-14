USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 14/08/2023 16:44:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RegistrarProveedor] (

@Documento varchar(50),
@RazonSocial varchar(100),
@Correo varchar(100),
@Telefono varchar(100),
@Estado int,
@Resultado int output,
@Mensaje varchar(500) output
)
as 
begin
	set @Resultado=0 
	set @Mensaje=''
	if not exists(select * from PROVEEDOR where Documento=@Documento)
	begin
	insert into PROVEEDOR(Documento,RazonSocial,Correo,Telefono,Estado)values
	(@Documento,@RazonSocial,@Correo,@Telefono,@Estado)
	set @Resultado= SCOPE_IDENTITY()
	end

	else
	set @mensaje ='El Documento ya existe'

end
GO

