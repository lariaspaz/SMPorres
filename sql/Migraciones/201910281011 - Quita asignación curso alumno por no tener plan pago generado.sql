use SMPorres

--select 
--	* 
--from 
--	CursosAlumnos ca,
--	alumnos a,
--	PlanesPago pp
--where
--	pp.IdAlumno = a.Id	and
--	pp.IdCurso = ca.IdCurso	and
--	ca.IdAlumno = a.Id	and
--	a.NroDocumento = 39897781
--
--select 
--	* 
--from 
--	CursosAlumnos ca,
--	alumnos a
--where
--	ca.IdAlumno = a.Id	and
--	a.NroDocumento = 39897781


-- Asignación curso-alumno incorrecta
delete CursosAlumnos where id = 167