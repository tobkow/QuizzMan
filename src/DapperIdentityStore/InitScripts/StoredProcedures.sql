USE [QuizzMan.System]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_Insert')
DROP PROCEDURE [Users_Insert]
GO

CREATE PROC [dbo].[Users_Insert]  
(  
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
INSERT INTO [dbo].[Users](UserName, NormalizedUserName, PasswordHash, SecurityStamp, Email, NormalizedEmail, EmailConfirmed, AccessFailedCount, LockoutEnabled, LockoutEnd, PhoneNumber, PhoneNumberConfirmed, 
                        TwoFactorEnabled, ConcurrencyStamp) VALUES (@UserName,@NormalizedUserName,@PasswordHash,@SecurityStamp,@Email,@NormalizedEmail,@EmailConfirmed,@AccessFailedCount,@LockoutEnabled,@LockoutEnd,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled,@ConcurrencyStamp);  
RETURN  
END  
	
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_Update')
DROP PROCEDURE [Users_Update]
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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_Delete')
DROP PROCEDURE [Users_Delete]
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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_GetById')
DROP PROCEDURE [Users_GetById]
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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_GetByNormalizedName')
DROP PROCEDURE [Users_GetByNormalizedName]
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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_GetByNormalizedEmail')
DROP PROCEDURE [Users_GetByNormalizedEmail]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserRoles_AddUserToRole')
DROP PROCEDURE [UserRoles_AddUserToRole]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserClaims_Create')
DROP PROCEDURE [UserClaims_Create]

GO

CREATE PROC [dbo].[UserClaims_Create]  
(  
@UserId int,
@ClaimType nvarchar(max),
@ClaimValue nvarchar(max)
)  
AS  
BEGIN  
INSERT INTO [dbo].[UserClaims](UserId, ClaimType, ClaimValue) VALUES(@UserId,@ClaimType,@ClaimValue)
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserClaims_DeleteClaimForUser')
DROP PROCEDURE [UserClaims_DeleteClaimForUser]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserClaims_GetClaimsForUser')
DROP PROCEDURE [UserClaims_GetClaimsForUser]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_GetUsersByClaim')
DROP PROCEDURE [Users_GetUsersByClaim]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserLogins_Create')
DROP PROCEDURE [UserLogins_Create]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserLogins_GetByProviderAndKey')
DROP PROCEDURE [UserLogins_GetByProviderAndKey]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserLogins_GetByUserId')
DROP PROCEDURE [UserLogins_GetByUserId]

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
	
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserLogins_DeleteForUserAndProvider')
DROP PROCEDURE [UserLogins_DeleteForUserAndProvider]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Users_GetUsersInRole')
DROP PROCEDURE [Users_GetUsersInRole]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_GetRolesForUser')
DROP PROCEDURE [Roles_GetRolesForUser]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_GetByName')
DROP PROCEDURE [Roles_GetByName]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_GetById')
DROP PROCEDURE [Roles_GetById]

GO

CREATE PROC [dbo].[Roles_GetById]  
(  
@RoleId nvarchar(max)
)  
AS  
BEGIN  
SELECT * FROM [dbo].[Roles] WHERE Id=@RoleId
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_Create')
DROP PROCEDURE [Roles_Create]

GO

CREATE PROC [dbo].[Roles_Create]  
(  
@Name nvarchar(max),
@NormalizedName nvarchar(max),
@ConcurrencyStamp nvarchar(max)
)  
AS  
BEGIN  
INSERT INTO [dbo].[Roles](Name, NormalizedName, ConcurrencyStamp) VALUES(@Name, @NormalizedName, @ConcurrencyStamp)
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_Update')
DROP PROCEDURE [Roles_Update]

GO

CREATE PROC [dbo].[Roles_Update]  
(
@RoleId int,  
@Name nvarchar(max),
@NormalizedName nvarchar(max),
@ConcurrencyStamp nvarchar(max)
)  
AS  
BEGIN  
UPDATE [dbo].[Roles] SET Name=@Name, NormalizedName=@NormalizedName, ConcurrencyStamp=@ConcurrencyStamp WHERE Id=@RoleId
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_Delete')
DROP PROCEDURE [Roles_Delete]

GO

CREATE PROC [dbo].[Roles_Delete]  
(  
@RoleId int
)  
AS  
BEGIN  
DELETE FROM [dbo].[Roles] WHERE Id=@RoleId
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserRoles_DeleteByUserIdAndRoleId')
DROP PROCEDURE [UserRoles_DeleteByUserIdAndRoleId]

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

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'RoleClaims_Create')
DROP PROCEDURE [RoleClaims_Create]

GO

CREATE PROC [dbo].[RoleClaims_Create]  
(  
@RoleId int,
@ClaimType nvarchar(max),
@ClaimValue nvarchar(max)
)  
AS  
BEGIN  
INSERT INTO [dbo].[RoleClaims](RoleId, ClaimType, ClaimValue) VALUES(@RoleId, @ClaimType, @ClaimValue)
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'RoleClaims_Delete')
DROP PROCEDURE [RoleClaims_Delete]

GO

CREATE PROC [dbo].[RoleClaims_Delete]  
(  
@Id int
)  
AS  
BEGIN  
DELETE FROM [dbo].[RoleClaims] WHERE Id=@Id
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_GetByRole')
DROP PROCEDURE [Roles_GetByRole]

GO

CREATE PROC [dbo].[Roles_GetByRole]  
(  
@RoleId int
)  
AS  
BEGIN  
SELECT * FROM [dbo].[RoleClaims] WHERE RoleId=@RoleId
RETURN  
END  

GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Roles_Get')
DROP PROCEDURE [Roles_Get]

GO

CREATE PROC [dbo].[Roles_Get]  
(  
@RoleId int,
@ClaimType nvarchar(max),
@ClaimValue nvarchar(max)
)  
AS  
BEGIN  
SELECT * FROM [dbo].[RoleClaims] WHERE RoleId=@RoleId AND ClaimType=@ClaimType AND ClaimValue=@ClaimValue
RETURN  
END  

GO
