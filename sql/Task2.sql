-- Task 2. Part 1
-- with contact_type
select 
  clients.id,
  clients.client_name,
  cont.contact_type,
  cont.cont_count
from
(
  select 
    client_id, contact_type, count(client_id) as cont_count
  from
    client_contacts 
  group by
    client_id, contact_type
  order by
    client_id, contact_type
) as cont
left join clients on cont.client_id = clients.id
;

-- Task 2. Part 1
-- without contact_type
select 
  clients.id,
  clients.client_name,
  cont.cont_count
from
(
  select 
    client_id, count(client_id) as cont_count
  from
    client_contacts 
  group by
    client_id
  order by
    client_id
) as cont
left join clients on cont.client_id = clients.id
;

-- Task 2. Part 2
select 
  clients.id,
  clients.client_name,
  cont.cont_count
from
(
  select 
    client_id, count(client_id) as cont_count
  from
    client_contacts 
  group by
    client_id
  order by
    client_id
) as cont
left join clients on cont.client_id = clients.id
where cont.cont_count > 2
;

-- Task 2. Part 2
-- use having
select 
  clients.id,
  clients.client_name,
  cont.cont_count
from
(
  select 
    client_id, count(client_id) as cont_count
  from
    client_contacts 
  group by
    client_id
  having count(client_id) > 2
  order by
    client_id
) as cont
left join clients on cont.client_id = clients.id
;

