PRINT '----*** Data Insertion Start For Products Table ***-----'

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[Temp_InsertDataIn_Products]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[Temp_InsertDataIn_MasterData]
GO

CREATE PROCEDURE [Temp_InsertDataIn_MasterData]
(
	 @Name AS NVARCHAR(200)
	,@Brand AS NVARCHAR(200)
	,@Category NVARCHAR(200)
	,@Price AS decimal(16, 2)
	,@Description  NVARCHAR(MAX)
	,@ImageFileName AS NVARCHAR(200)
)
AS 
    BEGIN

		IF NOT EXISTS ( SELECT *
						    FROM Products (NOLOCK)
						    WHERE Name = @Name AND Brand=@Brand) 
		BEGIN
	        
			PRINT 'Inserting ' + @Name + ' in [Products] table'
				
			INSERT  INTO Products 
			( 
				Name
				,Brand
				,Category
				,Price
				,Description
				,ImageFileName
				,CreatedOn
			)
			VALUES  
			( 
				@Name,
				@Brand,
				@Category,
				@Price,
				@Description,
				@ImageFileName,
				GETDATE()
			)
		END
		ELSE 
			BEGIN
			
				PRINT @Name + ' already exist in [Products] table'
				
			END
		END
    
GO
 
EXEC Temp_InsertDataIn_MasterData 'MSI Pulse', 'MSI', 'Laptops', 1750.34, 'Pulse GL66 is a portable gaming laptop packed with up to 11th Gen. Intel® Core™ i7 processor and the NVIDIA® GeForce RTX™ 3060 graphics, cooled by cooler', '1001.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Acer Swift', 'Acer', 'Laptops', 2750.34, 'The 16-inch display is OLED and has a high resolution of 3840 x 2400 pixels. It''s mighty sharp. Though the refresh rate is just 60Hz', '1002.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Lenovo Ideapad', 'Lenovo', 'Laptops', 1750.34, 'Experience augmented user experience with the IdeaPad Pro 5i gen 9 14″ laptop, which boasts intuitive and optimized features with the built-in Lenovo AI Engine', '1003.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Dell Latitude 7000', 'Dell', 'Laptops', 3750.34, 'Elite design and reliability. The thin, lightweight Latitude 7000 Series Ultrabooks offer mobility at its finest, with the 12-inch model starting at just.', '1004.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Oppo A31', 'Oppo', 'Mobiles', 1750.34, 'A31 ; RAM · 4GB/ 6GB ; Storage · 64GB/ 128GB (Expandable up to 256GB) ; Size · 16.5cm (6.5'') ; Touchscreen · Multi-touch, Capacitive Screen, Gorilla Glass 3', '1005.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Motorola edge 40', 'Motorola', 'Mobiles', 1750.34, 'Motorola Edge 40 · Released 2023, May 04 · 167g or 171g, 7.6mm thickness · Android 13, up to Android 14 · 128GB/256GB storage, no card slot', '1006.jpeg'
EXEC Temp_InsertDataIn_MasterData 'iPhone 15 Pro', 'Apple', 'Mobiles', 1750.34, 'iPhone 15 Pro and iPhone 15 Pro Max. Titanium design. A17 Pro chip. Action button. 48MP Main camera. USB-C.', '1007.jpeg'
EXEC Temp_InsertDataIn_MasterData 'Nokia magic max 2023', 'Nokia', 'Mobiles', 1750.34, 'The Nokia Magic Max 2023 Mobile Phone features a 6.9 inches Super AMOLED display with a screen resolution of 1080 x 2400 Pixel', '1008.jpeg'
EXEC Temp_InsertDataIn_MasterData 'HP - Smart Tank 67', 'HP', 'Printers', 1750.34, 'HP - Smart Tank 670 All-in-One Wireless Ink Tank Colour Printer,Scanner,Copier with Auto-Duplex,High Capacity Tank,B&W Prints at Rs 0.10/Page*,Color', '1009.jpeg'
EXEC Temp_InsertDataIn_MasterData 'HP - Laserjet Pro MFP', 'Sub Call DDL', 'Printers', 1750.34, 'HP - Laserjet Pro MFP M128fn All-in-One Monochrome Printer USB+Ethernet+WiFi,Laser Multifunction Printer (Print,Copy,Scan)-Simple Setup with HP Smart', '1010.jpeg'

GO
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'[dbo].[Temp_InsertDataIn_Products]')
                    AND type IN ( N'P', N'PC' ) ) 
    DROP PROCEDURE [dbo].[Temp_InsertDataIn_Products]
GO

PRINT '----*** Data Insertion End For Products Table ***-----'