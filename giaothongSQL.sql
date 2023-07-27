create database giaothong

create table USERS
(
	ID int primary key identity(1,1),
	Role_ID int,
	UserName char(30),
	Password char(20),
	Name nvarchar(30),
	Phone char(15),
	Email char(25),
	Address nvarchar(60),
	BirthDay Date,
)

Create table USERS_ROLE
(
	ID int primary key identity(1,1),
	name nvarchar(30)
)

alter table users add constraint fk_user_role foreign key(Role_ID) references USERS_ROLE(id) 

Create table GIAOVIEN
(
	MaGV char(20) primary key,
	HoDem nvarchar(25),
	TenGV nvarchar(15),
	NgaySinh Date,
	SoCCCD Char(12) null,
	NoiCT nvarchar(30),
	GioiTinh int,
	Phone Char(15),
	TuyenDung nvarchar(10),
	TrinhDo_VH nvarchar(10),
	Nganh_CM nvarchar(20),
	TrinhDo_SP nvarchar(10),
	GV_LT Bit,
	GV_TH Bit,
	SoGCN char(15),
	MaSoGTVT char(5),
	MaCSDT char(5),
	TrangThai Bit,
	AnhCD Text,
	NguoiCapNhat nvarchar(20),
	NgayCapNhat Date,
	NguoiDuyet nvarchar(20),
	NgayDuyet Date
)


ALTER TABLE GIAOVIEN
ALTER COLUMN MaGV char(16);

create table GIAOVIEN_GCN
(
	SoGCN char(15) primary key,
	QDCap char(15),
	DonViCap char(5),
	NgayCap date,
	HangXe char(5),
	MaDonViTapHuan char(5),
	NgayKiemTra date,
	AnhGCN text,
	NguoiCapNhat nvarchar(15),
	NgayCapNhat date
)
alter table GIAOVIEN add constraint fk_giaovien_gcn foreign key(SoGCN) references GIAOVIEN_GCN(SoGCN) on delete cascade

create table XETAPLAI	
(
	SoDangKy char(10) primary key,
	TenChuSoHuu char(30),
	SoGPTL char(15),
	NhanHieu char(15),
	KieuLoai char(20),
	HangXe char(5),
	MauXe nvarchar(20),
	SoDongCo char(20),
	SoKhung char(20),
	NamSX char(10),
	LoaiXe nvarchar(15),
	XeSatHach bit,
	TrangThaiHD bit,
	NgayCapNhat date,
	NguoiCapNhat nvarchar(15)
)

create table XETAPLAI_GP
(
	SoGPXTL char(15) primary key,
	SoDangKy char(10),
	MaDonViCap char(5),
	MaCSDT char(5),
	XeHopDong bit,
	NgayCap date,
	NgayHH date,
	AnhGP text,
	AnhDKiem text,
	AnhDky text,
	NgayCapNhat date,
	NguoiCapNhat nvarchar(15),
	DuongTapLai char(200)
)
alter table XETAPLAI add constraint fk_xetl_gp foreign key(SoGPTL) references XETAPLAI_GP(SoGPXTL) on delete cascade
