

-- Table: dates

DROP TABLE IF EXISTS dates;

CREATE TABLE IF NOT EXISTS dates (id bigint, ddate date);

-- Insert test DATA's into table datas
-- generate random dataset without order by date

INSERT INTO dates(id, ddate) VALUES (1, '01/01/2021');
INSERT INTO dates(id, ddate) VALUES (1, '10/01/2021');
INSERT INTO dates(id, ddate) VALUES (1, '30/01/2021');

INSERT INTO dates(id, ddate) VALUES (2, '30/01/2022');
INSERT INTO dates(id, ddate) VALUES (2, '15/01/2022');
INSERT INTO dates(id, ddate) VALUES (2, '01/09/2022');

INSERT INTO dates(id, ddate) VALUES (3, '16/04/2022');
INSERT INTO dates(id, ddate) VALUES (3, '30/01/2022');
INSERT INTO dates(id, ddate) VALUES (3, '11/09/2022');
INSERT INTO dates(id, ddate) VALUES (3, '11/09/2022');
INSERT INTO dates(id, ddate) VALUES (3, '11/09/2022');

-- select with the 'LEAD' func to get next value
select 
  id, sd, ed
  from (
	select distinct -- to crop dublicate
	  id, ddate as sd,  lead(ddate) over (partition by id order by id, ddate) as ed
	from
	  dates
	order by
	  id, ddate
  ) as d
where
  ed is not null
;
