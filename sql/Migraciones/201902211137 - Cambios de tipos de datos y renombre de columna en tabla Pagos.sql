alter table Pagos add ImporteBeca numeric(18, 2) null
alter table Pagos add PorcRecargo float null
alter table Pagos add FechaVto datetime null
alter table Pagos alter column PorcDescPagoTermino float null
alter table Pagos alter column PorcentajeBeca float null 
exec sp_rename 'Pagos.PorcentajeBeca', 'PorcBeca', 'COLUMN'
exec sp_rename 'Pagos.Recargo', 'ImporteRecargo', 'COLUMN'
go
