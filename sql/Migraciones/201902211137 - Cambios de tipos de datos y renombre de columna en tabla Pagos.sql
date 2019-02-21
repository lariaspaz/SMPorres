alter table Pagos add PorcRecargo float null
alter table Pagos alter column PorcDescPagoTermino float null
alter table Pagos alter column PorcentajeBeca float null 
exec sp_rename 'Pagos.PorcentajeBeca', 'PorcBeca', 'COLUMN'
go
