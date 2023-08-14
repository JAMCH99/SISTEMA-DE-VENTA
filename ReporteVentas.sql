USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[ReporteVentas]    Script Date: 14/08/2023 16:45:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[ReporteVentas](

@FechaInicio varchar(10),
@FechaFin varchar(10)
)
as
begin

set dateformat dmy;

select CONVERT(char(10),v.FechaCreacion,103)[FechaRegistro],v.TipoDocumento,v.NumeroDocumento,v.MontoTotal,
u.NombreCompleto[UsuarioRegistro],
v.DocumentoCliente,v.NombreCliente,
p.Codigo[CodigoProducto],p.Nombre[NombreProducto],
ca.Descripcion[CategoriaProducto],dv.PrecioVenta,dv.Cantidad,dv.Subtotal

from VENTA v inner join USUARIO u on v.IdUsuario= u.IdUsuario
inner join DETALLE_VENTA dv on dv.IdVenta= v.IdVenta
inner join PRODUCTO p on p.IdProducto= dv.IdProducto
inner join CATEGORIA ca on ca.IdCategoria=p.IdCategoria

 where CONVERT(char(10),v.FechaCreacion,103) between @FechaInicio and @FechaFin

 end
GO

