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
ALTER TABLE Arenda.[j_Fines] ADD MetersData numeric(16,2) null 
ALTER TABLE Arenda.[j_Fines] ADD PlanDate Date null 



ALTER TABLE Arenda.[s_AddPayment] ADD isMeter bit not null DEFAULT 0




/****** Object:  Table [Arenda].[j_tDiscount]    Script Date: 31.07.2020 9:15:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Arenda].[j_tDiscount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Agreements] [int] NOT NULL,
	[DateStart] [date] NOT NULL,
	[DateEnd] [date] NULL,
	[id_TypeDiscount] [int] NOT NULL,
	[id_StatusDiscount] [int] NOT NULL,
	[Discount] [numeric](16, 2) NOT NULL,
	[id_Editor] [int] NOT NULL,
	[DateEdit] [datetime] NOT NULL,
 CONSTRAINT [PK_j_tDiscount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_tDiscount]  WITH CHECK ADD  CONSTRAINT [FK_j_tDiscount_id_Editor] FOREIGN KEY([id_Editor])
REFERENCES [dbo].[ListUsers] ([id])
GO

ALTER TABLE [Arenda].[j_tDiscount] CHECK CONSTRAINT [FK_j_tDiscount_id_Editor]
GO

ALTER TABLE [Arenda].[j_tDiscount]  WITH CHECK ADD  CONSTRAINT [FK_j_tDiscount_id_StatusDiscount] FOREIGN KEY([id_StatusDiscount])
REFERENCES [Arenda].[s_StatusDiscount] ([id])
GO

ALTER TABLE [Arenda].[j_tDiscount] CHECK CONSTRAINT [FK_j_tDiscount_id_StatusDiscount]
GO

ALTER TABLE [Arenda].[j_tDiscount]  WITH CHECK ADD  CONSTRAINT [FK_j_tDiscount_id_TypeDiscount] FOREIGN KEY([id_TypeDiscount])
REFERENCES [Arenda].[s_TypeDiscount] ([id])
GO

ALTER TABLE [Arenda].[j_tDiscount] CHECK CONSTRAINT [FK_j_tDiscount_id_TypeDiscount]
GO



ALTER TABLE Arenda.[j_AdditionalDocuments] ADD Comment varchar(1024) null
GO