create schema [test_al]

create table [test_al].[User](
	id int identity(1,1) primary key,
	username varchar(50) not null,
	password varchar(255) not null,
	name varchar(30) not null,
	lastname varchar(30) not null,
	active bit,
	attemps int
)

create table [test_al].Currency(
	id int identity(1,1) primary key not null,
	code varchar(3) not null,
	description varchar(50),
	active bit
)


create table [test_al].Office (
	id int identity(1,1) primary key not null,
	code int not null,
	identification varchar(50) not null,
	description varchar(250) not null,
	address varchar(50) not null,
	currency int foreign key references [test_al].[Currency](id),
	create_user int foreign key references [test_al].[User](id),
	create_date date,
	modify_user int foreign key references [test_al].[User](id),
	modify_date date,
	active bit
)

create table [test_al].[Log](
	id int identity(1,1) primary key not null,
	description varchar(255) not null,
	user_id int foreign key references [test_al].[User](id) not null,
	log_date date not null
)

select * from [test_al].[User]
select * from [test_al].[Currency]
select * from [test_al].[Office]
select * from [test_al].[Log]