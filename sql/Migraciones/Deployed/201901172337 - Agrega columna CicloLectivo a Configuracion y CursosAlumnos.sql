alter table Configuracion add [CicloLectivo] [smallint] NOT NULL DEFAULT YEAR(GetDate())
go

alter table CursosAlumnos add [CicloLectivo] [smallint] NOT NULL DEFAULT YEAR(GetDate())
go

