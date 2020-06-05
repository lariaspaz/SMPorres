select 
	PlanPago=pp.Id,				Carrera=cc.Nombre, Curso=c.Nombre, Alumno=a.Apellido +', '+ a.Nombre, 
	Documento=a.NroDocumento,	Matricula=p.importecuota
into #Planespago2020
from 
	pagos p,
	PlanesPago	pp,
	alumnos	a,
	Cursos	c,
	Carreras cc
where 
	pp.Id			= p.IdPlanPago	and	-- Todos los Planes de Pago
	pp.Estado		= 1				and	-- Activos

	p.NroCuota		= 0				and	-- Cuya matricula
	p.ImporteCuota	<> 4100			and	-- sea diferente a la del a�o 2019
	
	a.Id		= pp.IdAlumno	and	-- alumno
	
	c.Id		= pp.IdCurso	and	-- curso
	
	cc.Id		= c.IdCarrera		-- carrera
group by
	pp.Id,				cc.Nombre, c.Nombre, a.Apellido, a.Nombre, 
	a.NroDocumento,		p.importecuota
order by
	pp.Id, cc.Id,	c.Id
--(631 filas afectadas)

select 
	PlanPago,	Carrera,	Curso,	Alumno,
	Documento,	Matricula
from 
	#Planespago2020
group by
	PlanPago,	Carrera,	Curso,	Alumno,
	Documento,	Matricula
--(571 filas afectadas)


--> Select Final
select 
	PlanPago,	Carrera,	Curso,	Alumno,
	Documento,	Matricula
from 
	#Planespago2020
where
	--Carrera = 'T�cnico Superior en Hemoterapia'
	--Carrera = 'T�cnico Superior en Laboratorio de An�lisis Cl�nicos'
	--Carrera = 'T�cnico Superior en Instrumentaci�n Quir�rgica'
	--Carrera = 'T�cnico Superior en Radiolog�a'
	Carrera = 'Trabajador Social'
group by
	PlanPago,	Carrera,	Curso,	Alumno,
	Documento,	Matricula
order by
	Carrera,	Curso
--Carrera = 'T�cnico Superior en Hemoterapia'						(66 filas afectadas)
--Carrera = 'T�cnico Superior en Laboratorio de An�lisis Cl�nicos'	(97 filas afectadas)
--Carrera = 'T�cnico Superior en Instrumentaci�n Quir�rgica'		(125 filas afectadas)
--Carrera = 'T�cnico Superior en Radiolog�a'						(174 filas afectadas)
--Carrera = 'Trabajador Social'										(109 filas afectadas)
--> 571 filas