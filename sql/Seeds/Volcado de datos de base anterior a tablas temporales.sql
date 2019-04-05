use Temp
go

drop table #alumnos
go
select * into #alumnos from SMPorres..alumnos
go

update ALUMNOS
set
	DNI = case	when CODALUMNO = 1662 then 1 
				when CODALUMNO = 1926 then 2
			else 3 end 
where CODALUMNO in (1662, 1808, 1926)

begin tran datos

insert into #alumnos(
		Id,			IdTipoDocumento,	NroDocumento,				Nombre, 
		Apellido,	Direccion,			Sexo,						FechaNacimiento,
		EMail,		Estado
)
select	CODALUMNO,	1,					CONVERT(numeric(18), LTRIM(RTRIM((DNI)))),	APELLIDOYNOMBRE,  
		'',			DOMICILIO,			case when SEXO = 1 then 'M' else 'F' end, '1900/01/01',
		'',			1
from ALUMNOS
-- 4363 
go

drop table #carreras
go
select * into #carreras from SMPorres..Carreras
go
insert into #carreras(
	Id,				Nombre,			Duracion,		Estado,		FechaEstado)
SELECT 
	[codCarrera],	[Descripcion],	0,				1,			getdate()
FROM [Temp].[dbo].[CARRERAS]
-- 5
go

drop table #cursos
go
select * into #cursos from SMPorres..Cursos
go

insert into #cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select	codigo,		DESCCURSO,	11,				0,					0
FROM [dbo].[CURSOS]
where DESCCURSO like '%laboratorio%'
-- 3

insert into #cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select	codigo,		DESCCURSO,	12,				0,					0
FROM [dbo].[CURSOS]
where DESCCURSO like '%radiologia%'
-- 3

insert into #cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select	codigo,		DESCCURSO,	13,				0,					0
FROM [dbo].[CURSOS]
where DESCCURSO like '%hemoterapia%'
-- 3

insert into #cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select	codigo,		DESCCURSO,	14,				0,					0
FROM [dbo].[CURSOS]
where DESCCURSO like '%instrumentaci%'
-- 3

insert into #cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select	codigo,		DESCCURSO,	15,				0,					0
FROM [dbo].[CURSOS]
where DESCCURSO like '%social%'
-- 4

use SMPorres
go

begin tran datos

insert into Alumnos(
		Id,			IdTipoDocumento,	NroDocumento,				Nombre, 
		Apellido,	Direccion,			Sexo,						FechaNacimiento,
		EMail,		Estado)
select
		Id,			IdTipoDocumento,	NroDocumento,				Nombre, 
		Apellido,	Direccion,			Sexo,						FechaNacimiento,
		EMail,		Estado
from #alumnos
-- 4363

insert into Carreras(
	Id,				Nombre,			Duracion,		Estado,		FechaEstado)
select
	Id,				Nombre,			Duracion,		Estado,		FechaEstado
from #carreras
-- 5

insert into Cursos (
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula)
select
		Id,			Nombre,		IdCarrera,		ImporteCuota,		ImporteMatricula
from #cursos
-- 16

-- si todo ok	=> commit tran datos
-- sino			=> rollback tran datos
