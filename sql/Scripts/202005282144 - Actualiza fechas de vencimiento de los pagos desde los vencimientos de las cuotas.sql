update Pagos
set
	FechaVto = c.VtoCuota
from Pagos p
inner join PlanesPago pp on pp.Id = p.IdPlanPago
inner join CursosAlumnos ca on ca.IdCurso = pp.IdCurso and ca.IdAlumno = pp.IdAlumno
inner join Cuotas c on c.Cuota = p.NroCuota and c.CicloLectivo = ca.CicloLectivo
where
	p.FechaVto is null
