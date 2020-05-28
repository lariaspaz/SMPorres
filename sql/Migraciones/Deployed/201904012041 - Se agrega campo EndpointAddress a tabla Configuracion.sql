alter table Configuracion add EndpointAddress varchar(255) NULL
go
update Configuracion set EndpointAddress = 'http://localhost:49963/Web_Services/SMP.asmx' 
go

alter table Configuracion alter column EndpointAddress varchar(255) NOT NULL
go