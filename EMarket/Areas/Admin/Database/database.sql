create database EMarket;
go
use EMarket;
go
drop database EMarket;

create table [Loai](
		[LoaiID] int not null identity(1,1),
		[TenLoai] nvarchar(100) not null,
		[MoTa] nvarchar(max) ,
		constraint [PK_Loai] primary key([LoaiID])
)
create table [NhaCungCap](
	[NhaCungCapID] int not null identity(1,1),
	[TenNhaCungCap] nvarchar(200) not null,
	[MoTa] nvarchar(200),
	constraint [PK_NhaCungCap] primary key([NhaCungCapID])
)
create table [HangHoa](
		[HangHoaID] int not null identity(1,1),
		[TenHangHoa] nvarchar(200) not null,
		[NhaCungCapID] int not null,
		[LoaiID] int not null,
		[Gia] float not null,
		[Hinh] varchar(255),
		[MoTa] nvarchar(max),
		constraint [PK_HangHoa] primary key([HangHoaID]),
		constraint [FK_HangHoa_NhaCungCap] foreign key ([NhaCungCapID]) references [NhaCungCap] ([NhaCungCapID]),
		constraint [FK_HangHoa_LoaiID] foreign key ([LoaiID]) references [Loai] ([LoaiID])
)
create table [ThongTinTaiKhoan] (
	[ThongTinTaiKhoanID] int not null identity(1,1),
	[HoVaTen] nvarchar(200) not null,
	[NgaySinh] datetime,
	[SDT] varchar (20),
	[DiaChi] nvarchar(200),
	constraint [PK_ThongTinTaiKhoan] primary key ([ThongTinTaiKhoanID])
)
create table [TaiKhoan](
	[TaiKhoanID] int not null identity(1,1),
	[UserName] varchar(100) not null,
	[Password] varchar(100) not null,
	[NgayDK] datetime not null,
	[LoaiTaiKhoan] bit not null, -- 1 là tài khoản khách hàng
	[ThongTinTaiKhoanID] int not null,
	constraint [PK_TaiKhoan] primary key([TaiKhoanID]),
	constraint [FK_TaiKhoan_ThongTinTaiKhoan] foreign key ([ThongTinTaiKhoanID]) references [ThongTinTaiKhoan] ([ThongTinTaiKhoanID])

)
create table [TopSelling] (
	[TopSellingID] int not null identity(1,1),
	[HangHoaID] int not null,
	[SoLan] int null default(0),
	[DanhGia] int,
	constraint [PK_TopSelling] primary key ([TopSellingID]),
	constraint [FK_TopSelling_HangHoa] foreign key ([HangHoaID]) references [HangHoa] ([HangHoaID])
)


insert into [NhaCungCap] ([TenNhaCungCap],[MoTa])
values ('NOKIA','Nokia is a global leader in creating the technologies at the heart of our connected world'),
		('SAMSUNG','Samsung is a South Korean electronics company which sells televisions, household appliances and perhaps most notably, mobile devices.'),
		('APPLE','Apple Inc. is an American multinational technology company headquartered in Cupertino, California, that designs, develops, and sells consumer electronics, ...'),
		('SONY','Sony Corporation is a Japanese multinational conglomerate corporation headquartered in Konan, Minato, Tokyo.'),
		('MICROSOFT','Microsoft Corporation (MS) is an American multinational technology company with headquarters in Redmond, Washington.')

insert into [Loai] ([TenLoai],[MoTa])
values (N'Điện Thoại',N'Điện thoại di động, còn gọi là điện thoại cầm tay, là loại điện thoại kết nối dựa trên sóng điện từ vào mạng viễn thông.'),
		(N'Laptop','A laptop is a computer designed for portability. Laptops are usually less than 3 inches thick, weigh less than 5 pounds and can be powered by a battery.'),
		(N'Máy Ảnh',N'Máy ảnh hay máy chụp hình là một dụng cụ dùng để thu ảnh thành một ảnh tĩnh hay thành một loạt các ảnh chuyển động (gọi là phim hay video). '),
		(N'Tablet','A tablet is a wireless touch screen personal computer (PC) that is smaller than a notebook but larger than a smartphone.'),
		(N'Phụ Kiện',N'Phụ Kiện máy ảnh, Máy tính, Điện Thoại')

select * from NhaCungCap
select * from Loai
select * from HangHoa


insert into [HangHoa] (TenHangHoa,NhaCungCapID,LoaiID,Gia,Hinh,MoTa)
values (N'IPhone XS',3,1,999.9,'iphonexs.jpg','Super Retina in two sizes — including the largest display ever on an iPhone. Even faster Face ID. The smartest, most powerful chip in a smartphone.'),
		(N'MacBook-Air',3,2,1600,'macbookair.jpg','The most loved Mac is about to make you fall in love all over again. Available in silver, space gray, and gold, the new thinner and lighter MacBook Air features a brilliant Retina display, Touch ID, the latest-generation keyboard, and a Force Touch trackpad.'),
		(N'IPad-Pro',3,4,475,'ipadpro.jpg','The new all-screen design means iPad Pro is a magical piece of glass that does everything you need, any way you hold it.'),
		(N'SamSung Galaxy S9+',2,1,899.9,'samsunggalaxys9+.jpg','Our category-defining Dual Aperture lens adapts like the human eye. It†s able to automatically switch between various lighting conditions with ease—making your photos look great whether it†s bright or dark, day or night.'),
		(N'Galaxy Note9',2,1,799.9,'galaxynote9.jpg','Galaxy Note has always put powerful technology in the hands of those who demand more. Now, the all new Galaxy Note9 surpasses even these high expectations, focusing on what matters most in today’s always-on, mobile world.'),
		(N'Galaxy Tab S',2,4,499.9,'galaxytabs.jpg','inge watch your favourite movies and TV shows. The slim bezels and 10.5" immersive screen show every moment in crisp detail and rich colour.'),
		(N'Samsung NX1',2,3,1499.9,'samsungnx1.jpg','Samsung’s NX1 camera is ready to capture life as it happens. Thanks to 205 phase-detection points and 209 contrast-detection points, the NX AF System III covers an incredible 90% of the sensor area. This super-wide AF area is superior to those in digital SLRs.'),
		(N'Samsung Sound+ SWA-9000S',2,5,299.6,'samsungsound+swa-9000s','The Wireless Surround Kit* lets you expand your surround sound system easily without the mess of wires**. Together with your Soundbar, you can create a multi-channel system for a true surround sound experience.'),
		(N'Xperia XZ3',4,1,399.9,'xperiaxz3.jpg','The beauty of Sony OLED, now on Xperia. Escape from the everyday into your world of entertainment with the Xperia XZ3.'),
		(N'A9-CMOS',4,3,3499.9,'a9cmos.jpg','A game-changing image sensor from Sony makes conventional camera mechanisms redundant, achieving speed and performance that are beyond the capabilities of mechanical devices.'),
		(N'Z1R Premium Headphones',4,5,149.9,'z1rpremiumheadphones.jpg','Capture pure sound with the 2.76" HD Driver The newly developed diaphragm with magnesium dome and liquid crystal polymer edge enables up to 120-kHz playback in High-Resolution Audio.'),
		(N'Nokia 7.1',1,1,399.9,'nokia7.jpg','Nokia 7.1 is all about capturing photos that look so good, you just have to share them. The dual rear cameras with ZEISS optics deliver stunning shots, while the AI technology lets you add an artistic touch to every photo with 3D personas, masks and filters.'),
		(N'Lumia 950 XL',5,1,499.9,'lumia950xl.jpg','With a stunning 5.7” Quad HD display and a powerful octa-core processor, it’s the Lumia you’ve been waiting for. Get the phone that works like your PC and push the limits of what’s possible.'),
		(N'Lenovo Ideapad 720S',5,2,799.9,'lenovoideapad720s.jpg','Lightweight, powerful, and stylish, the Lenovo Ideapad 720S Touch easily keeps pace with your mobile lifestyle. Its a breeze to carry at just 4 pounds and comes loaded with a 7th Gen Intel Core i7 processor, NVIDIA graphics, and a fingerprint reader for extra security.')

select * from HangHoa order by NhaCungCapID ASC



insert into Topselling(HangHoaID,SoLan,DanhGia)
values (1,10,10),
		(4,8,7),
		(2,10,9),
		(14,7,7),
		(3,10,10),
		(5,8,8)

select *
from HangHoa
where HangHoaID in (select HangHoaID from TopSelling)
