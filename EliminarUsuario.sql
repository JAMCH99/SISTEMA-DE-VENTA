USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 14/08/2023 16:42:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[EliminarUsuario](
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin 
	set @Respuesta=0
	set @respuesta= '' 
	declare @pasoreglas bit =1

	if  exists(select * from COMPRA c inner join USUARIO u on c.IdUsuario=u.IdUsuario
	where c.IdUsuario= @IdUsuario)
	
	begin

	set @Mensaje='El usuario se encuentra realizando una compra y no puede eliminarse'
	set @Respuesta=0
	set @pasoreglas=0
	end

	if  exists(select * from VENTA v inner join USUARIO u on v.IdUsuario=u.IdUsuario
	where v.IdUsuario=@IdUsuario)
	
	begin

	set @Mensaje='El usuario se encuentra relacionado a una venta y no puede eliminarse'
	set @Respuesta=0
	set @pasoreglas=0
	end

	if (@pasoreglas=1) 
	begin
	delete USUARIO where IdUsuario= @IdUsuario
	set @respuesta=1
	end
end
GO

