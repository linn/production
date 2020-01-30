create view failed_parts_report_view as 
select
f.part_number,
p.description,
f.qty,
f.batch_ref,
f.date_booked,
f.stock_rotation_date,
f.total_value,
f.remarks stock_locator_remarks,
au.user_name created_by,
c.code cit_code,
c.name cit_name,
f.storage_place,
p.preferred_supplier,
supp.supplier_name
from v_tqms_fflagged f,
parts p, suppliers supp,
cits c, auth_user_name_view au
where f.part_number =  p.part_number 
and which_cit(p.part_number) = c.code (+) 
and p.accounting_company = 'LINN'
and f.created_by = au.user_number
and p.preferred_supplier = supp.supplier_id (+)
and f.being_verified = 'N'
