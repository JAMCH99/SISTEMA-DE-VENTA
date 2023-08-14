USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarCategoria]    Script Date: 14/08/2023 16:43:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RegistrarCategoria]
@Descripcion varchar(50),
@Resultado int output,
@Estado bit,
@Mensaje varchar(500) output
as
begin
	set @resultado=0
	if not exists(select * from CATEGORIA c where c.Descripcion=@Descripcion)
	begin 
	insert into CATEGORIA(Descripcion,Estado)values(@Descripcion,@Estado)
	set @Resultado= SCOPE_IDENTITY()
	end
	else
	set @mensaje= 'No se puede repetir la categoria de un producto'
end
GO

