create table Document
(
    Id int identity(1,1) primary key,
    DocumentDescription varchar(100) not null
);

create table Period
(
    DocumentId int not null references Document(Id),
    Id int identity(1,1) primary key,
    PeriodDescription varchar(100) not null
);

insert into Document(DocumentDescription) values('Document Description');

insert into Period(DocumentId, PeriodDescription) values(1, 'Period Description');



