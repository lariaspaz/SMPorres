
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SET FMTONLY OFF
--GO
-- ==========================================================================================
-- Author:		Joaquín Hernán Castillo
-- Create date: 2020-03-15
-- Description:	Consulta los pagos realizados por ciclo Lectivo de todas las carreras y 
--				cursos.
-- ==========================================================================================
alter PROCEDURE [dbo].[ConsInformeEconomico]
@CicloLectivo	smallint	
AS

SET NOCOUNT ON
SET FMTONLY OFF

create table #pagos(
	planPago			int				null,
	pago				int				null,
	carrera				varchar(255)	null,
	curso				varchar(50)		null,
	nroCuota			smallint		null,
	importeCuota		numeric(18,2)	null,
	importePagoTermino	numeric(18,2)	null,
	importeBeca			numeric(18,2)	null,
	importeRecargo		numeric(18,2)	null,
	importePagado		numeric(18,2)	null,
	medioPago			varchar(50)		null,
	fechaVto			datetime		null,
	fechaPago			datetime		null,
	cicloLectivo		smallint		null
)

create table #salida(
	carrera					varchar(255)	null,
	curso					varchar(50)		null,
	nroCuota				smallint		not null,		
	importeCuota			numeric(18,2)	not null,
	impCuotas				numeric(18,2)	not null,
	impPagoTer				numeric(18,2)	not null,
	impBeca					numeric(18,2)	not null,
	impRec					numeric(18,2)	not null,
	impPag					numeric(18,2)	not null,
	cantidadCuotas			int				not null,
	cantidadCuotasPagadas	int				not null,
	cantidadCuotasAdeudadas int				not null
)

insert into #pagos(
	planPago,		pago,	carrera,	curso,
	nroCuota,	importeCuota,
	importePagoTermino,
	importeBeca,			
	importeRecargo,		
	importePagado,		
	medioPago,			
	fechaVto,			
	fechaPago,			
	cicloLectivo)
select
	p.IdPlanPago,	p.id,	ca.Nombre, 	c.Nombre, 
	p.NroCuota,		p.ImporteCuota,	
	isnull(- p.ImportePagoTermino,0),	
	isnull(- p.ImporteBeca,0),
	isnull(p.ImporteRecargo,0), 
	isnull(p.ImportePagado,0),
	MedioPago = '',
	convert(varchar, isnull(p.FechaVto,''),103),
	FechaPago= convert(varchar, isnull(p.FechaGrabacion,''), 103),
	cicloLectivo = 2020
from 
	pagos p,
	PlanesPago pp,
	Cursos c,
	Carreras ca--,	
	--CursosAlumnos cal
where
	pp.Id				=	p.IdPlanPago	and
	pp.Estado			<	3				and
			
	--cal.IdCurso		=	pp.IdCurso		and
	--cal.IdAlumno		=	pp.IdAlumno		and
			
	--c.Id				=	cal.IdCurso		and
	c.Id				=	pp.IdCurso		and
			
	ca.Id				=	c.IdCarrera		--and
	
	--cal.cicloLectivo	= 	@CicloLectivo
--(10236 filas afectadas)
--> Actualizar ciclo lectivo
update #pagos
set cicloLectivo = 2019
from
	pagos p,
	#pagos tp
where
	p.IdPlanPago	= tp.planPago		and
	p.NroCuota		= 0					and
	p.ImporteCuota	= 4100

--select cicloLectivo, count(1) from #pagos group by cicloLectivo
--> Control ciclo lectivo solicitado
delete from #pagos
where
	cicloLectivo <> @CicloLectivo

--> Actualiza medios de pago
--update #pagos
--set medioPago = mp.Descripcion
--from
--	#pagos		p,
--	Pagos		po,
--	MediosPago	mp
--where
--	po.Id			= p.pago	and
--
--	po.IdMedioPago	= mp.Id

insert into #salida(
	carrera,	curso,	nroCuota,		importeCuota,
	impCuotas,
	impPagoTer,
	impBeca,
	impRec,
	impPag,
	cantidadCuotas,
	cantidadCuotasPagadas,
	cantidadCuotasAdeudadas
)
select 
	p.carrera, p.curso, p.nroCuota,		p.importeCuota,
	impCuotas=sum(p.importeCuota),	
	impPagoTer=sum(p.importePagoTermino),	
	impBeca=sum(p.importeBeca),	
	impRec=sum(p.importeRecargo),	
	impPag=sum(p.importePagado),	
	cantidadCuotas=count(1),
	cantidadCuotasPagadas = 0,
	cantidadCuotasAdeudadas = 0
from #pagos p
--where
--	cicloLectivo = @CicloLectivo
group by carrera, curso, nroCuota, importeCuota
Order by carrera, curso, nrocuota

update #salida
set cantidadCuotasPagadas = (
	select count(1)
	from
		#pagos	p
	where
		p.carrera		=  t.carrera		and
		p.curso			=  t.curso			and
		p.nroCuota		=  t.nroCuota		and
		p.importeCuota	= t.importeCuota	and
		p.importePagado >	0)
from #salida t

-->Cuotas impagas
update #salida
set cantidadCuotasAdeudadas = (
	select count(1)
	from
		#pagos	p
	where
		p.carrera		=  t.carrera		and
		p.curso			=  t.curso			and
		p.nroCuota		=  t.nroCuota		and
		p.importeCuota	= t.importeCuota	and
		p.importePagado =	0)
from #salida t

select 
	carrera,
	curso,
	nroCuota,		
	importeCuota,
	impCuotas,	
	impPagoTer,	
	impBeca,	
	impRec,	
	impPag,	
	cantidadCuotas,
	cantidadCuotasPagadas,
	cantidadCuotasAdeudadas
from #salida
order by carrera, curso, nroCuota