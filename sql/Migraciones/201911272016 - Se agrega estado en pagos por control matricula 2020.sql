--Estado, ser� usado inicialmente para controlar el pago en cuotas de matr�cula 2020
--	1 - Activo: Matr�cula al d�a, puede tener pagos, pero la suma de los mismo no completa pago total
--	2 - Baja: cuota dada de baja
--  3 - Pagado: Matr�cula 2020 pagada

alter table Pagos	
	add Estado	smallint	null
go

update Pagos
	set Estado = 1
go