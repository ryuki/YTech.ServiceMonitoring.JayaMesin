USE [DB_WOT_JAYA_MESIN]
GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 03/16/2014 09:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
GO
