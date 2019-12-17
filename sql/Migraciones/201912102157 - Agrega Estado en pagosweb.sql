--> Estado = 1: mostrar el pago en la web y permitir imprimir
--> Estado = 2: no mostrar el pago en la web

alter table pagosweb
	add Estado tinyint
go
update pagosweb
	set Estado = 1