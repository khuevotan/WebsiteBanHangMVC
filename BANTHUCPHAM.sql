CREATE DATABASE QLTP
GO
USE QLTP
GO
CREATE TABLE Nhom
(
	MaNhom varchar(15) PRIMARY KEY,
	TenNhom nvarchar(20) NOT NULL
)
GO
CREATE TABLE NhanVien
(
	MaNV varchar(15) PRIMARY KEY,
	TaiKhoan varchar(30) NOT NULL,
	MatKhau varchar(20) NOT NULL,
	HoNV nvarchar(30) NOT NULL,
	TenNV nvarchar(30) NOT NULL,
	SoDT varchar(15) NOT NULL,
	DiaChi nvarchar(50) NOT NULL,
	Email varchar(30) NOT NULL,
	Luong float,
	NgaySinh date,
	GioiTinh bit DEFAULT(1),
	HinhDD varchar(50),
	MaNhom varchar(15) FOREIGN KEY REFERENCES Nhom(MaNhom)
)
GO
CREATE TABLE BaiViet
(
	MaBV varchar(15) PRIMARY KEY,
	TenBV nvarchar(200) NOT NULL,
	NoiDung nvarchar(3000) NOT NULL,
	HinhDD varchar(50),
	NgayDang date NOT NULL,
	MaNV varchar(15) FOREIGN KEY REFERENCES NhanVien(MaNV)
)
GO
CREATE TABLE NhaCungCap
(
	MaNCC varchar(15) PRIMARY KEY,
	TenNCC nvarchar(50) NOT NULL,
	DiaChi nvarchar(100) NOT NULL,
	SoDT varchar(15) NOT NULL,
	HinhDD varchar(50) NOT NULL
)
GO
CREATE TABLE PhieuNhap
(
	MaPN varchar(15) PRIMARY KEY,
	NgayNhap date,
	ThanhToan nvarchar(20) NOT NULL,
	MaNV  varchar(15) FOREIGN KEY REFERENCES NhanVien(MaNV),
	MaNCC varchar(15) FOREIGN KEY REFERENCES NhaCungCap(MaNCC)
)

GO
CREATE TABLE DanhMuc
(
	MaDM varchar(15) PRIMARY KEY,
	TenDM nvarchar(25) NOT NULL,
	HinhDD varchar(50) NOT NULL,
	MoTaCT nvarchar(100) NOT NULL,
)
GO

GO
CREATE TABLE SanPham
(
	MaSP varchar(15) PRIMARY KEY,
	TenSP nvarchar(200) NOT NULL,
	HinhDD nvarchar(50) NOT NULL,
	SoLuong int NOT NULL,
	MoTaCT nvarchar(1000) NOT NULL,
	GiaBan float,
	PhoBien bit DEFAULT(1),
	NgayDang date,
	MaNCC varchar(15) FOREIGN KEY REFERENCES NhaCungCap(MaNCC),
	MaDM varchar(15) FOREIGN KEY REFERENCES DanhMuc(MaDM)
)
GO
CREATE TABLE TrangThai
(
	MaTT varchar(15) PRIMARY KEY,
	TenTT nvarchar(20) NOT NULL
)
GO
CREATE TABLE CTPhieuNhap
(
	MaPN varchar(15) FOREIGN KEY REFERENCES PhieuNhap(MaPN),
	MaSP varchar(15) FOREIGN KEY REFERENCES SanPham(MaSP),
	SoLuong int NOT NULL,
	GiaTien float not null,
	CONSTRAINT pk_CTPhieuNhap PRIMARY KEY (MaPN, MaSP)
)

CREATE TABLE KhachHang
(
	MaKH varchar(15) PRIMARY KEY,
	TaiKhoan varchar(30) NOT NULL,
	MatKhau varchar(20) NOT NULL,
	HoKH nvarchar(30) NOT NULL,
	TenKH nvarchar(30) NOT NULL,
	NgaySinh date,
	SoDT varchar(15) NOT NULL,
	DiaChi nvarchar(50) NOT NULL,
	Email varchar(30) NOT NULL,
	GioiTinh bit DEFAULT(1),
	HinhDD varchar(50) NOT NULL,
)

GO
CREATE TABLE HoaDon
(
	MaHD varchar(30) PRIMARY KEY,
	NgayDat date not null,
	NgayGiao date not null,
	SoDT varchar(15) NOT NULL,
	DiaChi nvarchar(50) NOT NULL,
	GhiChu nvarchar(50) NOT NULL,
	ThanhToan float not null,
	MaTT varchar(15) FOREIGN KEY REFERENCES TrangThai(MaTT),
	MaNV varchar(15) FOREIGN KEY REFERENCES NhanVien(MaNV),
	MaKH varchar(15) FOREIGN KEY REFERENCES KhachHang(MaKH)
)
GO

GO
CREATE TABLE CTHoaDon
(
	GiaTien float not null,
	SoLuong int not null,
	MaHD varchar(30) FOREIGN KEY REFERENCES HoaDon(MaHD),
	MaSP varchar(15) FOREIGN KEY REFERENCES SanPham(MaSP),
	CONSTRAINT pk_CTHoaDon PRIMARY KEY (MaHD, MaSP)
)
GO
CREATE TABLE PhanHoi
(
	MaPH varchar(15) PRIMARY KEY,
	NgayGui date,
	ChuDe nvarchar(30) NOT NULL,
	NoiDung nvarchar(200) NOT NULL,
	MaKH varchar(15) FOREIGN KEY REFERENCES KhachHang(MaKH)
)
GO

INSERT INTO dbo.Nhom (MaNhom, TenNhom)
VALUES ('MN1', N'Quản lý'),
	   ('MN2', N'Nhân viên')	
		

GO
INSERT INTO dbo.NhanVien (MaNV, TaiKhoan, MatKhau, HoNV, TenNV, SoDT, DiaChi,Email, Luong, NgaySinh, GioiTinh, HinhDD, MaNhom)
VALUES ('NV01', N'khuevotan','123', N'Võ', N'Tấn Khuê','0987664220', N'Cam Ranh, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-09-06' AS Date), 1, N'khuevotan.jpg','MN1'),
	   ('NV02', N'admin','123', N'Quản', N'Trị Viên','01627240041', N'Tuy Hòa, Phú Yên','hello@gmail.com', 20000000.0 ,CAST(N'2001-09-06' AS Date), 1, N'employee.jpg','MN2'),
	   ('NV03', N'huynguyenhuu','123', N'Nguyễn', N'Hữu Huy','0987413571', N'Cam Lâm, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-03-04' AS Date), 1, N'employee.jpg','MN1'),
	   ('NV04', N'phucnguyenvan','123', N'Nguyễn', N'Văn Phúc','0983468912', N'Nha Trang, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-11-24' AS Date), 1, N'employee.jpg','MN1'),
	   ('NV05', N'phuongthihuynh','123', N'Huỳnh', N'Thị Phương','0983347511', N'Đông Hòa, Phú Yên','hello@gmail.com',10000000.0 ,CAST(N'2001-02-07' AS Date), 2, N'employee.jpg','MN1'),
	   ('NV06', N'khanhtranvan','123', N'Trần', N'Văn Khánh','0984478901', N'Nha Trang, Khánh Hòa','hello@gmail.com',20000000.0 ,CAST(N'2001-06-24' AS Date), 1, N'employee.jpg','MN2'),
	   ('NV07', N'namnguyenvan','123', N'Nguyễn', N'Văn Nam','0984416745', N'Nha Trang, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-09-22' AS Date), 1, N'employee.jpg','MN1'),
	   ('NV08', N'nhinguyenthiai','123', N'Nguyễn', N'Thị Ái Nhi','0983468912', N'Cam Lâm, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-11-03' AS Date), 2, N'employee.jpg','MN1'),
	   ('NV09', N'huytranvan','123', N'Trần', N'Văn Huy','0983515131', N'Nha Trang, Khánh Hòa','hello@gmail.com',10000000.0 ,CAST(N'2001-10-01' AS Date), 1, N'employee.jpg','MN1'),
	   ('NV10', N'datnguyenthanh','123', N'Nguyễn', N'Thành Đạt','0984141674', N'Nha Trang, Khánh Hòa','hello@gmail.com',20000000 ,CAST(N'2001-11-20' AS Date), 1, N'employee.jpg','MN2')

GO

INSERT INTO dbo.BaiViet (MaBV, MaNV, NgayDang,TenBV, HinhDD, NoiDung)
VALUES	('BV01', 'NV01', CAST(N'2020-11-20' AS Date), N'Hướng dẫn 2 cách làm ổi lắc xí muội chua cay ngon khó cưỡng','oilacximui.jpg',N'Ổi lắc xí muội đang ngày càng phổ biến với nhiều người Việt, đặc biệt là các bạn trẻ. Cùng tìm hiểu 2 cách làm ổi lắc xí muội ngon khó cưỡng ngay tại nhà nhé!'),
		('BV02', 'NV03', CAST(N'2020-10-11' AS Date), N'Cây monstera đột biến là cây gì? Một vài lưu ý khi trồng cây monstera', 'caymonstera.jpg',N'Cây monstera hay trầu bà Nam Mỹ đột biến có nguồn gốc từ Châu Mỹ, có nhiều ở vùng rừng rậm nhất là vùng nhiệt đới phía nam Mexico. Lá của loại cây này có rãnh hình cánh và tròn, nó có ý nghĩa mang lại may mắn cho gia chủ'),
		('BV03', 'NV07', CAST(N'2020-9-20' AS Date), N'Đặc điểm của cây bạch tuyết? Ý nghĩa và cách trồng cây', 'caybachtuyet.jpg',N'Bạch Tuyết Mai hay còn được gọi là cây Bạch Tuyết, Mã Thiên Hương, hoa ngàn sao, bỏng nẻ và có tên khoa học là Serissa foetida, Serissa japonica Thunb. Cây thuộc họ cà phê Rubiaceae, phân bố nhiều nhất tại Nhật Bản và các quốc gia châu Á. Hiện nay, Bạch Tuyết Mai ở nước ta thường được thấy ở Đà Lạt cùng các tỉnh phía Nam khác.'),
		('BV04', 'NV01', CAST(N'2020-10-01' AS Date), N'Cây bàng gai là gì? Đặc điểm, tác dụng và các bài thuốc của cây bàng gai', 'caybanggai.jpg',N'Cây bàng gai hay còn có những cái tên khác như Quang lang, Bàng biển, Badamier, Choambok Barangparrcang Prang, đây là loại cây thân gỗ lớn, mọc thẳng, có chiều cao lên đến 25m, tán lá rộng, cành nằm ngang.')
GO

INSERT INTO dbo.NhaCungCap (MaNCC, TenNCC, DiaChi, SoDT, HinhDD)
VALUES ('NCC01', N'Công ty TNHH SX TM DV Nguyên Khang', N'2.12 Cao ốc KP, 67 Huỳnh Thiện Lộc, P Hòa Thạnh, Q Tân Phú, TPHCM', 0987642245, 'nguyenkhang.jpg'),
	   ('NCC02', N'Công ty TNHH Thực phẩm Hoàng Đông', N'Phòng 201- Nhà C6- Phường Mai Động -Quận Hoàng Mai -Thành phố Hà Nội', 0987642245, 'phuongdong.jpg'),
	   ('NCC03', N'Phân Phối Thực Phẩm Sạch Tươi Sống Quốc Huy', N'Xã Đặng Xá, Huyện Gia Lâm, Hà Nội', 0985167812,'quochuy.jpg'),
	   ('NCC04', N'Doanh Nghiệp Tư Nhân Ngọc Cường', N'Tổ 3 Vọng La, Vọng La, Đông Anh, Hà Nội', 0985551152,'ngoccuong.jpg'),
	   ('NCC05', N'Công Ty TNHH Thực Phẩm Hoàng Đông', N'Hà Nội', 0985161722,'hoangdong.png'),
	   ('NCC06', N'Công Ty Xuất Khẩu Rau Quả Tiền Giang', N'Km1977 Quốc Lộ 1, Long Định, Châu Thành, Tiền Giang', 098789906, 'tiengiang.jpg'),
	   ('NCC07', N'Hoàng Huy', N'Đà Nẵng', 0985161132,'tiengiang.jpg'),
	   ('NCC08', N'An Phúc', N'Khánh Hòa', 098715123,'tiengiang.jpg'),
	   ('NCC09', N'Phố Vọng', N'Hà Nội', 0985168142,'tiengiang.jpg'),
	   ('NCC10', N'Vĩnh Hòa', N'Huế', 098415125,'tiengiang.jpg')
GO
INSERT INTO dbo.PhieuNhap (MaPN, NgayNhap, ThanhToan, MaNV, MaNCC)
VALUES	('PN001', CAST(N'2020-09-11' AS Date), N'Tiền mặt', 'NV01', 'NCC03'),
		('PN002', CAST(N'2020-10-01' AS Date), N'Tiền mặt', 'NV05', 'NCC02'),
		('PN003', CAST(N'2020-03-15' AS Date), N'Tiền mặt', 'NV02', 'NCC09'),
		('PN004', CAST(N'2020-05-19' AS Date), N'Tiền mặt', 'NV08', 'NCC01'),
		('PN005', CAST(N'2020-09-20' AS Date), N'Tiền mặt', 'NV07', 'NCC04'),
		('PN006', CAST(N'2020-04-11' AS Date), N'Tiền mặt', 'NV01', 'NCC07'),
		('PN007', CAST(N'2020-08-10' AS Date), N'Tiền mặt', 'NV10', 'NCC10'),
		('PN008', CAST(N'2020-12-01' AS Date), N'Tiền mặt', 'NV06', 'NCC05'),
		('PN009', CAST(N'2020-04-11' AS Date), N'Tiền mặt', 'NV02', 'NCC03'),
		('PN010', CAST(N'2020-06-21' AS Date), N'Tiền mặt', 'NV04', 'NCC08')

GO
INSERT INTO dbo.DanhMuc (MaDM, TenDM, HinhDD, MoTaCT)
VALUES	('DM01', N'Rau', 'rau.jpg', N'Rau'),
		('DM02', N'Củ', 'cu.jpg',N'Củ'),
		('DM03', N'Trái Cây', 'traicay.jpg', N'Trái cây'),
		('DM04', N'Sữa', 'sua.jpg', N'Sữa'),
		('DM05', N'Trứng', 'trung.jpg', N'Trứng'),
		('DM06', N'Gạo', 'gao.jpg', N'Gạo'),
		('DM07', N'Bột', 'bot.jpg', N'Bột'),
		('DM08', N'Đồ Khô', 'dokho.jpg', N'Đồ Khô')
GO
INSERT INTO dbo.SanPham (MaSP, TenSP, HinhDD, SoLuong, MoTaCT, GiaBan,PhoBien, NgayDang, MaNCC, MaDM)
VALUES	('SP01', N'Nấm kim châm Hàn Quốc', 'namkimcham.jpg', 30, N'Nấm kim châm Hàn Quốc được nuôi trồng và đóng gói theo những tiêu chuẩn nghiêm ngặt, bảo đảm các tiêu chuẩn xanh - sạch, chất lượng và an toàn với người dùng. Sợi nấm dai, giòn và ngọt, khi nấu chín rất thơm nên thường được lăn bột chiên giòn, nấu súp hoặc để nướng ăn kèm.', 10000.0,0,CAST(N'2020-09-06' AS Date), 'NCC01','DM01'),
		('SP02', N'Cải ngọt', 'caingot.jpg', 30, N'Rau an toàn 4KFarm với tiêu chí 4 KHÔNG, luôn ưu tiên bảo vệ sức khỏe người tiêu dùng. Hàm lượng chất xơ cao, chứa nhiều chất dinh dưỡng,Cải Ngọt 4KFarm là một trong những loại rau được ưa thích để chế biến những món canh rau ngon bổ dưỡng phù hợp với mọi lứa tuổi.', 15000.0,0, CAST(N'2020-07-01' AS Date), 'NCC02','DM01'),
		('SP03', N'Rau muống', 'raumuong.jpg', 30, N'Rau an toàn 4KFarm với tiêu chí 4 KHÔNG, luôn ưu tiên bảo vệ sức khỏe người tiêu dùng. Với vị ngọt, tính mát, hàm lượng dinh dưỡng cao đặc biệt là sắt, vitamin A,c, Rau muống 4KFarm luôn là loại rau được ưa chuộng chọn lựa cho bữa cơm ngon mỗi ngày.', 9000.0 ,1, CAST(N'2020-10-02' AS Date), 'NCC03', 'DM01'),
		('SP04', N'Cà rốt ', 'carot.jpg', 30, N'Cà rốt Đà Lạt là một loại củ rất quen thuộc trong các món ăn của người Việt. Loại củ này có hàm lượng chất dinh dưỡng và vitamin A cao, được xem là nguyên liệu cần thiết cho các món ăn dặm của trẻ nhỏ, giúp trẻ sáng mắt và cung cấp nguồn chất xơ dồi dào.', 5000.0 ,1, CAST(N'2020-07-11' AS Date), 'NCC05', 'DM02'),
		('SP05', N'Bắp mỹ', 'bap.jpg', 30, N'Bắp Mỹ là một loại thực phẩm được trồng rất nhiều ở khắp nơi trên thế giới. Đây là một loại quả vừa ngon, lại có rất nhiều chất khoáng chất và vitamin. Bắp có thể chế biến thành nhiều món ăn ngon như: cơm bắp, chè bắp, sữa bắp,... bất kỳ món gì cũng tạo nên hương vị tuyệt hảo.', 15000.0,1, CAST(N'2020-11-01' AS Date), 'NCC04', 'DM02'),
		('SP06', N'Bí đỏ', 'bido.jpg', 30,N'Còn gọi là bí đỏ hạt đậu, đây là giống bí có ruột rất đặc, ít hả ăn dẻo và ngọt. Bí hồ lô chứa nhiều chất dinh dưỡng và mang lại nhiều lợi ích cho sức khoẻ. Bí hồ lô có thể chế biến thành nhiều món ăn ngon như: sữa bí, canh bí, súp bí,... món nào cũng đều thơm ngon.', 15000.0,1, CAST(N'2020-07-01' AS Date), 'NCC02', 'DM02'),
		('SP07', N'Táo Ninh Thuận', 'tao.jpg', 30,N'Táo Ninh Thuận có hình dáng khá tròn, nhỏ, giòn và có vị ngọt thanh. Táo chỉ cần rửa sạch, chấm với muối ớt hoặc muối tôm ăn bắt vị vô cùng. Bên cạnh đó táo Ninh Thuận còn rất nhiều lợi ích cho sức khỏe như đẹp da, tốt cho trí não, hệ tiêu hóa và hệ miễn dịch nên rất được ưa chuộng.', 3000.0,1, CAST(N'2020-04-22' AS Date), 'NCC06', 'DM03'),
		('SP08', N'Xoài cát Hoà Lộc', 'xoai.jpg', 30,N'Loại trái cây giàu chất xơ, vitamin, khoáng chất thiết yếu giúp cung cấp chất dinh dưỡng cho cơ thể con người và mang lại nhiều lợi ích tuyệt vời cho hệ tiêu hóa, tim mạch, giúp mắt sáng, làm đẹp da. Xoài cát Hòa Lộc có vị ngọt thanh dễ chịu, thịt dày, ít xơ và có độ dẻo lý tưởng', 15000.0,1, CAST(N'2020-07-01' AS Date), 'NCC08', 'DM03'),
		('SP09', N'Thùng 48 hộp sữa tươi có đường Vinamilk', 'suavn.jpg', 30,N'Được chế biến từ nguồn sữa tươi 100% chứa nhiều dưỡng chất như vitamin A, D3, canxi,... tốt cho xương và hệ miễn dịch. Sữa tươi Vinamilk là thương hiệu được tin dùng hàng đầu với chất lượng tuyệt vời. Thùng 48 hộp sữa tươi có đường Vinamilk 100% Sữa Tươi 180ml thơm ngon dễ uống', 9000.0,1, CAST(N'2020-05-10' AS Date), 'NCC10', 'DM04'),
		('SP10', N'Thùng 48 hộp sữa tiệt trùng ít đường Dutch Lady Cao Khoẻ 170ml', 'suadu.jpg', 30,N'Sữa Dutch Lady gấp đôi vitamin D và canxi giúp bé cao khoẻ hơn mỗi ngày. Sữa thơm ngon, dễ uống. Thùng 48 hộp sữa tiệt trùng ít đường Dutch Lady Cao Khoẻ 170ml có đường giúp bạn tiết kiệm được nhiều chi phí, sử dụng lâu dài', 6000.0,1, CAST(N'2020-09-01' AS Date), 'NCC09', 'DM04'),
		('SP11', N'Thùng 48 hộp sữa tươi tiệt trùng có đường Dutch Lady 110ml', 'suadu2.jpg', 30,N'Sữa Dutch Lady với hương vị thơm ngon, có thêm đường dễ uống, đóng gói hộp nhỏ tiện dụng, dễ dàng mang đi xa. Đảm bảo là sản phẩm sữa yêu thích của các bé. Thùng 48 hộp sữa tươi tiệt trùng có đường Dutch Lady 110ml tiết kiệm, tiện sử dụng', 10000.0,1, CAST(N'2020-09-06' AS Date), 'NCC01','DM04'),
		('SP12', N'Hộp 10 trứng vịt xanh 4KFarm', 'trungvit.jpg', 30,N'Hộp 10 trứng vịt của 4KFarm được đóng gói và bảo quản theo những tiêu chuẩn nghiêm ngặt về vệ sinh và an toàn thực phẩm, đảm bảo về chất lượng của thực phẩm. Trứng vịt của 4K Farm to tròn, đều. Đây là sản phẩm thích hợp để nấu các món như trứng chiên thịt, cháo trứng vịt,...', 15000.0,1, CAST(N'2020-07-01' AS Date), 'NCC02','DM05'),
		('SP13', N'Hộp 10 trứng gà tươi Happy Egg', 'trungga.jpg', 30,N'Hộp 10 trứng gà tươi của Happy Egg được đóng gói và bảo quản theo những tiêu chuẩn nghiêm ngặt về vệ sinh và an toàn thực phẩm, đảm bảo về chất lượng của thực phẩm. Trứng gà to tròn, đều. Trứng gà thì bạn có thể luộc chín chế biến thành một số món ăn khác như: thịt kho trứng, cơm chiên trứng,...', 9000.0,1, CAST(N'2020-10-02' AS Date), 'NCC03', 'DM05'),
		('SP14', N'Hộp 30 trứng cút tươi V.Farm', 'trungcut.jpg', 30,N'Hộp 30 trứng cút tươi của V.Farm được đóng gói và bảo quản theo những tiêu chuẩn nghiêm ngặt về vệ sinh và an toàn thực phẩm, đảm bảo về chất lượng của thực phẩm. Trứng cút tròn, đều.Trứng cút thì bạn có thể luộc chín chế biến thành một số món ăn như: trứng cút hun khói, xíu mại trứng cút,..', 5000.0,1, CAST(N'2020-07-11' AS Date), 'NCC05', 'DM05'),
		('SP15', N'Gạo Ông Táo đặc sản Sóc Trăng Delifarm ST25 túi 5kg', 'gaotao.jpg', 30,N'Gạo ông táo là loại gạo cao cấp của thương hiệu Delifarm thơm ngon, giàu chất dinh dưỡng. Gạo ông táo Delifarm đặc sản sóc trăng ST25 túi 5kg là một trong những loại gạo xuất khẩu được ưa chuộng trên Thế giới với hương thơm đặc trưng, độ mềm dẻo vừa phải, đặc biệt cơm để nguội vẫn ngon', 15000.0,1, CAST(N'2020-11-01' AS Date), 'NCC04', 'DM06'),
		('SP16', N'Gạo hương lài Thiên Kim túi 5kg', 'gaohuonglai.jpg', 30,N'Cơm dẻo mềm, thơm lài, ngọt nhẹ. Gạo Thiên Kim Hương Lài được sản xuất theo quy trình khoa học, đảm bảo mang lại những hạt gạo ngon, tươi mới, thơm lành, chất lượng nhất mà vẫn an toàn cho sức khoẻ của người tiêu dùng.', 15000.0,1, CAST(N'2020-07-01' AS Date), 'NCC02', 'DM06'),
		('SP17', N'Gạo Tám thơm Meizan túi 5kg', 'gaotamthom.jpg', 30,N'Gạo nấu cho ra cơm mềm, dẻo nhiều, vị ngọt nhiều , thơm, ít nở. Gạo Tám thơm Meizan túi 5kg của Meizan giúp bữa cơm thêm ngon miệng, cung cấp nhiều chất dinh dưỡng, đảm bảo an toàn, chất lượng và hương vị đến tay người tiêu dùng.', 3000.0,1, CAST(N'2020-04-22' AS Date), 'NCC06', 'DM06'),
		('SP18', N'Bột mì đa dụng Meizan cao cấp gói 500g', 'botmi.jpg', 30,N'Dùng để làm bánh mì, bánh bông lan, bánh ngọt, bánh rán, bánh pizza, mì sợi, bánh bao hoặc dùng làm các món chiên giòn, tẩm bột. Bột mì đa dụng Meizan gói 500g tiện lợi, phù hợp cho gia đình bạn. Bột mì Meizan được nhiều người chọn lựa và tin dùng', 15000.0,1, CAST(N'2020-07-01' AS Date), 'NCC08', 'DM07'),
		('SP19', N'Bột rau câu pha sẵn Dragon hương dừa gói 10g', 'botraucau.jpg', 30,N'Bột rau câu giúp tạo ra loại rau câu mềm, dẻo và có hương dừa thơm. Bột rau câu hương dừa Dragon gói 10g được chiết xuất từ rong biển. Bột rau câu Dragon tiện lợi, chỉ cần nấu với nước và kết hợp với trái cây tươi thơm ngon là cho ra rau câu giải nhiệt hấp dẫn mua hè.', 9000.0,1, CAST(N'2020-05-10' AS Date), 'NCC10', 'DM07'),
		('SP20', N'Rong biển nấu canh Tohogenkai gói 50g', 'rongbien.jpg', 30,N'Rong biển nấu canh giúp bạn chế biến những món canh rong biển dinh dưỡng, mát lành. Rong biển nấu canh Tohogenkai gói 50g tiện lợi, chỉ cần ngâm rong biển trong nước và rửa sạch là có thể chế biến. Rong biển Tohogenkai chất lượng, vệ sinh, không lẫn tạp chất, yên tâm cho mọi người sử dụng', 6000.0,1, CAST(N'2020-09-01' AS Date), 'NCC09', 'DM08')
GO

GO
INSERT INTO dbo.TrangThai (MaTT, TenTT)
VALUES ('TT1', N'Chưa duyệt'),
	   ('TT2', N'Đã duyệt'),
	   ('TT3', N'Đang vận chuyển'),
	   ('TT4', N'Giao hàng thành công'),
	   ('TT5',  N'Hủy')
Go


INSERT INTO dbo.CTPhieuNhap (MaPN, MaSP, SoLuong, GiaTien)
VALUES	('PN001', 'SP02', 10, 15000),
		('PN002', 'SP06', 15, 15000),
		('PN003', 'SP01', 7, 10000),
		('PN004', 'SP07', 10, 3000),
		('PN005', 'SP03', 10, 9000),
		('PN006', 'SP09', 5, 9000),
		('PN007', 'SP04', 2, 5000),
		('PN008', 'SP05', 10, 15000),
		('PN009', 'SP10', 4, 6000),
		('PN010', 'SP01', 10, 10000)

GO
INSERT INTO dbo.KhachHang (MaKH, TaiKhoan, MatKhau, HoKH, TenKH, NgaySinh, SoDT, DiaChi, Email, GioiTinh, HinhDD)
VALUES	('KH001', N'huynguyen','123', N'Nguyễn', N'Huy', CAST(N'2001-03-16' AS Date), 0985101312, N'Cam Ranh, Khánh Hòa', N'nguyenhuy@gmail.com', 1, N'nguyenhuy.jpg'),
		('KH002', N'nhinguyen','123', N'Nguyễn', N'Nhi', CAST(N'2002-04-26' AS Date), 0981231232, N'Cam Ranh, Khánh Hòa', N'nguyennhi@gmail.com', 2, N'employee.jpg'),
		('KH003', N'phucnguyen','123', N'Nguyễn', N'Phúc', CAST(N'2001-06-01' AS Date), 0985323422, N'Cam Lâm, Khánh Hòa', N'nguyenphuc@gmail.com', 1, N'employee.jpg'),
		('KH004', N'minhtran','123', N'Trần', N'Minh', CAST(N'2002-03-17' AS Date), 0985101312, N'Nha Trang, Khánh Hòa', N'tranminh@gmail.com', 1, N'employee.jpg'),
		('KH005', N'hoapham','123', N'Phạm', N'Hoa', CAST(N'2001-11-21' AS Date), 0983231112, N'Cam Lâm, Khánh Hòa', N'hoapham@gmail.com', 2, N'employee.jpg'),
		('KH006', N'namvannguyen','123', N'Nguyễn', N'Văn Nam', CAST(N'2001-11-16' AS Date), 0982325222, N'Cam Ranh, Khánh Hòa', N'nguyenhuy@gmail.com', 1, N'employee.jpg'),
		('KH007', N'hongphamthi','123', N'Phạm', N'Thị Hồng', CAST(N'2001-10-26' AS Date), 0985312312, N'Nha Trang, Khánh Hòa', N'phamthihong@gmail.com', 2, N'employee.jpg'),
		('KH008', N'phinguyen','123', N'Nguyễn', N'Phi', CAST(N'2001-02-11' AS Date), 0985424212, N'Cam Ranh, Khánh Hòa', N'nguyenphi@gmail.com', 1, N'employee.jpg'),
		('KH009', N'hoaitran','123', N'Trần', N'Hoài', CAST(N'2001-03-20' AS Date), 0985231312, N'Nha Trang, Khánh Hòa', N'nguyenhoai@gmail.com', 1, N'employee.jpg'),
		('KH010', N'thupham','123', N'Phạm', N'Thu', CAST(N'2001-11-16' AS Date), 0985441312, N'Cam Lâm, Khánh Hòa', N'phamthu@gmail.com', 2, N'employee.jpg')
GO

INSERT INTO dbo.HoaDon (MaHD, NgayDat, NgayGiao, SoDT, DiaChi, GhiChu, MaTT, MaNV, MaKH, ThanhToan)
VALUES	('HD001', CAST(N'2020-09-03' AS Date), CAST(N'2020-09-04' AS Date), 0984614815, N'Vĩnh Điềm Trung', N'aaa', 'TT1', 'NV01','KH001', 10.0),
		('HD002', CAST(N'2020-10-02' AS Date), CAST(N'2020-10-03' AS Date), 0985161712, N'Mã Vòng', N'aaa', 'TT2', 'NV03','KH002', 10.0),
		('HD003', CAST(N'2020-04-10' AS Date), CAST(N'2020-04-11' AS Date), 0984614235, N'Vĩnh Điềm Trung', N'aaa', 'TT3', 'NV01','KH003', 10.0),
		('HD004', CAST(N'2020-09-11' AS Date), CAST(N'2020-09-12' AS Date), 0984641215, N'Vĩnh Hải', N'aaa', 'TT4', 'NV02', 'KH004', 10.0),
		('HD005', CAST(N'2020-04-06' AS Date), CAST(N'2020-04-07' AS Date), 0984611256, N'Vĩnh Phước', N'aaa', 'TT5', 'NV08', 'KH005', 10.0),
		('HD006', CAST(N'2020-11-20' AS Date), CAST(N'2020-11-21' AS Date), 0982324812, N'Xóm Cồn', N'aaa', 'TT2', 'NV05', 'KH006', 10.0),
		('HD007', CAST(N'2020-10-11' AS Date), CAST(N'2020-10-12' AS Date), 0984123815, N'Vĩnh Hải', N'aaa', 'TT2', 'NV10', 'KH007', 10.0),
		('HD008', CAST(N'2020-02-01' AS Date), CAST(N'2020-02-02' AS Date), 0985125115, N'Vĩnh Phước', N'aaa', 'TT2', 'NV04', 'KH008', 10.0),
		('HD009', CAST(N'2020-01-03' AS Date), CAST(N'2020-01-04' AS Date), 0984612314, N'Mường Thanh', N'aaa', 'TT2', 'NV09', 'KH009', 10.0),
		('HD010', CAST(N'2020-11-09' AS Date), CAST(N'2020-11-10' AS Date), 098111155, N'Vĩnh Điềm Trung', N'aaa', 'TT2', 'NV03', 'KH001', 10.0)
GO

GO
INSERT INTO dbo.CTHoaDon (GiaTien, SoLuong, MaHD, MaSP)
VALUES	(15000, 15, 'HD001', 'SP01'),
		(10000, 10, 'HD002', 'SP02'),
		(9000, 5, 'HD003', 'SP03'),
		(9000, 10, 'HD004', 'SP04'),
		(3000, 10, 'HD005', 'SP05'),
		(15000, 15, 'HD006', 'SP06'),
		(10000, 10, 'HD007', 'SP07'),
		(6000, 4, 'HD008', 'SP08'),
		(10000, 7, 'HD009', 'SP09'),
		(15000, 10, 'HD010', 'SP10')

GO
INSERT INTO dbo.PhanHoi (MaPH, NgayGui, ChuDe, NoiDung, MaKH)
VALUES	('PH01', CAST(N'2020-11-10' AS Date), N'Đánh giá', N'Rau tươi', 'KH002'),
		('PH02', CAST(N'2020-12-02' AS Date), N'Đánh giá', N'Củ quả sạch', 'KH009'),
		('PH03', CAST(N'2020-09-22' AS Date), N'Đánh giá', N'Giao hàng nhanh', 'KH003'),
		('PH04', CAST(N'2020-03-15' AS Date), N'Đánh giá', N'Rau tươi', 'KH004'),
		('PH05', CAST(N'2020-05-22' AS Date), N'Đánh giá', N'Giao hàng nhanh', 'KH008'),
		('PH06', CAST(N'2020-10-10' AS Date), N'Đánh giá', N'Rau tươi', 'KH007'),
		('PH07', CAST(N'2020-08-29' AS Date), N'Đánh giá', N'Củ quả sạch', 'KH005'),
		('PH08', CAST(N'2020-01-11' AS Date), N'Đánh giá', N'Rau tươi', 'KH001'),
		('PH09', CAST(N'2020-10-03' AS Date), N'Đánh giá', N'Củ quả sạch', 'KH004'),
		('PH10', CAST(N'2020-12-10' AS Date), N'Đánh giá', N'Giao hàng nhanh', 'KH005')
GO


------------------- TÌM KIẾM ----------------------------------------
GO
CREATE PROCEDURE HoaDon_TimKiemNC
    @MaHD varchar(30)=NULL,
	@NgayDat nvarchar(15)=NULL,
	@NgayGiao nvarchar(15)= NULL,
	@DiaChi nvarchar(50)=NULL,
	@MaTT nvarchar(15)=NULL,
	@MaNV nvarchar(15)=NULL
	@MaKH nvarchar(15)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM HoaDon
       WHERE  (1=1)
       '
IF @MaHD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaHD LIKE ''%'+@MaHD+'%'')
              '
IF @NgayDat IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (NgayDat LIKE ''%'+@NgayDat+'%'')
              '
IF @NgayGiao IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (NgayGiao LIKE ''%'+@NgayGiao+'%'')
             '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE ''%'+@DiaChi+'%'')
              '
IF @MaTT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaTT LIKE ''%'+@MaTT+'%'')
              '
IF @MaNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNV LIKE ''%'+@MaNV+'%'')
              '
IF @MaKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaKH LIKE ''%'+@MaKH+'%'')
              '

	EXEC SP_EXECUTESQL @SqlStr
END
GO


GO
CREATE PROCEDURE PhanHoi_TimKiemNC
    @MaPH varchar(30)=NULL,
	@NgayGui nvarchar(15)=NULL,
	@ChuDe nvarchar(15)= NULL,
	@NoiDung nvarchar(50)=NULL,
	@MaKH nvarchar(15)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM PhanHoi
       WHERE  (1=1)
       '
IF @MaPH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaPH LIKE ''%'+@MaPH+'%'')
              '
IF @NgayGui IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (NgayGui LIKE ''%'+@NgayGui+'%'')
              '
IF @ChuDe IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (ChuDe LIKE ''%'+@ChuDe+'%'')
             '
IF @NoiDung IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (NoiDung LIKE ''%'+@NoiDung+'%'')
              '
IF @MaKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaKH LIKE ''%'+@MaKH+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
GO

GO
CREATE PROCEDURE NhanVien_TimKiemNC
    @MaNV varchar(15)=NULL,
	@TaiKhoan nvarchar(30)=NULL,
	@MatKhau nvarchar(20)=NULL,
	@HoNV nvarchar(30)=NULL,
	@TenNV nvarchar(30)=NULL,
	@SoDT nvarchar(15)= NULL,
	@DiaChi nvarchar(50)=NULL,
	@Email nvarchar(30)=NULL,
	@Luong nvarchar(30)=NULL,
	@NgaySinh nvarchar(20)=NULL,
	@GioiTinh nvarchar(5)=NULL,
	@HinhDD nvarchar(50)=NULL,
	@MaNhom nvarchar(15)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNV LIKE ''%'+@MaNV+'%'')
              '
IF @TaiKhoan IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TaiKhoan LIKE ''%'+@TaiKhoan+'%'')
              '
IF @MatKhau IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MatKhau LIKE ''%'+@MatKhau+'%'')
              '
IF @HoNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (HoNV LIKE ''%'+@HoNV+'%'')
              '
IF @TenNV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (TenNV LIKE ''%'+@TenNV+'%'')
              '
IF @SoDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SoDT LIKE ''%'+@SoDT+'%'')
             '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE ''%'+@DiaChi+'%'')
              '
IF @Email IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (Email LIKE ''%'+@Email+'%'')
              '
IF @Luong IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (Luong LIKE ''%'+@Luong+'%'')
              '
IF @NgaySinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (NgaySinh LIKE ''%'+@NgaySinh+'%'')
              '
IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (GioiTinh LIKE ''%'+@GioiTinh+'%'')
              '
IF @HinhDD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HinhDD LIKE ''%'+@HinhDD+'%'')
              '
IF @MaNhom IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNhom LIKE ''%'+@MaNhom+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
GO

GO
CREATE PROCEDURE SanPham_TimKiemNC
    @MaSP varchar(15)=NULL,
	@TenSP nvarchar(30)=NULL,
	@HinhDD nvarchar(50)=NULL,
	@SoLuong nvarchar(50)=NULL,
	@MoTaCT nvarchar(1000)=NULL,
	@GiaBan nvarchar(30)=NULL,
	@PhoBien nvarchar(15)=NULL,
	@NgayDang nvarchar(15)= NULL,
	@MaNCC nvarchar(15)=NULL,
	@MaDM nvarchar(15)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaSP IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaSP LIKE ''%'+@MaSP+'%'')
              '
IF @TenSP IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (TenSP LIKE ''%'+@TenSP+'%'')
              '
IF @HinhDD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (HinhDD LIKE ''%'+@HinhDD+'%'')
              '
IF @SoLuong IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (SoLuong LIKE ''%'+@SoLuong+'%'')
              '
IF @MoTaCT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (MoTaCT LIKE ''%'+@MoTaCT+'%'')
              '
IF @GiaBan IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (GiaBan LIKE ''%'+@GiaBan+'%'')
              '
IF @PhoBien IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (PhoBien LIKE ''%'+@PhoBien+'%'')
              '
IF @NgayDang IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (NgayDang LIKE ''%'+@NgayDang+'%'')
             '
IF @MaNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNCC LIKE ''%'+@MaNCC+'%'')
              '
IF @MaDM IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaDM LIKE ''%'+@MaDM+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
GO


GO
CREATE PROCEDURE KhachHang_TimKiemNC
    @MaKH varchar(15)=NULL,
	@TaiKhoan nvarchar(30)=NULL,
	@MatKhau nvarchar(20)=NULL,
	@HoKH nvarchar(30)=NULL,
	@TenKH nvarchar(30)=NULL,
	@NgaySinh nvarchar(20)=NULL,
	@SoDT nvarchar(15)= NULL,
	@DiaChi nvarchar(50)=NULL,
	@Email nvarchar(30)=NULL,
	@GioiTinh nvarchar(5)=NULL,
	@HinhDD nvarchar(50)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaKH LIKE ''%'+@MaKH+'%'')
              '
IF @TaiKhoan IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (TaiKhoan LIKE ''%'+@TaiKhoan+'%'')
              '
IF @MatKhau IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MatKhau LIKE ''%'+@MatKhau+'%'')
              '
IF @HoKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (HoKH LIKE ''%'+@HoKH+'%'')
              '
IF @TenKH IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (TenKH LIKE ''%'+@TenKH+'%'')
              '
IF @NgaySinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (NgaySinh LIKE ''%'+@NgaySinh+'%'')
              '
IF @SoDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SoDT LIKE ''%'+@SoDT+'%'')
             '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE ''%'+@DiaChi+'%'')
              '
IF @Email IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (Email LIKE ''%'+@Email+'%'')
              '
IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (GioiTinh LIKE ''%'+@GioiTinh+'%'')
              '
IF @HinhDD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HinhDD LIKE ''%'+@HinhDD+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
GO


GO
CREATE PROCEDURE NhaCungCap_TimKiemNC
    @MaNCC varchar(15)=NULL,
	@TenNCC nvarchar(50)=NULL,
	@DiaChi nvarchar(50)=NULL,
	@SoDT nvarchar(15)= NULL,
	@HinhDD nvarchar(50)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM NhanVien
       WHERE  (1=1)
       '
IF @MaNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaNCC LIKE ''%'+@MaNCC+'%'')
              '
IF @TenNCC IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
               AND (TenNCC LIKE ''%'+@TenNCC+'%'')
              '
IF @DiaChi IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (DiaChi LIKE ''%'+@DiaChi+'%'')
              '
IF @SoDT IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (SoDT LIKE ''%'+@SoDT+'%'')
             '
IF @HinhDD IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HinhDD LIKE ''%'+@HinhDD+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
GO


---------------- Thống kê --------------------------------
