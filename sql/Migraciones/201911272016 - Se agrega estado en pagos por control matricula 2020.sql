Use SMPorres_Dev

--Estado, será usado inicialmente para controlar el pago en cuotas de matrícula 2020
--	1 - Activo: Matrícula al día, puede tener pagos, pero la suma de los mismo no completa pago total
--	2 - Baja: cuota dada de baja
--  3 - Pagado: Matrícula 2020 pagada

alter table Pagos	
	add Estado	smallint	null
go

update Pagos
	set Estado = 1
go