USE [MTCatalogDB]
GO
SET IDENTITY_INSERT [dbo].[AppRole] ON 
GO
INSERT [dbo].[AppRole] ([roleid], [rolename], [createddate]) VALUES (1, N'Admin', CAST(N'2023-03-25T09:15:56.417' AS DateTime))
GO
INSERT [dbo].[AppRole] ([roleid], [rolename], [createddate]) VALUES (2, N'OrgAdmin', CAST(N'2023-03-25T09:15:56.417' AS DateTime))
GO
INSERT [dbo].[AppRole] ([roleid], [rolename], [createddate]) VALUES (3, N'OrgUser', CAST(N'2023-03-25T09:15:56.417' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AppRole] OFF
GO
SET IDENTITY_INSERT [dbo].[AppRoute] ON 
GO
INSERT [dbo].[AppRoute] ([routeid], [routepath], [isexclude], [isactive], [createddate]) VALUES (1, N'*', 0, 1, CAST(N'2023-03-25T15:12:50.610' AS DateTime))
GO
INSERT [dbo].[AppRoute] ([routeid], [routepath], [isexclude], [isactive], [createddate]) VALUES (2, N'/organization/deleteorganization', 1, 1, CAST(N'2023-03-25T15:12:50.610' AS DateTime))
GO
INSERT [dbo].[AppRoute] ([routeid], [routepath], [isexclude], [isactive], [createddate]) VALUES (3, N'/get', 0, 1, CAST(N'2023-03-25T15:12:50.610' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AppRoute] OFF
GO
SET IDENTITY_INSERT [dbo].[AppRoleRoute] ON 
GO
INSERT [dbo].[AppRoleRoute] ([rolerouteid], [routeid], [roleid], [isactive], [createddate]) VALUES (1, 1, 1, 1, CAST(N'2023-03-25T15:14:43.357' AS DateTime))
GO
INSERT [dbo].[AppRoleRoute] ([rolerouteid], [routeid], [roleid], [isactive], [createddate]) VALUES (2, 1, 2, 1, CAST(N'2023-03-25T15:14:43.357' AS DateTime))
GO
INSERT [dbo].[AppRoleRoute] ([rolerouteid], [routeid], [roleid], [isactive], [createddate]) VALUES (3, 2, 2, 1, CAST(N'2023-03-25T15:14:43.357' AS DateTime))
GO
INSERT [dbo].[AppRoleRoute] ([rolerouteid], [routeid], [roleid], [isactive], [createddate]) VALUES (4, 3, 3, 1, CAST(N'2023-03-25T15:14:43.357' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AppRoleRoute] OFF
GO
