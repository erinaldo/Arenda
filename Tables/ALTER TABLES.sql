
ALTER TABLE  Arenda.j_Agreements ADD id_Creator int null
ALTER TABLE  Arenda.j_Agreements ADD DateCreate DateTime null

ALTER TABLE  Arenda.j_Agreements ADD id_Editor int null
ALTER TABLE  Arenda.j_Agreements ADD DateEdit DateTime null

ALTER TABLE [Arenda].[j_PaymentContract] ADD Description varchar(1024) not null DEFAULT ''