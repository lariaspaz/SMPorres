drop view vwPagos
go
create view vwPagos
as
select a.Nrodocumento,	IdAlumno = a.Id, IdCursoAlumno = ca.Id, IdPlanPago = pp.Id, IdPago = p.Id, ca.CicloLectivo, EstadoPlanPago = pp.Estado, 
	EstadoPago = p.Estado, FechaPago = p.Fecha, p.NroCuota, CuotasPlanPago = pp.CantidadCuotas
from Pagos p
inner join PlanesPago pp on pp.Id = p.IdPlanPago
inner join CursosAlumnos ca on ca.IdCurso = pp.IdCurso and ca.IdAlumno = pp.IdAlumno
inner join Alumnos a on a.Id = ca.IdAlumno
inner join Cuotas c on c.Cuota = p.NroCuota and c.CicloLectivo = ca.CicloLectivo
go
