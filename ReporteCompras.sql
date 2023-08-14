USE [SISTEMA DE VENTAS]
GO

/****** Object:  StoredProcedure [dbo].[ReporteCompras]    Script Date: 14/08/2023 16:45:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReporteCompras](
@IdProveedor int,
@FechaInicio varchar(10),
@FechaFin varchar(10)
)
as
begin

set dateformat dmy;
select CONVERT(char(10),c.FechaCreacion,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
u.NombreCompleto[UsuarioRegistro],
pr.Documento[DocumentoProveedor],pr.RazonSocial,
p.Codigo[CodigoProducto],p.Nombre[NombreProducto],
ca.Descripcion[CategoriaProducto],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[subtotal]

from COMPRA c inner join USUARIO u on c.IdUsuario= u.IdUsuario
inner join PROVEEDOR pr on pr.IdProveedor= c.IdProveedor
inner join DETALLE_COMPRA dc on dc.IdCompra= c.IdCompra
inner join PRODUCTO p on p.IdProducto= dc.IdProducto
inner join CATEGORIA ca on ca.IdCategoria=p.IdCategoria

where CONVERT(char(10),c.FechaCreacion,103) between @FechaInicio and @FechaFin
-- la siguiente función es una condicional que compara los valores que se le pasa
--si es igual a 0 entonces mostrara todos los proveedorees
-- caso contrario mostrara el provedor que se le esta pasando
 
and pr.IdProveedor=iif(@IdProveedor=0,pr.IdProveedor,@IdProveedor)

end
GO

