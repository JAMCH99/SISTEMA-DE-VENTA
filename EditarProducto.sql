USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EditarProducto]    Script Date: 14/08/2023 16:40:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EditarProducto]
 @IdProducto int,
 @Codigo varchar(20),
 @Nombre varchar(30),
 @Descripcion varchar(30),
 @IdCategoria int,
 @Estado bit,
 @Resultado bit output,
 @Mensaje varchar(500) output
 as
 begin
	set @Resultado=1
	if not exists(select * from PRODUCTO where Codigo=@Codigo and IdProducto!=@IdProducto)
	begin
	update PRODUCTO set
	codigo=@Codigo,
	nombre=@Nombre,
	Descripcion=@Descripcion,
	IdCategoria=@IdCategoria,
	Estado=@Estado
	where IdProducto= @IdProducto
	
	end
	else
	set @Resultado =0
	set @Mensaje= 'Ya existe un producto con el mismo código'
 end
GO

