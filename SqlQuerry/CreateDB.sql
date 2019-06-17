create database ComputerAccessoriesStore;

go
use ComputerAccessoriesStore;

go
create table Products
(
	Id int primary key identity(1,1),
	[Name] nvarchar(50) not null,
	[Description] nvarchar(500) not null,
	[Price] decimal(16,2) not null,
	[Category] nvarchar(50) not null
);

go
insert into Products([Name], [Description], [Price], [Category])
values	('N1', 'D1', 1050, 'C1'),
		('N2', 'D2', 1050, 'C2'),
		('N3', 'D3', 1050, 'C3'),
		('N4', 'D4', 1050, 'C1'),
		('N5', 'D5', 1050, 'C2'),
		('N6', 'D6', 1050, 'C3'),
		('N7', 'D7', 1050, 'C1')