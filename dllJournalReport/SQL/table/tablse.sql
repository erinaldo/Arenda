CREATE TABLE [Arenda].[j_tMonthPlan](
	[id] [int] IDENTITY(1,1)	NOT NULL,
	[PeriodMonthPlan]	date	not null,
	[id_ObjectLease]	int		not null,
	[is—onfirmed]		bit		not null	default 0,
	[id_—onfirmed]		int		null,
	[Date—onfirmed]		datetime	null,
	[id_Editor] [int] NOT NULL,
	[DateEdit] [datetime] NOT NULL,
 CONSTRAINT [PK_j_tMonthPlan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_tMonthPlan]  WITH CHECK ADD  CONSTRAINT [FK_j_tMonthPlan_id_Editor] FOREIGN KEY([id_Editor])
REFERENCES [dbo].[ListUsers] ([id])
GO

ALTER TABLE [Arenda].[j_tMonthPlan] CHECK CONSTRAINT [FK_j_tMonthPlan_id_Editor]
GO

ALTER TABLE [Arenda].[j_tMonthPlan]  WITH CHECK ADD  CONSTRAINT [FK_j_tMonthPlan_id_ObjectLease] FOREIGN KEY([id_ObjectLease])
REFERENCES [Arenda].[s_ObjectLease] ([id])
GO

ALTER TABLE [Arenda].[j_tMonthPlan] CHECK CONSTRAINT [FK_j_tMonthPlan_id_ObjectLease]
GO



CREATE TABLE [Arenda].[j_MonthPlan](
	[id] [int] IDENTITY(1,1)	NOT NULL,
	[id_tMonthPlan]		int		not null,
	[id_Agreements]		int		not null,
	[SummaContract]		numeric(16,2) not null,
	[Discount]			numeric(16,2) not null,
	[Plan]				numeric(16,2) not null,
 CONSTRAINT [PK_j_MonthPlan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_MonthPlan]  WITH CHECK ADD  CONSTRAINT [FK_j_MonthPlan_id_tMonthPlan] FOREIGN KEY([id_tMonthPlan])
REFERENCES [Arenda].[j_tMonthPlan] ([id])
GO

ALTER TABLE [Arenda].[j_MonthPlan] CHECK CONSTRAINT [FK_j_MonthPlan_id_tMonthPlan]
GO

ALTER TABLE [Arenda].[j_MonthPlan]  WITH CHECK ADD  CONSTRAINT [FK_j_MonthPlan_id_Agreements] FOREIGN KEY([id_Agreements])
REFERENCES [Arenda].[j_Agreements] ([id])
GO

ALTER TABLE [Arenda].[j_MonthPlan] CHECK CONSTRAINT [FK_j_MonthPlan_id_Agreements]
GO



CREATE TABLE [Arenda].[j_tPlanReport](
	[id] [int] IDENTITY(1,1)	NOT NULL,
	[PeriodMonthPlan]	date	not null,
	[id_ObjectLease]	int		not null,
	[is—onfirmed]		bit		not null	default 0,
	[id_—onfirmed]		int		null,
	[Date—onfirmed]		datetime	null,
	[id_Editor] [int] NOT NULL,
	[DateEdit] [datetime] NOT NULL,
 CONSTRAINT [PK_j_tPlanReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_tPlanReport]  WITH CHECK ADD  CONSTRAINT [FK_j_tPlanReport_id_Editor] FOREIGN KEY([id_Editor])
REFERENCES [dbo].[ListUsers] ([id])
GO

ALTER TABLE [Arenda].[j_tPlanReport] CHECK CONSTRAINT [FK_j_tPlanReport_id_Editor]
GO

ALTER TABLE [Arenda].[j_tPlanReport]  WITH CHECK ADD  CONSTRAINT [FK_j_tPlanReport_id_ObjectLease] FOREIGN KEY([id_ObjectLease])
REFERENCES [Arenda].[s_ObjectLease] ([id])
GO

ALTER TABLE [Arenda].[j_tPlanReport] CHECK CONSTRAINT [FK_j_tPlanReport_id_ObjectLease]
GO



CREATE TABLE [Arenda].[j_PlanReport](
	[id] [int] IDENTITY(1,1)	NOT NULL,
	[id_tPlanReport]		int		not null,
	[id_Agreements]		int		not null,
	[SummaContract]		numeric(16,2) not null,
	[Discount]			numeric(16,2) not null,
	[SecurityPayment]		numeric(16,2) not null,
	[EndPlan]		numeric(16,2) not null,
	[Penalty]		numeric(16,2) not null,
	[OtherPayments]		numeric(16,2) not null,
	[TotalPaid]		numeric(16,2) not null,
	[Included]		numeric(16,2) not null,
	[Credit]		numeric(16,2) not null,
	[OverPayment]		numeric(16,2) not null,	
 CONSTRAINT [PK_j_PlanReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_PlanReport]  WITH CHECK ADD  CONSTRAINT [FK_j_PlanReport_id_tPlanReport] FOREIGN KEY([id_tPlanReport])
REFERENCES [Arenda].[j_tPlanReport] ([id])
GO

ALTER TABLE [Arenda].[j_PlanReport] CHECK CONSTRAINT [FK_j_PlanReport_id_tPlanReport]
GO

ALTER TABLE [Arenda].[j_PlanReport]  WITH CHECK ADD  CONSTRAINT [FK_j_PlanReport_id_Agreements] FOREIGN KEY([id_Agreements])
REFERENCES [Arenda].[j_Agreements] ([id])
GO

ALTER TABLE [Arenda].[j_PlanReport] CHECK CONSTRAINT [FK_j_PlanReport_id_Agreements]
GO