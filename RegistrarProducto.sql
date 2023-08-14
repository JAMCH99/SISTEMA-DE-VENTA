USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarProducto]    Script Date: 14/08/2023 16:44:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RegistrarProducto]
 @Codigo varchar(20),
 @Nombre varchar(30),
 @Descripcion varchar(30),
 @IdCategoria int,
 @Estado bit,
 @Resultado int output,
 @Mensaje varchar(500) output
 as
 begin
	set @Resultado=0
	if not exists(select * from PRODUCTO where Codigo=@Codigo)
	begin
	insert into PRODUCTO(Codigo,Nombre,Descripcion,IdCategoria,Estado)values
	(@Codigo,@Nombre,@Descripcion,@IdCategoria,@Estado)
	set @Resultado =1
	end
	else
	set @Mensaje= 'Ya existe un producto con el mismo código'
 end
GO

