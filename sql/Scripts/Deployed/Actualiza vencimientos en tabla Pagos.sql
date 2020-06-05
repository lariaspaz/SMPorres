drop table #Cuotas2019
select 
	NroCuota, FechaVto
into #Cuotas2019
from
	Pagos
where
	ImportePagado>=	0	and
	FechaVto < '20200101'
group by
	NroCuota, FechaVto
order by
	NroCuota


--0	2019-12-31 00:00:00.000

delete from #Cuotas2019 where NroCuota = 1 and FechaVto>'20190415'
--1	2019-04-15 00:00:00.000
--1	2019-04-30 00:00:00.000

--2	2019-05-15 00:00:00.000

delete from #Cuotas2019 where NroCuota = 3 and FechaVto>'20190617'
--3	2019-06-17 00:00:00.000
--3	2019-06-18 00:00:00.000

--4	2019-07-15 00:00:00.000

delete from #Cuotas2019 where NroCuota = 5 and FechaVto>'20190815'
--5	2019-08-15 00:00:00.000
--5	2019-08-18 00:00:00.000

--6	2019-09-16 00:00:00.000

delete from #Cuotas2019 where NroCuota = 7 and FechaVto>'20191015'
--7	2019-10-16 00:00:00.000
--7	2019-10-15 00:00:00.000

delete from #Cuotas2019 where NroCuota = 8 and FechaVto>'20191115'
--8	2019-11-15 00:00:00.000
--8	2019-11-16 00:00:00.000

delete from #Cuotas2019 where NroCuota = 9 and FechaVto<'20191216'
--9	2019-12-16 00:00:00.000
--9	2019-12-15 00:00:00.000

select * from #Cuotas2019
order by NroCuota

--> Ciclo Lectivo 2019
drop table #planesPago2019
go
select	
	IdPlanPago
into #planesPago2019
from
	pagos
where
	NroCuota = 0		and
	ImporteCuota = 4100
group by
	IdPlanPago
--(602 filas afectadas)

select
	FechaVto = (select c.FechaVto
				from
					#Cuotas2019 c
				where
					c.NroCuota		= p.NroCuota
				)
from
	pagos p,
	#planesPago2019 pp
where
	p.IdPlanPago	= pp.IdPlanPago

--(5857 filas afectadas)

begin Transaction
go
	update Pagos
	set FechaVto = (select c.FechaVto
					from
						#Cuotas2019 c
					where
						c.NroCuota		= p.NroCuota
					)
	from
		pagos p,
		#planesPago2019 pp
	where
		p.IdPlanPago	= pp.IdPlanPago
if @@ROWCOUNT = 5857
		commit transaction 
else
		rollback transaction
--(5857 filas afectadas)

select count(1) from pagos where FechaVto is not null

drop table #planesPago2020
go
select	
	IdPlanPago
into #planesPago2020
from
	pagos
where
	NroCuota = 0	and
	ImporteCuota <> 4100
group by
	IdPlanPago
--(597 filas afectadas)


select
	FechaVto= (select c.VtoCuota
				from
					Cuotas c
				where
					c.Cuota		= p.NroCuota
				)
from
	pagos p,
	#planesPago2020 pp
where
	pp.IdPlanPago	= p.IdPlanPago	
	--and
	--isnull(p.ImportePagado,0) <> 0
--(5446 filas afectadas)

begin Transaction
go
	update Pagos
	set FechaVto = (select c.VtoCuota
				from
					Cuotas c
				where
					c.Cuota		= p.NroCuota
				)
from
	pagos p,
	#planesPago2020 pp
where
	p.IdPlanPago	= pp.IdPlanPago
if @@ROWCOUNT = 5446
		commit transaction
else
		rollback transaction