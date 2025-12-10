USE master
CREATE DATABASE LeadGenerator
GO

CREATE LOGIN LeadGeneratorApp WITH PASSWORD=N'N3#hkcZf4iuUnW5u#cWQV33R24&65M#4', DEFAULT_DATABASE=LeadGenerator, DEFAULT_LANGUAGE=us_english, CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE LeadGenerator
CREATE USER LeadGeneratorApp FOR LOGIN LeadGeneratorApp
GO

ALTER ROLE db_owner ADD MEMBER LeadGeneratorApp
GO

CREATE SCHEMA [User]
GO

CREATE SCHEMA [Address]
GO

CREATE SCHEMA [Company]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Address].[Geolocation](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Latitude] [nvarchar](10) NULL,
	[Longitude] [nvarchar](10) NULL,
 CONSTRAINT [PK_Geolocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Address].[Address](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Street] [nvarchar](50) NULL,
	[Suite] [nvarchar](50) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[GeolocationId] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Address].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Geolocation] FOREIGN KEY([GeolocationId])
REFERENCES [Address].[Geolocation] ([Id])
GO

ALTER TABLE [Address].[Address] CHECK CONSTRAINT [FK_Address_Geolocation]
GO

CREATE TABLE [Company].[Company](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CatchPhrase] [nvarchar](200) NULL,
	[BusinessStrategy] [nvarchar](200) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [User].[ContactDetails](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Website] [nvarchar](100) NULL,
 CONSTRAINT [PK_ContactDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [User].[User](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](100) NULL,
	[ContactDetailsId] [int] NULL,
	[IntegrationId] [int] NULL,
	[AddressId] [int] NULL,
	[CompanyId] [int] NULL,
	[DateCreated] [datetime2] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [User].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Address] FOREIGN KEY([AddressId])
REFERENCES [Address].[Address] ([Id])
GO

ALTER TABLE [User].[User] CHECK CONSTRAINT [FK_User_Address]
GO

ALTER TABLE [User].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Company] FOREIGN KEY([CompanyId])
REFERENCES [Company].[Company] ([Id])
GO

ALTER TABLE [User].[User] CHECK CONSTRAINT [FK_User_Company]
GO

ALTER TABLE [User].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_ContactDetails] FOREIGN KEY([ContactDetailsId])
REFERENCES [User].[ContactDetails] ([Id])
GO

ALTER TABLE [User].[User] CHECK CONSTRAINT [FK_User_ContactDetails]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Filip Rakić
-- Create date: 7.12.2023.
-- Description:	Save new user to database
-- =============================================
CREATE PROCEDURE [User].spInsertUser
	@Latitude nvarchar(10) = NULL,
	@Longitude nvarchar(10) = NULL,

	@Street nvarchar(50) = NULL,
	@Suite nvarchar(50) = NULL,
	@City nvarchar(100) = NULL,
	@ZipCode nvarchar(20) = NULL,

	@CompanyName nvarchar(100) = NULL,
	@CatchPhrase nvarchar(200) = NULL,
	@BusinessStrategy nvarchar(200) = NULL,

	@Email nvarchar(320) = NULL,
	@Phone nvarchar(50) = NULL,
	@Website nvarchar(100) = NULL,

	@FirstName nvarchar(100) = NULL, 
	@LastName nvarchar(100) = NULL,
	@Username nvarchar(100) = NULL,
	@IntegrationId int = NULL,
	@DateCreated datetime2 = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [LeadGenerator].[Address].[Geolocation] (Latitude, Longitude)
	VALUES (@Latitude, @Longitude)
	DECLARE @GeoId int
	SELECT @GeoId = SCOPE_IDENTITY()

	INSERT INTO [LeadGenerator].[Address].[Address] (Street, Suite, City, ZipCode, GeolocationId)
	VALUES (@Street, @Suite, @City, @ZipCode, @GeoId)
	DECLARE @AddressId int
	SELECT @AddressId = SCOPE_IDENTITY()

	INSERT INTO [LeadGenerator].[Company].[Company] ([Name], CatchPhrase, BusinessStrategy)
	VALUES (@CompanyName, @CatchPhrase, @BusinessStrategy)
	DECLARE @CompanyId int
	SELECT @CompanyId = SCOPE_IDENTITY()

	INSERT INTO [LeadGenerator].[User].[ContactDetails] (Email, Phone, Website)
	VALUES (@Email, @Phone, @Website)
	DECLARE @ContactDetailsId int
	SELECT @ContactDetailsId = SCOPE_IDENTITY()

	INSERT INTO [LeadGenerator].[User].[User] (FirstName, LastName, Username, ContactDetailsId, IntegrationId, AddressId, CompanyId, DateCreated)
	VALUES (@FirstName, @LastName, @Username, @ContactDetailsId, @IntegrationId, @AddressId, @CompanyId, @DateCreated)
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Filip Rakić
-- Create date: 8.02.2023.
-- Description:	Retrieve the date of last insertion for a user based on their email
-- =============================================
CREATE PROCEDURE [User].spGetDateCreatedByEmail 
	@Email nvarchar(320) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP(1) u.DateCreated
	FROM [LeadGenerator].[User].[User] u
	JOIN [LeadGenerator].[User].[ContactDetails] cd
	ON cd.[Id] = u.[ContactDetailsId]
	WHERE cd.Email = @Email
	ORDER BY 1 DESC
END
GO

-- =============================================
-- Author:		Filip Rakić
-- Create date: 9.02.2023.
-- Description:	Get users whose email addresses end in '.biz'
-- =============================================
CREATE VIEW [User].[vwBusinessUsers]
AS
SELECT	u.Id, u.FirstName, u.LastName, cd.Email, cd.Phone, cd.Website
FROM		[User].[User] AS u
INNER JOIN	[User].ContactDetails AS cd
ON 		cd.Id = u.ContactDetailsId
WHERE		(cd.Email LIKE '%.biz')
GO