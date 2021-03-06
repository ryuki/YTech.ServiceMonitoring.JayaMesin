USE [DB_WOT_JAYA_MESIN]
GO
/****** Object:  Table [dbo].[T_WO]    Script Date: 03/16/2014 09:43:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_WO](
	[WO_ID] [nvarchar](50) NOT NULL,
	[WO_NO] [nvarchar](50) NULL,
	[CUSTOMER_ID] [nvarchar](50) NULL,
	[WO_DATE] [datetime] NULL,
	[WO_ITEM_TYPE] [nvarchar](50) NULL,
	[WO_ITEM_SN] [nvarchar](50) NULL,
	[WO_IS_GUARANTEE] [bit] NULL,
	[WO_EQUIPMENTS] [nvarchar](max) NULL,
	[WO_SC_STORE] [nvarchar](50) NULL,
	[WO_PRIORITY] [nvarchar](50) NULL,
	[WO_START_DATE] [datetime] NULL,
	[WO_LAST_STATUS] [nvarchar](50) NULL,
	[WO_EST_FINISH_DATE] [datetime] NULL,
	[WO_TOTAL] [decimal](18, 0) NULL,
	[WO_DP] [decimal](18, 0) NULL,
	[WO_INVOICE_NO] [nvarchar](50) NULL,
	[WO_TAKEN_DATE] [datetime] NULL,
	[WO_BROKEN_DESC] [nvarchar](max) NULL,
	[WO_DESC] [nvarchar](max) NULL,
	[DATA_STATUS] [nvarchar](50) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[ROW_VERSION] [timestamp] NULL,
	[WO_COMPLAIN] [nvarchar](max) NULL,
 CONSTRAINT [PK_T_WO] PRIMARY KEY CLUSTERED 
(
	[WO_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[T_WO]  WITH CHECK ADD  CONSTRAINT [FK_T_WO_M_CUSTOMER] FOREIGN KEY([CUSTOMER_ID])
REFERENCES [dbo].[M_CUSTOMER] ([CUSTOMER_ID])
GO
ALTER TABLE [dbo].[T_WO] CHECK CONSTRAINT [FK_T_WO_M_CUSTOMER]
GO
