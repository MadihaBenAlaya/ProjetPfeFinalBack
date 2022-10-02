ALTER TABLE public."Offres"
DROP COLUMN "diplome"

ALTER TABLE public."AspNetUsers"
Add COLUMN "diplome" text COLLATE pg_catalog."default"