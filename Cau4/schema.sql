CREATE DATABASE AnKhangClinicDB;
GO
USE AnKhangClinicDB;
GO

CREATE TABLE BacSi (
    MaBS INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    ChuyenKhoa NVARCHAR(100),
    SDT VARCHAR(15)
);
GO

CREATE TABLE LichKham (
    MaLich INT PRIMARY KEY IDENTITY(1,1),
    TenBenhNhan NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    NgayKham DATE,
    GioKham NVARCHAR(10),
    MaBS INT,
    TrangThai NVARCHAR(20),
    FOREIGN KEY (MaBS) REFERENCES BacSi(MaBS)
);
GO

INSERT INTO BacSi (HoTen, ChuyenKhoa, SDT) VALUES 
(N'BS. Nguyễn Văn A', N'Nội tổng quát', '0912345678'),
(N'BS. Nguyễn Văn B', N'Nội tổng quát', '0912345679'),
(N'BS. Nguyễn Văn C', N'Nội tổng quát', '0912345670'),
(N'BS. Nguyễn Văn D', N'Nội tổng quát', '0912345671');
GO

INSERT INTO LichKham (TenBenhNhan, SDT, NgayKham, GioKham, MaBS, TrangThai) VALUES 
(N'Tên bệnh nhân', '09725667894', '2022-07-13', '09:00', 1, N'Chờ khám'),
(N'Nguyễn Xinh', '09725667897', '2022-12-29', '13:00', 1, N'Đã khám'),
(N'Nguyễn Hạm', '09725667899', '2022-12-23', '16:00', 1, N'Đã hủy');
GO
