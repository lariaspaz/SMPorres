select * from alumnos
select * from TiposDocumento
select * from cursos
select * from CursosAlumnos
select * from alumnos
select * from AlumnosWeb
select * from Carreras
select * from PagosWeb

begin tran lap

declare @id int

select @id = 21

delete Pagos from Pagos p inner join PlanesPago pp on p.IdPlanPago = pp.Id where pp.IdAlumno = @id
delete PlanesPago where IdAlumno = @id
delete CursosAlumnos where IdAlumno = @id
delete Alumnos where id = @id

delete PagosWeb from PagosWeb p inner join CursosAlumnosWeb ca on p.IdCursoAlumno = ca.Id where ca.IdAlumnoWeb = @id
delete CursosAlumnosWeb where IdAlumnoWeb = @id
delete AlumnosWeb where id = @id

-- commit tran lap
-- rollback tran lap
