/*
use master 
go

drop database QuanLyBanSach
go
*/

create database QuanLyBanSach
go

use QuanLyBanSach
go

CREATE TABLE KhachHang
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	TaiKhoan Varchar(50) UNIQUE,
	MatKhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiaChiKH nVarchar(200),
	DienThoaiKH Varchar(50),	
	NgaySinh DATETIME
	CONSTRAINT PK_KhachHang PRIMARY KEY(MaKH)
)
go

Create Table ChuDe
(
	MaCD int Identity(1,1),
	TenChuDe nvarchar(50) NOT NULL,
	CONSTRAINT PK_ChuDe PRIMARY KEY(MaCD)
)
go

Create Table NhaXuatBan
(
	MaNXB int identity(1,1),
	TenNXB nvarchar(50) NOT NULL,
	DiaChi NVARCHAR(200),
	DienThoai VARCHAR(50),
	CONSTRAINT PK_NhaXuatBan PRIMARY KEY(MaNXB)
)
go

CREATE TABLE Sach
(
	MaSach INT IDENTITY(1,1),
	TenSach NVARCHAR(100) NOT NULL,
	GiaBan Decimal(18,0) CHECK (GiaBan>=0),
	MoTa NVarchar(Max),
	AnhBia VARCHAR(50),
	NgayCapNhat DATETIME,
	SoLuongTon INT,
	MaCD INT,
	MaNXB INT,
	Constraint PK_Sach Primary Key(MaSach),
	Constraint FK_Sach_ChuDe Foreign Key(MaCD) References ChuDe(MaCD),
	Constraint FK_Sach_NhaXB Foreign Key(MaNXB)References NhaXuatBan(MANXB)
)
go

CREATE TABLE TacGia
(
	MaTG INT IDENTITY(1,1),
	TenTG NVARCHAR(50) NOT NULL,
	DiaChi NVARCHAR(100),
	TieuSu nVarchar(Max),
	DienThoai VARCHAR(50),
	CONSTRAINT PK_TacGia PRIMARY KEY(MaTG)
)
go

CREATE TABLE VietSach
(
	MaTG INT not null,
	MaSach INT not null,
	VaiTro NVARCHAR(50),
	ViTri nVarchar(50),
	CONSTRAINT PK_VietSach PRIMARY KEY(MaTG, MaSach),
	Constraint FK_VietSach_TacGia Foreign Key(MaTG) References TacGia(MaTg),
	Constraint FK_VietSach_Sach Foreign Key(MaSach)References Sach(MaSach)
)
go

CREATE TABLE DonDatHang
(
	MaDonHang INT IDENTITY(1,1),
	DaThanhToan bit,
	TinhTrangGiaoHang bit,
	NgayDat Datetime,
	NgayGiao Datetime,	
	MaKH INT,
	CONSTRAINT FK_DonDatHang_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
	CONSTRAINT PK_DonDatHang PRIMARY KEY (MaDonHang)
)
go

CREATE TABLE ChiTietDonDatHang
(
	MaDonHang INT,
	MaSach INT,
	SoLuong Int Check(SoLuong>0),
	DonGia Decimal(18,0) CHECK (DonGia>=0),
	CONSTRAINT PK_ChiTietDonDatHang PRIMARY KEY(MaDonHang, MaSach),
	CONSTRAINT FK_ChiTietDonDatHang_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonDatHang(MaDonHang),
	CONSTRAINT FK_ChiTietDonDatHang_Sach FOREIGN KEY (MaSach) REFERENCES Sach(MaSach)
)
go

Create Table Admin
(
	TaiKhoan VarChar(50) NOT NULL,
	MatKhau VarChar(50) NOT NULL,
	HoTen NVarChar(50) NOT NULL
)
go

insert into TacGia values (
	N'Tác giả 1',
	N'Địa chỉ tác giả 1',
	N'Tiểu sử tác giả 1',
	'0909696969'
),(
	N'Tác giả 2',
	N'Địa chỉ tác giả 2',
	N'Tiểu sử tác giả 2',
	'0909696969'
),(
	N'Tác giả 3',
	N'Địa chỉ tác giả 3',
	N'Tiểu sử tác giả 3',
	'0909696969'
),(
	N'Tác giả 4',
	N'Địa chỉ tác giả 4',
	N'Tiểu sử tác giả 4',
	'0909696969'
),(
	N'Tác giả 5',
	N'Địa chỉ tác giả 5',
	N'Tiểu sử tác giả 5',
	'0909696969'
)
go

set dateformat dmy;
go

insert into KhachHang values (
	N'Khách Hàng 1',
	'khachhang1',
	'khachhang1',
	'khachhang1@gmail.com',
	N'Địa chỉ khách hàng 1',
	'0900112233',
	'01/01/2001'
), (
	N'Khách Hàng 2',
	'khachhang2',
	'khachhang2',
	'khachhang2@gmail.com',
	N'Địa chỉ khách hàng 2',
	'0900112233',
	'01/01/2001'
), (
	N'Khách Hàng 3',
	'khachhang3',
	'khachhang3',
	'khachhang3@gmail.com',
	N'Địa chỉ khách hàng 3',
	'0900112233',
	'01/01/2001'
), (
	N'Khách Hàng 4',
	'khachhang4',
	'khachhang4',
	'khachhang4@gmail.com',
	N'Địa chỉ khách hàng 4',
	'0900112233',
	'01/01/2001'
), (
	N'Khách Hàng 5',
	'khachhang5',
	'khachhang5',
	'khachhang5@gmail.com',
	N'Địa chỉ khách hàng 5',
	'0900112233',
	'01/01/2001'
)
go

insert into NhaXuatBan values (
	N'Nhà xuất bản 1',
	N'Địa chỉ nhà xuất bản 1',
	'0989123456'
), (
	N'Nhà xuất bản 2',
	N'Địa chỉ nhà xuất bản 2',
	'0989123456'
), (
	N'Nhà xuất bản 3',
	N'Địa chỉ nhà xuất bản 3',
	'0989123456'
), (
	N'Nhà xuất bản 4',
	N'Địa chỉ nhà xuất bản 4',
	'0989123456'
), (
	N'Nhà xuất bản 5',
	N'Địa chỉ nhà xuất bản 5',
	'0989123456'
)
go

insert into ChuDe values (
	N'Chủ đề 1'
), (
	N'Chủ đề 2'
), (
	N'Chủ đề 3'
), (
	N'Chủ đề 4'
), (
	N'Chủ đề 5'
)
go

insert into Sach values (
	N'Tên sách 1',
	99000,
	'',
	'',
	'1/4/2021',
	10,
	1,
	1
), (
	N'Tên sách 2',
	99000,
	'',
	'',
	'1/4/2021',
	10,
	2,
	2
), (
	N'Tên sách 3',
	99000,
	'',
	'',
	'1/4/2021',
	10,
	3,
	3
), (
	N'Tên sách 4',
	99000,
	'',
	'',
	'1/4/2021',
	10,
	4,
	4
), (
	N'Tên sách 5',
	99000,
	'',
	'',
	'1/4/2021',
	10,
	5,
	5
)
go

insert into VietSach values (
	1, 1, N'Tác giả', ''	
), (
	2, 2, N'Tác giả', ''	
), (
	3, 3, N'Tác giả', ''	
), (
	4, 4, N'Tác giả', ''	
), (
	5, 5, N'Tác giả', ''	
)

go

insert into DonDatHang values (
	0,
	0,
	'1/4/2021',
	'2/4/2021',
	1
), (
	0,
	0,
	'1/4/2021',
	'3/4/2021',
	2
), (
	0,
	0,
	'1/4/2021',
	'4/4/2021',
	3
), (
	0,
	0,
	'1/4/2021',
	'5/4/2021',
	4
), (
	0,
	0,
	'1/4/2021',
	'6/4/2021',
	5
)
go

insert into ChiTietDonDatHang values (
	1,
	1,
	1,
	99000
), (
	2,
	2,
	2,
	198000
), (
	3,
	3,
	1,
	99000
), (
	4,
	4,
	1,
	99000
), (
	5,
	5,
	1,
	99000
)
go


insert into Admin values(
	'admin',
	'123456',
	N'Quản trị viên'
),(
	'manager',
	'manager',
	N'Quản lý'
)
go

/* -------------------------------

select * from TacGia

select * from VietSach

select * from Sach

select * from ChuDe

select * from NhaXuatBan

select * from ChiTietDonDatHang

select * from DonDatHang

select * from KhachHang

select * from Admin

*/
