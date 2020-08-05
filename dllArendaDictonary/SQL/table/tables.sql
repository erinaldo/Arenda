CREATE TABLE [Arenda].[s_SavePayment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
 CONSTRAINT [PK_s_SavePayment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Arenda].[s_SavePayment] (cName,isActive)
values 
	('Платеж отсутствует',1),
	('Присутствует платеж типа «Последний месяц аренды»',1),
	('Присутствует платеж типа «Ремонтные работы»',1)	
GO

CREATE TABLE [Arenda].[j_AdditionalAgreements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Agreements]			int				not null,
	[RentalVacation]		int				not null default 0 ,
	[id_SavePayment]		int				null,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_j_AdditionalAgreements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_AdditionalAgreements] ADD CONSTRAINT FK_j_AdditionalAgreements_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

--ALTER TABLE [Arenda].[j_AdditionalAgreements] ADD CONSTRAINT FK_j_AdditionalAgreements_id_SavePayment FOREIGN KEY (id_SavePayment)  REFERENCES [Arenda].[s_SavePayment] (id)
--GO

ALTER TABLE [Arenda].[j_AdditionalAgreements] ADD CONSTRAINT FK_j_AdditionalAgreements_id_Agreements FOREIGN KEY (id_Agreements)  REFERENCES [Arenda].[j_Agreements] (id)
GO



CREATE TABLE [Arenda].[j_SealSections](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Agreements]			int				not null,
	[DateSeal]				date			not null,
	[DateOpen]				date			null,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_j_SealSections] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_SealSections] ADD CONSTRAINT FK_j_SealSections_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_SealSections] ADD CONSTRAINT FK_j_SealSections_id_Agreements FOREIGN KEY (id_Agreements)  REFERENCES [Arenda].[j_Agreements] (id)
GO



CREATE TABLE [Arenda].[s_ReclamaPlace](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_ObjectLease]		int					not null,
	[NumberPlace]			varchar(max)		not null,
	[id_Building]			int					not null,
	[Length]				bigint				not null,
	[Width]					bigint				not null,
	[isActive]				bit					not null default 1,
	[id_Editor]				int					null,
	[DateEdit]				datetime			null,
 CONSTRAINT [PK_s_ReclamaPlace] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[s_ReclamaPlace] ADD CONSTRAINT FK_s_ReclamaPlace_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[s_ReclamaPlace] ADD CONSTRAINT FK_s_ReclamaPlace_id_ObjectLease FOREIGN KEY (id_ObjectLease)  REFERENCES [Arenda].[s_ObjectLease] (id)
GO

ALTER TABLE [Arenda].[s_ReclamaPlace] ADD CONSTRAINT FK_s_ReclamaPlace_id_Building FOREIGN KEY (id_Building)  REFERENCES [Arenda].[s_Building] (id)
GO


CREATE TABLE [Arenda].[s_LandPlot](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_ObjectLease]		int					not null,
	[NumberPlot]			varchar(max)		not null,	
	[AreaPlot]				bigint				not null,	
	[isActive]				bit					not null default 1,
	[id_Editor]				int					null,
	[DateEdit]				datetime			null,
 CONSTRAINT [PK_s_LandPlot] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[s_LandPlot] ADD CONSTRAINT FK_s_LandPlot_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[s_LandPlot] ADD CONSTRAINT FK_s_LandPlot_id_ObjectLease FOREIGN KEY (id_ObjectLease)  REFERENCES [Arenda].[s_ObjectLease] (id)
GO



CREATE TABLE [Arenda].[s_TypeDiscount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
 CONSTRAINT [PK_s_TypeDiscount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Arenda].[s_TypeDiscount] (cName,isActive)
values 
	('% от общей стоимости аренды',1),
	('Снижение стоимости 1 квадратного метра',1)	
GO


CREATE TABLE [Arenda].[s_TypeTenant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
 CONSTRAINT [PK_s_TypeTenant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Arenda].[s_TypeTenant] (cName,isActive)
values 
	('Все арендаторы',1),
	('Новый арендатор',1),	
	('Существующий арендатор',1)	
GO

CREATE TABLE [Arenda].[s_TypeAgreements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
 CONSTRAINT [PK_s_TypeAgreements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Arenda].[s_TypeAgreements] (cName,isActive)
values 
	('Все договора',1),
	('Новый договор',1),	
	('Действующий договор',1)	
GO


CREATE TABLE [Arenda].[s_StatusDiscount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
 CONSTRAINT [PK_s_StatusDiscount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [Arenda].[s_StatusDiscount] (cName,isActive)
values 
	('Создана скидка',1),
	('Подтверждена скидка Д',1)	
GO


CREATE TABLE [Arenda].[j_tDiscount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateStart]				date		not null,
	[DateEnd]				date		null,
	[id_TypeDiscount]		int			not null,
	[id_TypeTenant]			int			not null,
	[id_TypeAgreements]		int			not null,
	[id_StatusDiscount]		int			not null,	
	[id_Editor]				int			null,
	[DateEdit]				datetime	null,
 CONSTRAINT [PK_j_tDiscount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_tDiscount] ADD CONSTRAINT FK_j_tDiscount_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_tDiscount] ADD CONSTRAINT FK_j_tDiscount_id_TypeDiscount FOREIGN KEY (id_TypeDiscount)  REFERENCES [Arenda].[s_TypeDiscount] (id)
GO

ALTER TABLE [Arenda].[j_tDiscount] ADD CONSTRAINT FK_j_tDiscount_id_TypeTenant FOREIGN KEY (id_TypeTenant)  REFERENCES [Arenda].[s_TypeTenant] (id)
GO

ALTER TABLE [Arenda].[j_tDiscount] ADD CONSTRAINT FK_j_tDiscount_id_TypeAgreements FOREIGN KEY (id_TypeAgreements)  REFERENCES [Arenda].[s_TypeAgreements] (id)
GO

ALTER TABLE [Arenda].[j_tDiscount] ADD CONSTRAINT FK_j_tDiscount_id_StatusDiscount FOREIGN KEY (id_StatusDiscount)  REFERENCES [Arenda].[s_StatusDiscount] (id)
GO


CREATE TABLE [Arenda].[j_DiscountValue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_tDiscount]			int				not null,
	[PercentDiscount]		numeric (10,2)	null,
	[DiscountPrice]			numeric (16,2)	null,
	[Price]					numeric (16,2)	null,
	[TotalPrice]			numeric (16,2)	null,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_j_DiscountValue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_DiscountValue] ADD CONSTRAINT FK_j_DiscountValue_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_DiscountValue] ADD CONSTRAINT FK_j_DiscountValue_id_TypeDiscount FOREIGN KEY (id_tDiscount)  REFERENCES [Arenda].[j_tDiscount] (id)
GO





CREATE TABLE [Arenda].[j_DiscountObject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_tDiscount]			int				not null,
	[id_ObjectLease]		int				not null,
	[id_Buildings]			int				null,
	[id_Floor]				int				null,
	[id_rentalObject]		int				not null,
	[typeRentalObject]		int				not null,
	--[id_Sections]			int				null,
	--[id_LandPlot]			int				null,
	--[id_ReclamaPlace]		int				null,
	[isException]			bit				not null default 1,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_j_DiscountObject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_DiscountObject] ADD CONSTRAINT FK_j_DiscountObject_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_DiscountObject] ADD CONSTRAINT FK_j_DiscountObject_id_tDiscount FOREIGN KEY (id_tDiscount)  REFERENCES [Arenda].[j_tDiscount] (id)
GO

ALTER TABLE [Arenda].[j_DiscountObject] ADD CONSTRAINT FK_j_DiscountObject_id_ObjectLease FOREIGN KEY (id_ObjectLease)  REFERENCES [Arenda].[s_ObjectLease] (id)
GO

--ALTER TABLE [Arenda].[j_DiscountObject] ADD CONSTRAINT FK_j_DiscountObject_id_Buildings FOREIGN KEY (id_Buildings)  REFERENCES [Arenda].[s_Building] (id)
--GO

--ALTER TABLE [Arenda].[j_DiscountObject] ADD CONSTRAINT FK_j_DiscountObject_id_Floor FOREIGN KEY (id_Floor)  REFERENCES [Arenda].[s_Floors] (id)
--GO




CREATE TABLE [Arenda].[s_TypeActivities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName]					varchar(max)	not null,	
	[isActive]				bit				not null	DEFAULT 1,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_s_TypeActivities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[s_TypeActivities] ADD CONSTRAINT FK_s_TypeActivities_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO