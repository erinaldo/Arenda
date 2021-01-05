USE [dbase2]
GO

/****** Object:  Table [Arenda].[j_LoadAccount1C]    Script Date: 05.01.2021 18:56:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Arenda].[j_LoadAccount1C](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Agreements] [int] NOT NULL,
	[id_Scan] [int] NOT NULL,
	[NumberAccount] [int] NOT NULL,
	[DateAccount] [date] NOT NULL,
	[NumberAgreement] [varchar](max) NOT NULL,
	[TypePayment] [varchar](max) NOT NULL,
	[DateSendMail] [datetime] NULL,
	[id_Loader] [int] NOT NULL,
	[DateLoad] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


