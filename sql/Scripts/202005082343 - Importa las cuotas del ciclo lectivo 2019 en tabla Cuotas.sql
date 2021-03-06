update cuotas set CicloLectivo = 2020, VtoCuota = DATEADD(year, 1, vtoCuota), id = id + 10

INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (1, 0, CAST(N'2019-12-31 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (2, 1, CAST(N'2019-04-30 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (3, 2, CAST(N'2019-05-15 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (4, 3, CAST(N'2019-06-18 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (5, 4, CAST(N'2019-07-15 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (6, 5, CAST(N'2019-08-15 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (7, 6, CAST(N'2019-09-16 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (8, 7, CAST(N'2019-10-15 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (9, 8, CAST(N'2019-11-15 00:00:00' AS SmallDateTime), 2019)
INSERT [dbo].[Cuotas] ([Id], [Cuota], [VtoCuota], [CicloLectivo]) VALUES (10, 9, CAST(N'2019-12-16 00:00:00' AS SmallDateTime), 2019)

select * from Cuotas
