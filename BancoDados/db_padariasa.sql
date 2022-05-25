drop database dbPadariaSA;

create database dbPadariaSA;

use dbPadariaSA;


create table tbfuncionarios(
codfunc int not null auto_increment,
nome varchar(100) not null,
salario decimal(9,2) default 0.0 check(salario >=0.0),
cpf char(14) not null unique,
sexo char(1) default 'F' check(sexo in('F','M')),
primary key(codfunc));

create table tbUsuarios(
codUsu int not null auto_increment,
nome varchar(50) not null,
senha varchar(50) not null,
codfunc int not null,
primary key(codUsu),
foreign key(codfunc)references tbfuncionarios(codfunc));

create table tbProdutos(
codProd int not null auto_increment,
descricao varchar(200) not null,
precoVenda decimal(9,2),
precoCompra decimal(9,2),
estoqueAtual decimal(9,2),
primary key(codProd));

insert into tbfuncionarios(nome,salario,cpf,sexo)
	values ('Marco Antonio',1500.50,'258.352.685-77','M');

insert into tbfuncionarios(nome,salario,cpf,sexo)
	values ('Kaique de Melo',1800.50,'257.252.885-87','M');


insert into tbUsuarios(nome,senha,codfunc)
	values('marco.antonio','123456',1);

insert into tbUsuarios(nome,senha,codfunc)
	values('Kaique.melo','123456',2);

-- insert into tbProdutos(descricao,precoVenda,	precoCompra,estoqueAtual) values('Farinha',10.50,8.50,30);
-- insert into tbProdutos(descricao,precoVenda,precoCompra,estoqueAtual) values('Leite',4.79,3.50,40);
-- insert into tbProdutos(descricao,precoVenda,precoCompra,estoqueAtual) values('Acucar',3.75,2.50,50);

select * from tbfuncionarios;
select * from tbUsuarios;
select * from tbProdutos;