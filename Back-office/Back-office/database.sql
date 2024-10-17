Create database MiniProjetBiblio;
 use MiniProjetBiblio;

 Create table Utilisateur(
	id int primary key identity(1,1),
	nom varchar(50) not null,
	prenom varchar(50) not null,
	email varchar(50),
	password varchar(50),
	role varchar(50) not null
);

 Create table Adherent(
	id int primary key identity(1,1),
	nom varchar(50) not null,
	prenom varchar(50) not null,
	email varchar(50) not null,
	password varchar(50) not null,
	dateinscription date
);

Create table Document(
	id int primary key identity(1,1),
	libelle varchar(100) not null,
	dateajout date not null,
	stock int not null
);


Create table Reservation(
	id int primary key identity(1,1),
	datereservation date,
	datedebutemprunt date,
	datefinemprunt date,
	status varchar(50)
);

Create table Detail(
	idDocument int ,
	idReservation int ,
	primary key (idDocument,idReservation),
	CONSTRAINT FK_document FOREIGN KEY (idDocument) REFERENCES Document(id),
    CONSTRAINT FK_reserv FOREIGN KEY (idReservation) REFERENCES Reservation(id)

);


insert into Utilisateur values('Boulamaat','amina','amina@gmail.com','amina123','admin'),('aafif','khawla','khawla@gmail.com','aafif123','adherent');