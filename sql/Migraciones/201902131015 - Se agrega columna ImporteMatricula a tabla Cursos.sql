alter table Cursos add ImporteMatricula numeric(18, 2) NULL
go

update Cursos set ImporteMatricula = 0
go

alter table Cursos alter column ImporteMatricula numeric(18, 2) NOT NULL
go

