use SMPorres_Dev

--El descuento que se agrega corresponde al pago adelantado de matrícula 2020
-- Un alumno que pague la matrícula 2020 antes de fin de año, obtiene descuento de $1.100,00
-->	Descuento	
-->	FecVenDescuento

alter table cursos	
	add DescuentoMatricula	numeric(18,2)	null,
		FechaVencDescuento	datetime		null
go

-- Un alumno va a disponer de 3 cuotas para adelantar el pago de la matrícula, esas cuotas
-- tendrán un único vencimiento y los montos se definirán para cada curso
-->	Cuota1
-->	Cuota2
-->	Cuota3

alter table cursos	
	add Cuota1 numeric(18,2)	null,
		Cuota2 numeric(18,2)	null,
		Cuota3 numeric(18,2)	null
go


-- Un curso se diferencia por Estado
-- 1 - Activo
-- 2 - Baja

alter table cursos
	add Estado smallint null
go
update Cursos
	set Estado = 1