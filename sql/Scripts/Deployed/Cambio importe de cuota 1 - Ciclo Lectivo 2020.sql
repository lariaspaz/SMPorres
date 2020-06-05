--exec sp_who
-->cuota 1 ya estaba modificada

--begin Transaction 
--go	
--	update Pagos
--	set	ImporteCuota = 2320
--	--	select count(1)
--	--select p.*
--	from
--		cursos		cu,
--		PlanesPago	pp,
--		Pagos		p
--	where
--		cu.IdCarrera	in (1,2)		and	--> Técnico Superior en Instrumentación Quirúrgica / Técnico Superior en Radiología
	
--		pp.IdCurso		= cu.Id			and
--		pp.Id			= p.IdPlanPago	and
	
--		p.NroCuota		= 1				and	--> vence en abril
--		p.ImporteCuota	= 2900			and	--> 2900 importe actual
--		p.importepagado	is null
--if @@ROWCOUNT = 230
--		commit transaction 
--else
--		rollback transaction 
--(230 filas afectadas)

		
	
--begin Transaction 
--go		
--	update Pagos
--	set	ImporteCuota = 1920
--	--select count(1)
--	--select p.id, p.ImporteCuota
--	from
--		cursos		cu,
--		PlanesPago	pp,
--		Pagos		p
--	where
--		cu.IdCarrera	in (3,4)		and	--> --> Hemoterapia / Trabajador Social
--	
--		pp.IdCurso		= cu.Id			and
--		pp.Id			= p.IdPlanPago	and
--	
--		p.NroCuota		= 1				and	--> vence en abril
--		p.ImporteCuota	= 2400			and	--> 2400 importe actual
--		p.importepagado	is null
--	--> 158
--if @@ROWCOUNT = 158
--		commit transaction 
--else
--		rollback transaction 


begin Transaction 
go		

	update Pagos
	set	ImporteCuota = 2160
	--select count(1)
	from
		cursos		cu,
		PlanesPago	pp,
		Pagos		p
	where
		cu.IdCarrera	= 5				and	--> Técnico Superior en Laboratorio
	
		pp.IdCurso		= cu.Id			and
		pp.Id			= p.IdPlanPago	and
	
		p.NroCuota		= 1				and	--> vence en abril
		p.ImporteCuota	= 2700			and	--> 2700 importe actual
		p.importepagado	is null
	-->92
if @@ROWCOUNT = 92
		commit transaction 
else
		rollback transaction 