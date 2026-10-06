CREATE DATABASE NhaSachTriThucDB;
GO

USE NhaSachTriThucDB;
GO

CREATE TABLE TheLoaiSach (
    MaTL INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL,
    SoLuongSach INT DEFAULT 0,
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Chèn thử vài dòng mẫu
INSERT INTO TheLoaiSach (TenTheLoai, MoTa, SoLuongSach)
VALUES 
(N'Tiểu thuyết', N'Sách văn học tiểu thuyết', 10),
(N'Kỹ năng sống', N'Sách phát triển bản thân', 15),
(N'Thiếu nhi', N'Truyện tranh, cổ tích cho bé', 5);