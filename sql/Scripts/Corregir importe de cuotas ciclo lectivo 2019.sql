/*
	Este script se corre luego de encontrar un detalle en actualizacion de cursos.
	SMP actualizó importe de cuota de cursos. Dicho cambio afectó a planes de pago 2019
	los cuales no debieron ser modificados
*/
drop table #temp
go
create table #temp(
	IdPago				int				null,
	IdPlanPago			int				null,
	IdCurso				int				null,
	NroDocumento		decimal(18,0)	null,
	NroCuota			smallint		null,
	ImporteCuota		numeric(18,2)	null,
	ImportePagado		numeric(18,2)	null,
	FechaVto			datetime		null,
	Descripcion			varchar(255)	null,
	CicloLectivo		smallint		null,
	ValorAnterior		numeric(18,2)	null
)
go

insert into #temp(
	IdPago			,	IdPlanPago		, IdCurso			,   NroDocumento	,	
	NroCuota		,	ImporteCuota	, ImportePagado		,	FechaVto		,	
	Descripcion		,	CicloLectivo	, 
	ValorAnterior
)
select 
	p.id			,	pp.id			, pp.IdCurso		,  a.NroDocumento	,	
	p.NroCuota		,	p.ImporteCuota	, p.ImportePagado	,  p.fechavto		,	
	p.Descripcion	,	0				,--ca.ciclolectivo,
	valorAnterior=isnull(
		(select p1.ImporteCuota 
		from 
			Pagos p1 
		where 
			p.IdPlanPago = p1.IdPlanPago	and
			p1.ImportePagado is not null	and
			p1.nrocuota	= 1),	0)
from
	pagos p, 
	PlanesPago pp, 
	alumnos a
	--,
	--CursosAlumnos ca
where
	p.ImportePagado is null		and
	p.idplanpago	= pp.id		and

	pp.Estado		= 1			and

	pp.idalumno		= a.Id		
	--and
	--
	--ca.IdAlumno		= pp.IdAlumno	and
	--ca.IdCurso		= pp.IdCurso
	
	--	and	ca.ciclolectivo	= 2019

-->	(5233 filas afectadas)


--> Cuantos Planes de Pago fueron afectados
select IdPlanPago, NroDocumento, idCurso,	cuotas = count(1)
from 
	#temp 
group by IdPlanPago, NroDocumento, idCurso
order by IdPlanPago
--(678 filas afectadas)

update #temp
set CicloLectivo =
	isnull(
			(select 2019
			from pagos p
			where
				p.IdPlanPago	= t.IdPlanPago	and
				--p.Id			= t.IdPago		and
				p.NroCuota		= 0				and
				p.ImporteCuota	= 4100)
				, 0)
from
	#temp t
--(5233 filas afectadas)

--select count(1) from #temp where CicloLectivo <> 2019

--> Elimino pagos que no corresponden al CicloLectivo 2019
delete #temp
where
	CicloLectivo <> 2019
--(4436 filas afectadas)


--> Cargar importe de cuotas 
drop  table #ImporteCuota
go
create table #ImporteCuota(
	IdCurso			smallint		not null,
	ValorAnterior	numeric(18,2)	not null,
	ValorActual	numeric(18,2)	not null
)

insert into #ImporteCuota(
	IdCurso,	ValorAnterior,	ValorActual) 
select 
	IdCurso,	ValorAnterior,	ImporteCuota
from
	#temp
where
	ValorAnterior	> 0	and
	ImporteCuota	<	6000	--> Controla que el importe no correponda a una Matrícula
group by
	IdCurso,	ValorAnterior,	ImporteCuota
go
--(16 filas afectadas)

--> actualizar importe de cuotas
select 
	t.IdPago,		t.IdPlanPago, t.NroCuota, t.IdCurso,
	t.ImporteCuota,	
	CuotaCorregida = m.ValorAnterior
from 
	#temp	t,
	#ImporteCuota m
where
	m.IdCurso	= t.IdCurso



select 
t.IdCurso,
p.importecuota,
ImporteCuoa =
	(isnull(
			(select 
					m.ValorAnterior
				from 
					#ImporteCuota m
				where
					t.IdPago	= p.Id		and
					m.IdCurso	= t.IdCurso)
	, p.importecuota)
		
	)
from
	pagos	p,
	#temp	t
where
	p.Id	= t.IdPago
--(797 filas afectadas)
/*
select * from #temp where NroDocumento = 26368845
select * from pagos where IdPlanPago=98
select * from #ImporteCuota




*/

--> ACTUALIZA PAGOS

begin Transaction 
go	
	update pagos
	set ImporteCuota =
		(isnull(
			(select 
				m.ValorAnterior
			from 
				#ImporteCuota m
			where
				t.IdPago	= p.Id		and
				m.IdCurso	= t.IdCurso)
			, p.importecuota)
		)
	from
		pagos	p,
		#temp	t
	where
		p.Id	= t.IdPago
if @@ROWCOUNT = 797
		commit transaction 
else
		rollback transaction 


--> ver cuantos planes de pago fueron modificados
select 
	IdPlanPago, NroDocumento, ImporteCuota 
from 
	#temp 
group by
	IdPlanPago, NroDocumento, ImporteCuota 
--(181 filas afectadas)




--> Controlo importe de matrícula = 4100. Ciclo Lectivo 2019


/*
98	26368845	15	6
178	32055598	10	8
208	39899038	9	2
*/

select * from pagos where IdPlanPago = 208

select top 10 * from PlanesPago where estado = 3 and idcurso = 1
select * from Alumnos where id = 4862