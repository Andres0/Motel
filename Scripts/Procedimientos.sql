--Exec ReportKardexItems '16E3CB34-A329-4871-AD63-DC920292EF1D', '47D89498-9B8C-41C3-BC0D-B4F57E552B04', '2018-04-04', '2019-03-04', 0
--exec dbo.ReportKardexItems @ItemId='16E3CB34-A329-4871-AD63-DC920292EF1D',@AlmacenId='47D89498-9B8C-41C3-BC0D-B4F57E552B04',@Inicio='2019-03-01 00:00:00',@Fin='2019-03-05 00:00:00',@SaldoAnterior=0
Alter PROCEDURE ReportKardexItems
	@ItemId as Uniqueidentifier,
	@AlmacenId as Uniqueidentifier,
	@Inicio as DateTime,
	@Fin as DateTime,
	@SaldoAnterior decimal
AS
BEGIN
	--SELECT Format(i.Fecha, 'dd/MM/yyyy') Fecha, 
	--CASE WHEN i.Tipo=1 THEN 'Ingreso ' + Cast(i.Numero as nvarchar)
	--	 WHEN i.Tipo=2 THEN 'Egreso ' + Cast(i.Numero as nvarchar)
	--	 WHEN i.Tipo=3 THEN 'Transferencia ' + Cast(i.Numero as nvarchar)
 --               ELSE '' END AS Numero,
	--CASE WHEN i.Tipo=1 OR (i.Tipo=3 AND i.AAlmacenId=@AlmacenId) THEN id.Cantidad END as Ingreso,
	--CASE WHEN i.Tipo=2 OR (i.Tipo=3 AND i.DeAlmacenId=@AlmacenId) THEN id.Cantidad END as Egreso,
	--@SaldoAnterior Saldo
	--FROM Inventario AS i INNER JOIN InventarioDetalle AS id ON i.InventarioId = id.InventarioId
	--WHERE id.ItemId = @ItemId AND i.Fecha >= @Inicio AND i.Fecha < @Fin AND (i.AAlmacenId = @AlmacenId OR i.DeAlmacenId = @AlmacenId)
	--Order by i.Fecha asc

	Declare @Fecha nvarchar(max), @Numero nvarchar(max), @Ingreso decimal, @Egreso decimal, @Saldo decimal, @Balance decimal
	Declare @ToReturn TABLE (Fecha nvarchar(max), Numero nvarchar(max), Ingreso decimal, Egreso decimal, Saldo decimal)

	SET @Balance = @SaldoAnterior

	DECLARE _cursor CURSOR FOR 

		SELECT Format(i.Fecha, 'dd/MM/yyyy') Fecha, 
			CASE WHEN i.Tipo=1 THEN 'Ingreso ' + Cast(i.Numero as nvarchar)
				 WHEN i.Tipo=2 THEN 'Egreso ' + Cast(i.Numero as nvarchar)
				 WHEN i.Tipo=3 THEN 'Transferencia ' + Cast(i.Numero as nvarchar)
				 ELSE '' END AS Numero,
			CASE WHEN i.Tipo=1 OR (i.Tipo=3 AND i.AAlmacenId=@AlmacenId) THEN id.Cantidad END as Ingreso,
			CASE WHEN i.Tipo=2 OR (i.Tipo=3 AND i.DeAlmacenId=@AlmacenId) THEN id.Cantidad END as Egreso,
				@SaldoAnterior Saldo
		FROM Inventario AS i INNER JOIN InventarioDetalle AS id ON i.InventarioId = id.InventarioId
		WHERE id.ItemId = @ItemId AND i.Fecha >= @Inicio AND i.Fecha < @Fin AND (i.AAlmacenId = @AlmacenId OR i.DeAlmacenId = @AlmacenId)
		Order by i.Fecha asc

	OPEN _cursor
	FETCH NEXT FROM _cursor INTO @Fecha, @Numero, @Ingreso, @Egreso, @Saldo
	WHILE @@fetch_status = 0
	BEGIN
		SET @Balance = @Balance + @Ingreso - @Egreso
		Insert Into @ToReturn Values (@Fecha, @Numero, @Ingreso, @Egreso, @Balance)
	    FETCH NEXT FROM _cursor INTO @Fecha, @Numero, @Ingreso, @Egreso, @Saldo
	END
	CLOSE _cursor
	DEALLOCATE _cursor

	Select Fecha, Numero, ISNULL(Ingreso,0) Ingreso, ISNULL(Egreso,0) Egreso, ISNULL(Saldo,0) Saldo From @ToReturn
END
