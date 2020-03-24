SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	planPago			int				not null,
	pago				int				not null,
	carrera				varchar(255)	not null,
	curso				varchar(50)		not null,
	nroCuota			smallint		not null,
	importeCuota		numeric(18,2)	not null,
	importePagoTermino	numeric(18,2)	not null,
	importeBeca			numeric(18,2)	not null,
	importeRecargo		numeric(18,2)	not null,
	importePagado		numeric(18,2)	not null,
	medioPago			varchar(50)		not null,
	fechaVto			datetime		not null,
	fechaPago			datetime		not null,
	cicloLectivo		smallint		not null
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
	cal.cicloLectivo
from 
	pagos p,
	PlanesPago pp,
	Cursos c,
	Carreras ca,	
	CursosAlumnos cal
where
	pp.Id				=	p.IdPlanPago	and
	pp.Estado			<	3				and
			
	cal.IdCurso			=	pp.IdCurso		and
	cal.IdAlumno		=	pp.IdAlumno		and
			
	c.Id				=	cal.IdCurso		and
			
	ca.Id				=	c.IdCarrera		and
	
	cal.cicloLectivo	= 	@CicloLectivo

update #pagos
set medioPago = mp.Descripcion
from
	#pagos		p,
	Pagos		po,
	MediosPago	mp
where
	po.Id			= p.pago	and

	po.IdMedioPago	= mp.Id

select 
	p.nroCuota,		
	p.importeCuota,
	impCuotas=sum(p.importeCuota),	
	impPagoTer=sum(p.importePagoTermino),	
	impBeca=sum(p.importeBeca),	
	impRec=sum(p.importeRecargo),	
	impPag=sum(p.importePagado),	
	cantidadCuotas=count(1),
	cantidadCuotasPagadas=0,
	cantidadCuotasAdeudadas=0
into #temp
from #pagos p
where
	cicloLectivo = @CicloLectivo
group by nroCuota, importeCuota
Order by nrocuota

update #temp
set cantidadCuotasPagadas = (
	select count(1)
	from
		#pagos	p
	where
		p.nroCuota		=  t.nroCuota		and
		p.importeCuota	= t.importeCuota	and
		p.importePagado >	0)
from #temp t

-->Cuotas impagas
update #temp
set cantidadCuotasAdeudadas = (
	select count(1)
	from
		#pagos	p
	where
		p.nroCuota		=  t.nroCuota		and
		p.importeCuota	= t.importeCuota	and
		p.importePagado =	0)
from #temp t

select * from #temp
order by nroCuota