USE [APP.IDENTITY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'32272212-9808-4071-b437-05088f4117e5', N'DEV', N'DEV', N'17411828-4f6f-47ee-9796-6338bece3d89')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'4cc59cbe-9996-441d-91db-81425c6f583d', N'FINANCE', N'FINANCE', N'08f318b8-05d9-445a-8796-a5f7e59ebfb6')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'68798605-59ef-4283-984d-4447ffd83275', N'CEO', N'CEO', N'0195a787-1533-4072-8f0b-e0fe73576597')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6917bd62-5a63-43ba-811f-92bf73fae6f6', N'SALES', N'SALES', N'2bca1b9b-4cd7-4e96-a372-58c2f8cfe1e5')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b0e2e097-ee2a-406b-9140-0277247c9351', N'HR', N'HR', N'10459feb-d7ac-47d6-b952-f61a62702f62')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'83a4d7d9-7b34-4a46-ad6a-27b861ea25d2', N'ceo@test.com', N'CEO@TEST.COM', N'ceo@test.com', N'CEO@TEST.COM', 1, N'DQy6wrwZgBpN+mLpCxSBMwrY1J9YS7mfCa6TpdRPpBo=', N'2e1ff815-9ea7-4be3-b0b7-88a2e6c88a27', N'57710ae6-594a-4a0f-af80-6cea850d3010', N'010-1234-1234', 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8c44e6be-50ca-4fd1-9011-acc8b3cc47bf', N'hr@test.com', N'HR@TEST.COM', N'hr@test.com', N'HR@TEST.COM', 1, N'mpRVYqikR2cexGZmVjOz79zf/0iGACsyFkWiwkt6RgM=', N'ec2b3c20-c848-454f-8d19-cbd365672060', N'c93de60c-bc34-486b-adef-ceacaa5cdf36', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'91495e7f-8a1b-4496-9ffd-4f2db295a3a0', N'sales@test.com', N'SALES@TEST.COM', N'sales@test.com', N'SALES@TEST.COM', 1, N'H7CuxYAZ+1qftR2o7S3LVqHIYe93vn6lXFL+tBKebWo=', N'f9881162-014f-4445-9962-85170e2d9ef4', N'9a4cbaa9-f32c-4ee8-9251-728b2e6e61a2', NULL, 0, 0, NULL, 0, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ae60f700-689e-4e53-bb3c-8c9d84befa47', N'develop@test.com', N'DEVELOP@TEST.COM', N'develop@test.com', N'DEVELOP@TEST.COM', 1, N'k1izYWKImE+lXoxrWavDYwuVNvLYRE1c8lkR2NrieRo=', N'ef6f4826-cf2e-4287-a3fc-5e1b102c797a', N'2411ab93-e46b-479a-823e-67de0ddf13bc', NULL, 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ae60f700-689e-4e53-bb3c-8c9d84befa47', N'32272212-9808-4071-b437-05088f4117e5')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'83a4d7d9-7b34-4a46-ad6a-27b861ea25d2', N'68798605-59ef-4283-984d-4447ffd83275')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'91495e7f-8a1b-4496-9ffd-4f2db295a3a0', N'6917bd62-5a63-43ba-811f-92bf73fae6f6')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8c44e6be-50ca-4fd1-9011-acc8b3cc47bf', N'b0e2e097-ee2a-406b-9140-0277247c9351')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220408072010_AppIdentity', N'6.0.3')
GO
