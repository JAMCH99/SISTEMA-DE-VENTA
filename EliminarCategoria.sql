USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[EliminarCategoria]    Script Date: 14/08/2023 16:41:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[EliminarCategoria]
@Resultado bit output,
@Mensaje varchar(500) output,
@IdCategoria int
as
begin
	if not exists(select * from CATEGORIA c inner join PRODUCTO p on c.IdCategoria = p.IdCategoria
	where c.IdCategoria= @IdCategoria)
	begin
	set @Resultado=1
	delete top(1) from CATEGORIA where IdCategoria= @IdCategoria
	end
	else
	begin
	set @Resultado =0
	set @Mensaje = 'la categoria se encuentra relacionada con un producto'
	end
end
GO

