# Hệ Thống Quản Lý Khách Sạn

## Giới Thiệu
Hệ thống quản lý khách sạn này là một ứng dụng desktop được xây dựng bằng .NET với Windows Forms trong C#. Ứng dụng được thiết kế để quản lý các hoạt động khách sạn như đặt phòng, quản lý khách hàng, lập hóa đơn, và thống kê doanh thu. Hệ thống sử dụng SQL Server để quản lý cơ sở dữ liệu và Crystal Reports để tạo các báo cáo chi tiết.

## Tính Năng
1. **Quản Lý Phòng**:
   - Thêm, cập nhật, và xóa thông tin phòng.
   - Quản lý trạng thái phòng trống và tình trạng đặt phòng.

2. **Quản Lý Loại Phòng**:
   - Định nghĩa và quản lý các loại phòng khác nhau (ví dụ: phòng đơn, phòng đôi, phòng suite).
   - Cập nhật giá và tính năng của từng loại phòng.

3. **Quản Lý Khách Hàng**:
   - Quản lý thông tin khách hàng (tên, liên hệ, giấy tờ tùy thân).
   - Theo dõi hoạt động check-in và check-out của khách hàng.

4. **Quản Lý Hóa Đơn**:
   - Tạo hóa đơn cho khách hàng.
   - Bao gồm thông tin chi tiết về phí phòng, dịch vụ và thuế.

5. **Thống Kê Doanh Thu**:
   - Xem báo cáo doanh thu theo ngày, tuần hoặc tháng.
   - Sử dụng Crystal Reports để tạo và xuất các thống kê chi tiết.

## Công Nghệ Sử Dụng
- **Ngôn ngữ**: C#
- **Framework**: .NET Framework (Windows Forms)
- **Cơ Sở Dữ Liệu**: SQL Server
- **Báo Cáo**: Crystal Reports

## Hướng Dẫn Cài Đặt

1. **Yêu Cầu**:
   - Visual Studio 2019 (hoặc cao hơn)
   - .NET Framework
   - SQL Server (Local hoặc Remote)
   - Crystal Reports Runtime cho .NET Framework

2. **Cài Đặt Cơ Sở Dữ Liệu**:
   - Tạo cơ sở dữ liệu SQL Server bằng script được cung cấp (`/Database/setup.sql`).
   - Cấu hình chuỗi kết nối trong tệp cấu hình ứng dụng (`app.config`).

3. **Chạy Ứng Dụng**:
   - Mở dự án trong Visual Studio.
   - Khôi phục các gói NuGet và xây dựng giải pháp.
   - Chạy ứng dụng từ Visual Studio.

## Hướng Dẫn Sử Dụng

### Quản Lý Phòng
- Điều hướng đến phần **Quản Lý Phòng** để thêm hoặc cập nhật chi tiết phòng, hoặc kiểm tra tình trạng phòng trống.

### Quản Lý Khách Hàng
- Trong phần **Quản Lý Khách Hàng**, bạn có thể thêm, xem hoặc chỉnh sửa thông tin khách hàng, và quản lý hoạt động check-in/check-out.

### Tạo Hóa Đơn
- Để tạo hóa đơn, đi đến phần **Hóa Đơn**, chọn khách hàng và xem chi tiết thanh toán của họ, sau đó tạo và in hóa đơn bằng Crystal Reports.

### Thống Kê Doanh Thu
- Sử dụng phần **Thống Kê** để tạo và xem các báo cáo doanh thu. Bạn có thể chọn các khoảng thời gian khác nhau và xuất báo cáo sang định dạng PDF hoặc Excel.


