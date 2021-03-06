/****** Object:  StoredProcedure [dbo].[ConsAlumnosMorosos]    Script Date: 05/03/2019 06:37:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Author:		Leonardo Arias Paz
-- Create date: 2019-03-05
-- Description:	Consultar los alumnos al día o morosos en un curso o carrera. Muestra la beca
--				de cada alumno en el resultado.
-- ==========================================================================================
ALTER PROCEDURE [dbo].[ConsAlumnosMorosos]
@Fecha		smalldatetime,
@Tipo		smallint,	-- 1: al día, 2: morosos
@IdCarrera	int,
@IdCurso	int,
@TipoBecado	smallint	-- 0: todos, 1: sin beca, 2: con beca
AS

SET NOCOUNT ON
SET FMTONLY OFF

-- para cada plan obtengo la primera cuota impaga
select p.IdPlanPago, Cuota = MIN(p.NroCuota) 
into #temp
from Pagos p
inner join Cuotas c on c.Cuota = p.NroCuota
where	
	p.Fecha	is null	and
	p.EsContrasiento is null
group by p.IdPlanPago
--having min(p.nrocuota) > 0

-- para cada plan obtengo la última cuota pagada (si hubiera)
select p.IdPlanPago, Cuota = MAX(p.NroCuota)
into #ultPagos
from Pagos p
inner join Cuotas c on c.Cuota = p.NroCuota
where	
	p.Fecha	is not null	and
	p.EsContrasiento is null
group by p.IdPlanPago
--having max(p.nrocuota) <= 2


alter table #temp add 
	IdPago			int null,
	FechaPago		smalldatetime null,
	VtoCuota		smalldatetime,
	ImportePagado	numeric(18,2)

update #temp
set
	IdPago	= p.Id,
	VtoCuota= c.VtoCuota
from #temp t
inner join Pagos p on p.IdPlanPago = t.IdPlanPago and p.NroCuota = t.Cuota
inner join Cuotas c on t.Cuota = c.Cuota

update #temp
set
	FechaPago		= p.Fecha,
	ImportePagado	= p.ImportePagado
from #temp t
inner join #ultPagos u on u.IdPlanPago = t.IdPlanPago
inner join Pagos p on p.IdPlanPago = u.IdPlanPago and p.NroCuota = u.Cuota

select	IdCurso = c.Id, c.Nombre AS Curso, IdCarrera = c1.Id, c1.Nombre AS Carrera, TipoDocumento = (select Descripcion from TiposDocumento td where td.Id = a.IdTipoDocumento), 
		a.NroDocumento, a.Nombre, a.Apellido, t.VtoCuota, t.FechaPago, t.Cuota, p.ImporteCuota, t.ImportePagado, Beca = ISNULL(ba.PorcentajeBeca, p.PorcBeca)
into #salida
from #temp t
inner join PlanesPago pp on t.IdPlanPago = pp.Id
inner join Pagos p on t.IdPago = p.Id
inner join Alumnos a on pp.IdAlumno = a.Id
inner join Cursos c on pp.IdCurso = c.Id
inner join Carreras c1 on c.IdCarrera = c1.Id
left join BecasAlumnos ba on ba.Id = p.IdBecaAlumno
where a.Estado	= 1
order by Carrera, Curso

if @IdCarrera > 0
	delete from #salida where IdCarrera <> @IdCarrera

if @IdCurso > 0
	delete from #salida where IdCurso <> @IdCurso

-- 0: todos, 1: sin beca, 2: con beca
if @TipoBecado	= 1	
	delete from #salida where ISNULL(beca, 0) > 0
else 
	if @TipoBecado	= 2
		delete from #salida where ISNULL(beca, 0) = 0	

-- 1: al día, 2: morosos
if @Tipo = 1
	select * from #salida where VtoCuota >= @Fecha order by Nombre
else
	select * from #salida where VtoCuota < @Fecha order by Nombre



