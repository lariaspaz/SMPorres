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
	p.ImporteCuota	<> 4100			and	-- sea diferente a la del año 2019
	
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
	--Carrera = 'Técnico Superior en Hemoterapia'
	--Carrera = 'Técnico Superior en Laboratorio de Análisis Clínicos'
	--Carrera = 'Técnico Superior en Instrumentación Quirúrgica'
	--Carrera = 'Técnico Superior en Radiología'
	Carrera = 'Trabajador Social'
group by
	PlanPago,	Carrera,	Curso,	Alumno,
	Documento,	Matricula
order by
	Carrera,	Curso
--Carrera = 'Técnico Superior en Hemoterapia'						(66 filas afectadas)
--Carrera = 'Técnico Superior en Laboratorio de Análisis Clínicos'	(97 filas afectadas)
--Carrera = 'Técnico Superior en Instrumentación Quirúrgica'		(125 filas afectadas)
--Carrera = 'Técnico Superior en Radiología'						(174 filas afectadas)
--Carrera = 'Trabajador Social'										(109 filas afectadas)
--> 571 filas