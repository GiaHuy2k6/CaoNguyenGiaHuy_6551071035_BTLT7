-- =============================================
-- Script tạo CSDL GymFitZone và bảng HoiVien
-- =============================================

-- Tạo database nếu chưa có
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GymFitZone')
BEGIN
    CREATE DATABASE GymFitZone;
END
GO

USE GymFitZone;
GO

-- Tạo bảng HoiVien
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HoiVien')
BEGIN
    CREATE TABLE HoiVien (
        MaHV         INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
        HoTen        NVARCHAR(100) NOT NULL,
        GioiTinh     BIT           NOT NULL DEFAULT 1,  -- 1=Nam, 0=Nữ
        NgaySinh     DATE          NULL,
        SDT          VARCHAR(15)   NULL,
        Email        VARCHAR(100)  NULL,
        HangThanhVien NVARCHAR(20) NULL,  -- Basic / VIP / Premium
        NgayDangKy   DATETIME      NOT NULL DEFAULT GETDATE(),
        TrangThai    BIT           NOT NULL DEFAULT 1   -- 1=Đang hoạt động, 0=Tạm ngưng
    );
END
GO

-- Dữ liệu mẫu
INSERT INTO HoiVien (HoTen, GioiTinh, NgaySinh, SDT, Email, HangThanhVien, TrangThai)
VALUES
    (N'Họ tên Vân',    1, '1999-11-07', '07382735879', 'van@gmail.com',     N'Basic',   1),
    (N'Nguyễn Nnh',   1, '1999-11-10', '07382731034', 'nnh@gmail.com',     N'Premium', 1),
    (N'Nguyễn Bộ Đôn',1, '1999-12-29', '07882773727', 'bodong@gmail.com',  N'Basic',   1),
    (N'Nguyễn Xim',   1, '1999-11-17', '07375856597', 'xim@gmail.com',     N'VIP',     1),
    (N'Nguyễn Tương', 0, '1999-01-22', '07887775931', 'tuong@gmail.com',   N'Premium', 1);
GO

PRINT 'Tạo CSDL và dữ liệu mẫu thành công!';
GO
