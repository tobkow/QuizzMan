USE [QuizzMan.System]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Users_Insert]  
   (  
    @Id int,
	@UserName nvarchar(256),
	@NormalizedUserName nvarchar(256),
	@PasswordHash nvarchar(max),
	@SecurityStamp nvarchar(max),
	@Email nvarchar(256),
	@NormalizedEmail nvarchar(256),
	@EmailConfirmed bit,
	@AccessFailedCount int,
	@LockoutEnabled bit,
	@LockoutEnd datetimeoffset(7),
	@PhoneNumber nvarchar(max),
	@PhoneNumberConfirmed bit,
	@TwoFactorEnabled bit,
	@ConcurrencyStamp nvarchar(max)
    )  
    AS  
    BEGIN  
    INSERT INTO [dbo].[Users](Id, UserName, NormalizedUserName, PasswordHash, SecurityStamp, Email, NormalizedEmail, EmailConfirmed, AccessFailedCount, LockoutEnabled, LockoutEnd, PhoneNumber, PhoneNumberConfirmed, 
                         TwoFactorEnabled, ConcurrencyStamp) VALUES (@Id,@UserName,@NormalizedUserName,@PasswordHash,@SecurityStamp,@Email,@NormalizedEmail,@EmailConfirmed,@AccessFailedCount,@LockoutEnabled,@LockoutEnd,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled,@ConcurrencyStamp);  
    RETURN  
    END  
	
	GO

	CREATE PROC [dbo].[Users_Update]  
   (  
    @UserId int,
	@UserName nvarchar(256),
	@NormalizedUserName nvarchar(256),
	@PasswordHash nvarchar(max),
	@SecurityStamp nvarchar(max),
	@Email nvarchar(256),
	@NormalizedEmail nvarchar(256),
	@EmailConfirmed bit,
	@AccessFailedCount int,
	@LockoutEnabled bit,
	@LockoutEnd datetimeoffset(7),
	@PhoneNumber nvarchar(max),
	@PhoneNumberConfirmed bit,
	@TwoFactorEnabled bit,
	@ConcurrencyStamp nvarchar(max)  
    )  
    AS  
    BEGIN  
    UPDATE [dbo].[Users] SET UserName=@Username,NormalizedUserName=@NormalizedUserName,PasswordHash=@PasswordHash,SecurityStamp=@SecurityStamp,Email=@Email,NormalizedEmail=@NormalizedEmail,EmailConfirmed=@EmailConfirmed,AccessFailedCount=@AccessFailedCount,
	LockoutEnabled=@LockoutEnabled,LockoutEnd=@LockoutEnd,PhoneNumber=@PhoneNumber,PhoneNumberConfirmed=@PhoneNumberConfirmed,TwoFactorEnabled=@TwoFactorEnabled,ConcurrencyStamp=@ConcurrencyStamp WHERE Id=@UserId;  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Users_Delete]  
	(  
	@UserId int  
	)  
    AS  
    BEGIN  
    DELETE FROM [dbo].[Users] WHERE Id=@UserId  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Users_GetById]  
	(  
	@UserId int
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Users] WHERE Id=@UserId  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Users_GetByNormalizedName]  
	(  
	@NormalizedUserName nvarchar(256)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Users] WHERE NormalizedUserName=@NormalizedUserName  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Users_GetByNormalizedEmail]  
	(  
	@NormalizedEmail nvarchar(256)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Users] WHERE NormalizedEmail=@NormalizedEmail  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserRoles_AddUserToRole]  
	(  
	@UserId int,
	@RoleId int
	)  
    AS  
    BEGIN  
    INSERT INTO [dbo].[UserRoles](UserId, RoleId) VALUES(@UserId,@RoleId)
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserClaims_Create]  
	(  
	@Id int,
	@UserId int,
	@ClaimType nvarchar(max),
	@ClaimValue nvarchar(max)
	)  
    AS  
    BEGIN  
    INSERT INTO [dbo].[UserClaims](Id, UserId, ClaimType, ClaimValue) VALUES(@Id,@UserId,@ClaimType,@ClaimValue)
    RETURN  
    END  

	GO
	
	CREATE PROC [dbo].[UserClaims_DeleteClaimForUser]  
	(  
	@UserId int,
	@ClaimType nvarchar(max),
	@ClaimValue nvarchar(max)
	)  
    AS  
    BEGIN  
    DELETE FROM [dbo].[UserClaims] WHERE UserId=@UserId AND ClaimType=@ClaimType AND ClaimValue=@ClaimValue
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserClaims_GetClaimsForUser]  
	(  
	@UserId int
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[UserClaims] WHERE UserId=@UserId  
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Users_GetUsersByClaim]  
	(  
	@ClaimType nvarchar(max),
	@ClaimValue nvarchar(max)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Users] WHERE Id IN (SELECT UserId FROM [dbo].[UserClaims] WHERE ClaimType=@ClaimType AND ClaimValue=@ClaimValue)
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserLogins_Create]  
	(  
	@UserId int,
	@LoginProvider nvarchar(128),
	@ProviderDisplayName nvarchar(256),
	@ProviderKey nvarchar(128)
	)  
    AS  
    BEGIN  
    INSERT INTO [dbo].[UserLogins](UserId, LoginProvider, ProviderDisplayName, ProviderKey) VALUES (@UserId,@LoginProvider,@ProviderDisplayName,@ProviderKey)
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserLogins_GetByProviderAndKey]  
	(  
	@LoginProvider nvarchar(128),
	@ProviderKey nvarchar(128)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[UserLogins] WHERE LoginProvider=@LoginProvider AND ProviderKey=@ProviderKey
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserLogins_GetByUserId]  
	(  
	@UserId int
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[UserLogins] WHERE UserId=@UserId
    RETURN  
    END  

	GO
	
	CREATE PROC [dbo].[UserLogins_DeleteForUserAndProvider]  
	(  
	@UserId int,
	@LoginProvider nvarchar(128),
	@ProviderKey nvarchar(128)
	)  
    AS  
    BEGIN  
    DELETE FROM [dbo].[UserLogins] WHERE UserId=@UserId AND LoginProvider=@LoginProvider AND ProviderKey=@ProviderKey
    RETURN  
    END  

	GO
	
	CREATE PROC [dbo].[Users_GetUsersInRole]  
	(  
	@Name nvarchar(max)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Users] WHERE Id IN (SELECT UserId FROM [dbo].[UserRoles] WHERE RoleId IN (SELECT Id FROM [dbo].[Roles] WHERE Name=@Name))
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Roles_GetRolesForUser]  
	(  
	@UserId int
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Roles] WHERE Id IN (SELECT RoleId FROM [dbo].[UserRoles] WHERE UserId = @UserId)
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[Roles_GetByName]  
	(  
	@Name nvarchar(max)
	)  
    AS  
    BEGIN  
    SELECT * FROM [dbo].[Roles] WHERE Name=@Name
    RETURN  
    END  

	GO

	CREATE PROC [dbo].[UserRoles_DeleteByUserIdAndRoleId]  
	(  
	@UserId int,
	@RoleId int
	)  
    AS  
    BEGIN  
    DELETE FROM [dbo].[UserRoles] WHERE UserId=@UserId AND RoleId=@RoleId
    RETURN  
    END  

	GO