/****** Object:  StoredProcedure [dbo].[ConsTotalPagos]    Script Date: 05/03/2019 06:40:40 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Author:		Leonardo Arias Paz
-- Create date: 2019-03-05
-- Description:	Consulta los montos y cantidades de cuotas totales cobrados en un período a 
--				un curso o carrera.
-- ==========================================================================================
ALTER PROCEDURE [dbo].[ConsTotalPagos]
@Desde		smalldatetime,
@Hasta		smalldatetime,
@IdCarrera	int,
@IdCurso	int,
@MedPago	int				
AS

SET NOCOUNT ON
SET FMTONLY OFF

create table #temp(
	IdCurso		int				not null,
	IdCarrera	int				not null,
	IdMedPago	int				not null,
	Cantidad	int				not null,
	Total		numeric(18,2)	not null
)

create table #medPagos(
	Id		int				not null,
	Descripcion varchar(50)			null
)

insert into #medPagos (
	Id, Descripcion
)
select
	Id, Descripcion
from MediosPago

if isnull(@MedPago,0) <> 0
begin
	delete #medPagos
	where Id <> @MedPago
end

	
insert into #temp (
		IdCurso,		IdCarrera,		IdMedPago,				Cantidad,				
		Total
)
select	pp.IdCurso,		c.IdCarrera,	mp.Id,			Cantidad = COUNT(1),	
		Total = SUM(p.ImportePagado) 
from Pagos p
inner join PlanesPago pp on pp.Id = p.IdPlanPago
inner join Cursos c on c.Id = pp.IdCurso
inner join #medPagos mp on mp.Id = p.IdMedioPago
where p.fecha between @Desde and @Hasta
group by pp.IdCurso,	c.IdCarrera,		mp.Id

if @IdCarrera > 0 delete from #temp where IdCarrera <> @IdCarrera

if @IdCurso > 0 delete from #temp where IdCurso <> @IdCurso

select	Carrera = c1.Nombre,		Curso = c.Nombre,	t.IdCarrera,	t.IdCurso,
		MedioPago = mp.Descripcion, t.Cantidad,			t.Total
from #temp t
inner join Cursos c on t.IdCurso = c.Id
inner join Carreras c1 on t.IdCarrera = c1.Id
inner join #medPagos mp on t.IdMedPago = mp.Id
order by Carrera, Curso
