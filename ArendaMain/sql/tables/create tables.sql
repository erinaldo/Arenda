CREATE TABLE [Arenda].[s_LandlordTenantBank](
	[id]				int IDENTITY(1,1)	NOT NULL,	
	[id_LandlordTenant]	int			not null,
	[id_Bank]			int			not null,
	[PaymentAccount]	varchar(20)	null,
	--[OKPO]				varchar(10)	null,
	--[KPP]				varchar(9)	null,
	--[OGRN]				varchar(12)	null,
	[isActive]			bit			not null	default 1,
	[id_Creator]		int			NOT NULL,
	[DateCreate]		datetime	NOT NULL,
	[id_Editor]			int			NULL,
	[DateEdit]			datetime	NULL,
 CONSTRAINT [PK_s_LandlordTenantBank] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[s_LandlordTenantBank]  WITH CHECK ADD  CONSTRAINT [FK_s_LandlordTenantBank_id_Creator] FOREIGN KEY([id_Creator])
REFERENCES [dbo].[ListUsers] ([id])
GO

ALTER TABLE [Arenda].[s_LandlordTenantBank]  WITH CHECK ADD  CONSTRAINT [FK_s_LandlordTenantBank_id_Editor] FOREIGN KEY([id_Editor])
REFERENCES [dbo].[ListUsers] ([id])
GO

ALTER TABLE [Arenda].[s_LandlordTenantBank]  WITH CHECK ADD  CONSTRAINT [FK_s_LandlordTenantBank_id_LandlordTenant] FOREIGN KEY([id_LandlordTenant])
REFERENCES [Arenda].[s_Landlord_Tenant] ([id])
GO

ALTER TABLE [Arenda].[s_LandlordTenantBank]  WITH CHECK ADD  CONSTRAINT [FK_s_LandlordTenantBank_id_Bank] FOREIGN KEY([id_Bank])
REFERENCES [Arenda].[s_Banks] ([id])
GO





CREATE TABLE [Arenda].[j_AgreementsBank](
	[id]				int IDENTITY(1,1)	NOT NULL,	
	[id_Agreements]	int			not null,
	[id_LandlordTenantBank]		int			not null,
 CONSTRAINT [PK_j_AgreementsBank] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_AgreementsBank]  WITH CHECK ADD  CONSTRAINT [FK_j_AgreementsBank_id_Agreements] FOREIGN KEY([id_Agreements])
REFERENCES [Arenda].[j_Agreements] ([id])
GO

ALTER TABLE [Arenda].[j_AgreementsBank]  WITH CHECK ADD  CONSTRAINT [FK_j_AgreementsBank_id_LandlordTenantBank] FOREIGN KEY([id_LandlordTenantBank])
REFERENCES [Arenda].[s_LandlordTenantBank] ([id])
GO