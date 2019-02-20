ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_MediosPago] FOREIGN KEY([IdMedioPago])
REFERENCES [dbo].[MediosPago] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_MediosPago]
GO
