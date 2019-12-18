use smporres

alter table pagos
alter column Estado tinyint
go

update pagos
set Estado = 3	--> Pagado
from
	pagos
where
	ImportePagado > 0 and
	IdMedioPago > 0	and
	FechaGrabacion >= '20190101'


