USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[RegistrarVenta]    Script Date: 14/08/2023 16:44:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[RegistrarVenta]
(
@IdUsuario int ,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@DocumentoCliente varchar(500),
@NombreCliente varchar(500),
@MontoTotal decimal(18,2),
@MontoPago decimal(18,2),
@MontoCambio decimal(18,2),
@DetalleVenta[Edetalle_Venta] readonly,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin

	begin try
		declare @IdVenta int=0
		set @Resultado=1
		set @Mensaje=''
		
		begin transaction Registro

		 insert into VENTA(IdUsuario,TipoDocumento,NumeroDocumento,DocumentoCliente,NombreCliente,MontoTotal,MontoPago,MontoCambio)
		 values(@IdUsuario,@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoTotal,@MontoPago,@MontoCambio)

		 set @IdVenta= SCOPE_IDENTITY()

		insert into DETALLE_VENTA(IdVenta,IdProducto,Cantidad,PrecioVenta,Subtotal)
		select @IdVenta,IdProducto,Cantidad,PrecioVenta,MontoTotal from @DetalleVenta

		commit transaction  Registro

	end try
	
	begin catch
			
		set @Resultado=0
		set @Mensaje=ERROR_MESSAGE()
		 rollback transaction Registro
	end catch
	
end
GO

