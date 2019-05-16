drop table #temp
drop table #pagos
go

create table #temp (
	nrodoc		numeric(18),
	beca		float,
	idCarrera	int				null
)

set nocount on

insert into #temp(nrodoc, beca, idCarrera) values(40827326, 0.5, 3)
insert into #temp(nrodoc, beca, idCarrera) values(41747481, 0.7, 3)
insert into #temp(nrodoc, beca, idCarrera) values(40781714, 0.7, 3)
insert into #temp(nrodoc, beca, idCarrera) values(36620623, 0.7, 3)

insert into #temp(nrodoc, beca, idCarrera) values(34694523, 0.7, 4)
insert into #temp(nrodoc, beca, idCarrera) values(39581885, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(38641949, 0.7, 4)
insert into #temp(nrodoc, beca, idCarrera) values(35086783, 0.7, 4)
insert into #temp(nrodoc, beca, idCarrera) values(41362839, 0.7, 4)
insert into #temp(nrodoc, beca, idCarrera) values(37139230, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(43623608, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(40526942, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(40899112, 0.7, 4)
insert into #temp(nrodoc, beca, idCarrera) values(37214148, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(32859975, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(40445750, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(39056316, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(42021044, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(37700709, 0.5, 4)
insert into #temp(nrodoc, beca, idCarrera) values(34183075, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(39452373, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(33210191, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(35844290, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(36928714, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(38556449, 0.3, 4)
insert into #temp(nrodoc, beca, idCarrera) values(40445908, 0.3, 4)

insert into #temp(nrodoc, beca, idCarrera) values(35052246, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(37410819, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(33749320, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(39794376, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40025969, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41839315, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41748649, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(34314254, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40169731, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40287723, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41797249, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40526599, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40753658, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41937109, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40899267, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(33092789, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(42388208, 0.5, 1)

insert into #temp(nrodoc, beca, idCarrera) values(42521933, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(42698057, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(42388477, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41747607, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(42889949, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41720798, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(38368508, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(38640474, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(38735139, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(33627316, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(39450447, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(4016977, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(35053536, 0.3, 1)
insert into #temp(nrodoc, beca, idCarrera) values(40168868, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41838975, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41362602, 0.7, 1)
insert into #temp(nrodoc, beca, idCarrera) values(34981348, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(26368845, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(34994220, 0.5, 1)
insert into #temp(nrodoc, beca, idCarrera) values(41720982, 0.3, 1)

insert into #temp(nrodoc, beca, idCarrera) values(40332636, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42014754, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(39581206, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(43288329, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41748428, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(37130284, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(4226211, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(38721316, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(40686942, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42522760, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(35917393, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42288038, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(34913213, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42889539, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(40332627, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41480892, 0.2, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42074430, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(40050828, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(40827054, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41362639, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41344671, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41838275, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(42140249, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(40939861, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41093013, 0.3, 2)
insert into #temp(nrodoc, beca, idCarrera) values(38707959, 0.2, 2)
insert into #temp(nrodoc, beca, idCarrera) values(39900253, 0.5, 2)
insert into #temp(nrodoc, beca, idCarrera) values(41222270, 0.5, 2)

select t.nrodoc, t.beca, p.PorcBeca, p.Id
into #pagos
from #temp t
inner join Alumnos a on a.NroDocumento = t.nrodoc
inner join PlanesPago pp on pp.IdAlumno = a.Id
inner join Cursos c on c.Id = pp.IdCurso and c.IdCarrera = t.idCarrera
inner join Pagos p on p.IdPlanPago = pp.Id
inner join Cuotas c2 on c2.Cuota = p.NroCuota
where 
	c2.VtoCuota >= '2019/05/01'	and
	c2.Cuota	> 0

if exists(
	select * from #temp t where nrodoc not in (
		select nrodoc from #pagos
	)
) begin
	select * from #temp t where nrodoc not in (
		select nrodoc from #pagos
	)
	RAISERROR ('Hay alumnos inexistentes', 16, 1)
	--return
end

set nocount off

begin tran lap

update Pagos
set
	PorcBeca = t.beca
from #pagos t
inner join Pagos p on p.Id = t.Id

-- commit tran lap
-- rollback tran lap



--select * from PlanesPago 
--select * from Cursos
--select * from Carreras
--select * from Pagos
--select * from #temp
