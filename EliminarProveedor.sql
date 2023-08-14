USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EliminarProveedor]    Script Date: 14/08/2023 16:42:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[EliminarProveedor](
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin 
	set @Resultado=0
	set @Mensaje= '' 
	declare @pasoreglas bit =1

	if  exists(select * from COMPRA c inner join PROVEEDOR p on c.IdProveedor= p.IdProveedor
	where p.IdProveedor= @IdProveedor)
	begin

	set @Mensaje='El Proveedor se encuentra relacionado a una compra y no se puede eliminar'
	set @Resultado=0
	set @pasoreglas=0
	end

	if (@pasoreglas=1) 
	begin
	delete top(1) PROVEEDOR where IdProveedor=@IdProveedor
	set @Resultado=1
	end
end
GO

