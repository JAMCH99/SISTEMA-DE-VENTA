USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 14/08/2023 16:41:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EliminarProducto]
 @IdProducto int,
 @Resultado bit output,
 @Mensaje varchar(500) output
 as
 begin
	set @Resultado=0
	set @mensaje=''
	declare @Pasoreglas bit=1

	if exists(select * from PRODUCTO p inner join
	DETALLE_COMPRA dc on p.IdProducto= dc.IdProducto
	where p.IdProducto=@IdProducto)
	begin
	 set @Resultado=0
	 set @Pasoreglas=0
	 set @Mensaje='no se puede eliminar porque se encuentra relacionado a una compra \n'
	end

	if exists(select *from PRODUCTO p inner join
	DETALLE_VENTA dv on p.IdProducto= dv.IdProducto
	where p.IdProducto=@IdProducto)
	begin
	set @Resultado=0
	set @Pasoreglas=0
	set @Mensaje='no se puede eliminar porque se encuentra relacionado a una venta \n'
	end
	if(@Pasoreglas=1)
	begin
	delete from PRODUCTO where IdProducto=@IdProducto
	set @Resultado=1
	end
 end
GO

