USE [APP.DB]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408073042_AppDb', N'6.0.3')
GO
INSERT [dbo].[USERS] ([USER_CD], [USER_NM], [USER_DEPT], [USER_EMAIL], [USER_CONTACT], [REMARK]) VALUES (N'178f6db7-30b7-4cba-b672-708e92274809', N'김사장', NULL, N'ceo@test.com', N'010-1234-1234', NULL)
INSERT [dbo].[USERS] ([USER_CD], [USER_NM], [USER_DEPT], [USER_EMAIL], [USER_CONTACT], [REMARK]) VALUES (N'25536a5c-c748-4a26-92bc-7cc71227f59d', N'유영업', N'영업부', N'sales@test.com', N'010-1234-1234', NULL)
INSERT [dbo].[USERS] ([USER_CD], [USER_NM], [USER_DEPT], [USER_EMAIL], [USER_CONTACT], [REMARK]) VALUES (N'260cc165-6bbc-45fe-aca2-e7d0be54a78a', N'김개발', N'개발팀', N'develop@test.com', N'010-1234-1234', NULL)
INSERT [dbo].[USERS] ([USER_CD], [USER_NM], [USER_DEPT], [USER_EMAIL], [USER_CONTACT], [REMARK]) VALUES (N'87b11ba8-e2b7-48e7-8f6c-881e837e80e2', N'이인간', N'인사팀', N'hr@test.com', N'010-1234-1234', NULL)
GO
