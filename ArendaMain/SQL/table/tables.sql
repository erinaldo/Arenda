CREATE TABLE [Arenda].[s_PayType](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[cName]			varchar(1024)	not null,
	[isActive]		bit				not null default 1
 CONSTRAINT [PK_s_PayType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [Arenda].[s_PayType] (cName,isActive)
	values ('Обеспечительный платеж',1),('Оплата аренды',1),('Дополнительная оплата',1)
GO


ALTER TABLE Arenda.[j_PaymentContract] ADD id_PayType int null 
ALTER TABLE Arenda.[j_PaymentContract] ADD isToTenant bit null 
ALTER TABLE Arenda.[j_PaymentContract] ADD isCash bit null 
ALTER TABLE Arenda.[j_PaymentContract] ADD id_Fines int null 
ALTER TABLE Arenda.[j_PaymentContract] ADD PlaneDate Date null 


ALTER TABLE Arenda.[j_Fines] ADD isConfirmed bit null 
ALTER TABLE Arenda.[j_Fines] ADD MetersData int null 
ALTER TABLE Arenda.[j_Fines] ADD PlanDate Date null 