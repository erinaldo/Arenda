CREATE TABLE [Arenda].[s_StatusPenalty](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[cName]			varchar(1024)	not null,
	[isActive]		bit				not null default 1
 CONSTRAINT [PK_s_StatusPenalty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [Arenda].[s_StatusPenalty] (cName,isActive)
	values ('Пени не отображать',1),('Пени отображать',1),('Начисление пени подтверждена',1)
GO

CREATE TABLE [Arenda].[j_tPenalty](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_Agreements]	int				not null,
	[PeriodCredit]	date			not null,
	[id_StatusPenalty]	int			not null,	
	[id_Creator]	int				not	null,
	[DateCreate]	datetime		null,
	[id_Editor]		int				null,
	[DateEdit]		datetime		null,
 CONSTRAINT [PK_j_tPenalty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Arenda].[j_tPenalty] ADD CONSTRAINT FK_j_tPenalty_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_tPenalty] ADD CONSTRAINT FK_j_tPenalty_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [Arenda].[j_tPenalty] ADD CONSTRAINT FK_j_tPenalty_id_Agreements FOREIGN KEY (id_Agreements)  REFERENCES [Arenda].[j_Agreements] (id)
GO

ALTER TABLE [Arenda].[j_tPenalty] ADD CONSTRAINT FK_j_tPenalty_id_StatusPenalty FOREIGN KEY (id_StatusPenalty)  REFERENCES [Arenda].[s_StatusPenalty] (id)
GO

CREATE TABLE [Arenda].[j_Penalty](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[id_tPenalty]	int				not null,
	[SummaCredit]	numeric(12,2)	not null,
	[PercentPenalty]numeric(4,2)	not null,
	[SummaPenalty]	numeric(12,2)	not null,
	[id_PaymentContract]	int		not null,
	[CountDaysCredit]		int		not null,
	[id_Editor]		int				null,
	[DateEdit]		datetime		null
 CONSTRAINT [PK_j_Penalty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [Arenda].[j_Penalty] ADD CONSTRAINT FK_j_Penalty_id_tPenalty FOREIGN KEY (id_tPenalty)  REFERENCES [Arenda].[j_tPenalty] (id)
GO

ALTER TABLE [Arenda].[j_Penalty] ADD CONSTRAINT FK_j_Penalty_id_PaymentContract FOREIGN KEY (id_PaymentContract)  REFERENCES [Arenda].[j_PaymentContract] (id)
GO

ALTER TABLE [Arenda].[j_Penalty] ADD CONSTRAINT FK_j_Penalty_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO