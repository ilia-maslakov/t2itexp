-- Table: public.clients

-- DROP TABLE IF EXISTS public.clients;

CREATE TABLE IF NOT EXISTS public.clients
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 399999999 CACHE 1 ),
    client_name character varying(200) COLLATE pg_catalog."default",
    CONSTRAINT clients_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.clients
    OWNER to postgres;
    
-- Table: public.client_contacts

-- DROP TABLE IF EXISTS public.client_contacts;

CREATE TABLE IF NOT EXISTS public.client_contacts
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 399999999 CACHE 1 ),
    client_id bigint,
    contact_type character varying(255) COLLATE pg_catalog."default",
    contact_value character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT client_contacts_pkey PRIMARY KEY (id),
    CONSTRAINT clients_ref_fkey FOREIGN KEY (client_id)
        REFERENCES public.clients (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.client_contacts
    OWNER to postgres;
    


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

