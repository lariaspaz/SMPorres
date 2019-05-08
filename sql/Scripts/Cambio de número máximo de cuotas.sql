declare @nrocuotas int
select @nrocuotas = 9

if exists(select * from pagos where fecha is not null and NroCuota > @nrocuotas) begin
	print 'No se puede continuar. Hay cuotas pagadas.'
	return
end

begin tran lap

update PlanesPago set CantidadCuotas = @nrocuotas

delete from Cuotas where Cuota > @nrocuotas

delete from pagos where NroCuota > @nrocuotas and fecha is null

update PlanesPago
set Estado = @nrocuotas
from PlanesPago pp
inner join (
	select IdPlanPago, k = count(1) 
	from pagos 
	where fecha is not null 
	group by IdPlanPago
) q on q.IdPlanPago = pp.Id
where q.k = @nrocuotas

-- commit tran lap
-- rollback tran lap

