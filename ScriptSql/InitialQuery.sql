



CREATE TABLE Owner(
IdOwner int identity,
Name nvarchar(500) not null,
Address nvarchar(1000)not null,
Photo nvarchar(max),
Birthday datetime,
CONSTRAINT Pk_Owner_IdOwner primary key (IdOwner)
);

GO

CREATE TABLE Property(
IdProperty int identity,
Name nvarchar(500),
Address nvarchar(1000),
Price decimal(15,2),
CodeInternal nvarchar(20),
Year int,
IdOwner int not null,
CONSTRAINT Pk_Property_IdProperty   PRIMARY KEY (IdProperty),
CONSTRAINT Fk_Property_IdOwner FOREIGN KEY (IdOwner)  REFERENCES Owner(IdOwner)
);

GO

CREATE TABLE PropertyImage(
IdPropertyImage int identity,
IdProperty int not null,
FileImage nvarchar(max),
Enabled bit,
CONSTRAINT Pk_PropertyImage_IdPropertyImage   PRIMARY KEY (IdProperty),
CONSTRAINT Fk_PropertyImage_IdProperty FOREIGN KEY (IdProperty)  REFERENCES Property(IdProperty)
);


GO


CREATE TABLE PropertyTrace(
IdPropertyTrace int identity,
DateSale date,
Name nvarchar(500),
Value Decimal(20,2),
Tax Decimal(10,2),
IdProperty int not null,
CONSTRAINT Pk_PropertyTrace_IdPropertyTrace  PRIMARY KEY (IdPropertyTrace),
CONSTRAINT Fk_PropertyTrace_IdProperty FOREIGN KEY (IdProperty)  REFERENCES Property(IdProperty)
);

GO


CREATE TABLE Users(
IdUser int identity,
UserName nvarchar(500) not null,
Password nvarchar(max) not null
CONSTRAINT Pk_User_IdUser  PRIMARY KEY (IdUser)
);


GO

CREATE TABLE Configuration(
Id nvarchar(500) not null,
Value nvarchar(500) not null,
Description nvarchar(2000),
CONSTRAINT Pk_Configuration_Id  PRIMARY KEY (Id)
)

GO

INSERT INTO Configuration VALUES 
('JwtSecretKey', 'MIIEBAKCAQEAqCAqEALgjhNplvRhv4HE7XR3R9jCBqpU+DcPb', ''),
('JwtIssuerToken', 'http://190.66.12.50:8909',''),
('JwtAudienceToken', 'http://190.66.12.50:8909',''),
('JwtExpireTime', '600',''),   
('KeyEncrypted', 'UIGEmQDhcbmohf9A',''),
('IVEncrypted', 'UIGEmQDh','');

GO






