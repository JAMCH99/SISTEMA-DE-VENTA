create type [dbo].[Edetalle_Compra] as table
(
IdProducto int null,
Preciocompra decimal(18,2) null,
PrecioVenta decimal(18,2) null,
Cantidad int null,
MontoTotal decimal(18,2) null
)
go

create procedure RegistrarCompra
