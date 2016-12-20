with v1 as (
	select DepartamentoId, convert(varchar(100),Descricao) as Descricao, DepartamentoMasterId 
	from Departamento
	where DepartamentoMasterId is null
	union all
	select d.DepartamentoId, convert(varchar(100),convert(varchar(100),V1.Descricao) + ' | ' + convert(varchar(100),d.Descricao)) as Descricao, d.DepartamentoMasterId
	from Departamento d, v1
	where d.DepartamentoMasterId = v1.DepartamentoId
		
)

select *
from v1
order by Descricao