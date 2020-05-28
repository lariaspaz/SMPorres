create unique index IX_Departamentos_Nombre on Departamentos(Nombre, IdProvincia)
go

create unique index IX_Localidades_Nombre on Localidades(Nombre, IdDepartamento)
go

create unique index IX_Barrios_Nombre on Barrios(Nombre, IdLocalidad)
go
