begin tran lap 

-- se asigna estado Pagado pagos cancelados pero con estado Impago
update Pagos set estado = 3 where Fecha is not null and Estado = 1
-- 4189

-- se asigna estado Cancelado a planes de pago Vigentes con todas las cuotas pagadas
update PlanesPago
set
	Estado = 2
from
	PlanesPago pp, 
	vwPagos vw
where
	vw.IdPlanPago		= pp.Id		and

	vw.CicloLectivo		= 2019		and
	vw.EstadoPlanPago	= 1			and
	vw.FechaPago		is not null	and
	vw.CuotasPlanPago	< (
		select count(1) from vwPagos p 
		where 
			p.IdPlanPago = vw.IdPlanPago		and 
			p.EstadoPago = 3
	)
-- 98

-- actualiza el pr�ximo nro de cuota para los planes de pago Vigentes
update PlanesPago
set
	NroCuota = q.ProxCuota
from
	PlanesPago pp
	inner join vwPagos vw on vw.IdPlanPago = pp.Id
	inner join
		(	select IdPlanPago, ProxCuota = MAX(NroCuota + 1) 
			from vwPagos 
			where	FechaPago is not null	and 
					EstadoPago = 3			and 
					EstadoPlanPago = 1 
			group by IdPlanPago
		) q 
		on q.IdPlanPago = pp.Id
--681

-- actualiza el pr�ximo nro de cuota para los planes de pago Cancelados
update PlanesPago set NroCuota = CantidadCuotas where Estado = 2
-- 364

-- inserta alumnos en cursos que no estaban dados de alta
declare @maxid int
select @maxid = max(id) from CursosAlumnos
insert into CursosAlumnos(Id, IdCurso, IdAlumno, CicloLectivo)
select row# = @maxid + ROW_NUMBER() over (order by pp.IdCurso, pp.IdAlumno),  pp.IdCurso, pp.IdAlumno, CicloLectivo = case when pp.FechaGrabacion < '2019/12/01' then 2019 else 2020 end
from PlanesPago pp where IdAlumno not in (
	select IdAlumno from CursosAlumnos ca where ca.IdCurso = pp.IdCurso
)
order by row#
-- 30

-- commit tran lap
-- rollback tran lap
