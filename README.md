# 🛡️ Hệ Thống Quản Lý Bảo Hiểm (Insurance Management System)

Dự án phần mềm quản lý bảo hiểm được xây dựng bằng **C# (Windows Forms)** và cơ sở dữ liệu **SQL Server**. Hệ thống giúp quản lý thông tin khách hàng, hợp đồng bảo hiểm, kế toán và tài khoản nhân viên.

## 🚀 Tính năng chính

* **Quản lý hệ thống:** Đăng nhập, phân quyền người dùng.
* **Quản lý khách hàng (QLNBH):** Lưu trữ, tìm kiếm, cập nhật thông tin người mua bảo hiểm.
* **Quản lý hợp đồng:** Tạo mới, theo dõi trạng thái hợp đồng bảo hiểm.
* **Quản lý kế toán:** Theo dõi thu chi, thanh toán.
* **Trang Admin:** Thống kê và quản lý tài khoản nhân viên.

## 🛠️ Công nghệ sử dụng

* **Ngôn ngữ:** C# (.NET Framework 4.7.2)
* **Giao diện:** Windows Forms (WinForms)
* **Cơ sở dữ liệu:** SQL Server
* **IDE:** Visual Studio 2019/2022

## ⚙️ Hướng dẫn cài đặt (Installation)

Để chạy được dự án, bạn cần thực hiện các bước sau:

### 1. Cài đặt Database
1.  Mở **SQL Server Management Studio (SSMS)**.
2.  Mở file script: `QLBAOHIEM.sql` (nằm trong thư mục gốc).
3.  Chạy toàn bộ script (Execute/F5) để tạo Database và các bảng dữ liệu.
4.  *(Lưu ý: Kiểm tra xem trong script có dòng `INSERT` nào tạo tài khoản Admin mặc định không để dùng đăng nhập).*

### 2. Cấu hình kết nối (Connection String)
1.  Mở dự án bằng **Visual Studio** (mở file `Login.sln`).
2.  Tìm file `App.config` trong Solution Explorer.
3.  Tìm thẻ `<connectionStrings>`.
4.  Sửa lại `Data Source` thành tên Server của bạn (thường là `.` hoặc `.\SQLEXPRESS` hoặc tên máy tính của bạn).

Ví dụ:
`<add name="MyConn" connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=QLBAOHIEM;Integrated Security=True" providerName="System.Data.SqlClient" />`

### 3. Chạy chương trình
Nhấn Start hoặc F5 trong Visual Studio để chạy.



