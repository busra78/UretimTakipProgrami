CREATE TRIGGER [Trigger]
	ON [dbo].[Urun]
	FOR DELETE
	AS
	BEGIN
		DELETE FROM StokGiris WHERE Urun.urunAdi=StokGiris.urun_Adi AND Urun.urunMarkasi=StokGiris.urun_Markasi AND Urun.urunTipi=StokGiris.urun_Tipi
		DELETE FROM StokCikis WHERE Urun.urunAdi=StokGiris.urun_Adi AND Urun.urunMarkasi=StokGiris.urun_Markasi AND Urun.urunTipi=StokGiris.urun_Tipi
		DELETE FROM StokTransfer WHERE Urun.urunAdi=StokGiris.urun_Adi AND Urun.urunMarkasi=StokGiris.urun_Markasi AND Urun.urunTipi=StokGiris.urun_Tipi
		DELETE FROM Depo_Stok_Urun WHERE Urun.urunAdi=StokGiris.urun_Adi AND Urun.urunMarkasi=StokGiris.urun_Markasi AND Urun.urunTipi=StokGiris.urun_Tipi


	END
