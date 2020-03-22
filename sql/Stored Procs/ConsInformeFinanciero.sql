SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Author:		Joaquín Hernán Castillo
-- Create date: 2020-03-21
-- Description:	Consulta los pagos realizados por ciclo Lectivo entre fechas de todas las carreras y 
--				cursos.
-- ==========================================================================================
alter PROCEDURE [dbo].[ConsInformeFinanciero]
@desde datetime,
@hasta datetime
AS

SET NOCOUNT ON
SET FMTONLY OFF

create table #pagos(
	carrera				varchar(255)	not null,
	curso				varchar(50)		not null,
	importeCuota		numeric(18,2)	not null,
	importePagoTermino	numeric(18,2)	not null,
	importeBeca			numeric(18,2)	not null,
	importeRecargo		numeric(18,2)	not null,
	importePagado		numeric(18,2)	not null
)

insert into #pagos(
	carrera,	curso,
	importeCuota,
	importePagoTermino,
	importeBeca,			
	importeRecargo,		
	importePagado)
select
	ca.Nombre, 	c.Nombre, 
	p.ImporteCuota,	
	PagoTermino = isnull(- p.ImportePagoTermino,0),	
	Beca = isnull(- p.ImporteBeca,0),
	Recargo = isnull(p.ImporteRecargo,0), 
	Pagado = isnull(p.ImportePagado,0)
from 
	PlanesPago pp,
	pagos p,
	CursosAlumnos cal,
	Cursos c,
	Carreras ca
where
	pp.Id				=	p.IdPlanPago	and
	pp.Estado			<	3				and	-- no está dado de baja

	p.FechaGrabacion	>= @desde			and
	p.FechaGrabacion	<= @hasta			and

	cal.IdCurso			=	pp.IdCurso		and
	cal.IdAlumno		=	pp.IdAlumno		and
			
	c.Id				=	cal.IdCurso		and

	ca.Id				=	c.IdCarrera		

select 
	carrera,			
	curso, 
	Cuotas=count(1),
	Importe = sum(importeCuota),	
	pagoTermino = sum(importePagoTermino),	
	Beca=sum(importeBeca),			
	Recargo=sum(importeRecargo),	
	Pagado=sum(importePagado)
from #pagos
group by
carrera, curso