create database Supermarket;

use Supermarket;

create table Categories (
 Id int primary key identity (1,1) not null,
 Name varchar(50) not null,
 Description varchar(50) not null
)

insert into Categories
values ('name1', 'description1'),
       ('name2', 'description2'),
       ('name2', 'description3');

create table Products (
    Id int primary key identity (1,1) not null,
	Name varchar(50) not null,
	Price float not null,
	ExpirationDate date not null,
	CategoryId int foreign key references Categories(Id)
)

insert into Products
values ('name1', 1, CAST( GETDATE() AS Date ), 1),
       ('name2', 2, CAST( GETDATE() AS Date ), 2),
       ('name2', 3, CAST( GETDATE() AS Date ), 3);