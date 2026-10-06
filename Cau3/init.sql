CREATE DATABASE SunriseHomestay;
GO
USE SunriseHomestay;
GO

CREATE TABLE LoaiPhong (
    MaLoai int IDENTITY(1,1) PRIMARY KEY,
    TenLoai nvarchar(100) NOT NULL,
    GiaMoiDem decimal(18,2),
    MoTa nvarchar(255)
);

CREATE TABLE Phong (
    MaPhong int IDENTITY(1,1) PRIMARY KEY,
    SoPhong varchar(10) NOT NULL,
    TangSo int,
    TinhTrang nvarchar(20),
    HinhAnh nvarchar(255),
    MaLoai int,
    CONSTRAINT FK_Phong_LoaiPhong FOREIGN KEY (MaLoai) REFERENCES LoaiPhong(MaLoai)
);

INSERT INTO LoaiPhong (TenLoai, GiaMoiDem, MoTa) VALUES
(N'Phòng Đơn', 350000, N'Phòng cho 1 người'),
(N'Phòng Đôi', 550000, N'Phòng cho 2 người'),
(N'Phòng VIP', 1200000, N'Phòng cao cấp');

INSERT INTO Phong (SoPhong, TangSo, TinhTrang, HinhAnh, MaLoai) VALUES
('101', 1, N'Trống', NULL, 1),
('102', 1, N'Đang ở', NULL, 1),
('201', 2, N'Đang dọn', NULL, 2);
GO
