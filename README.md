﻿# ProductApi
Dựa trên code ASP.NET Core API bạn đã push, đây là hướng dẫn chi tiết cách sử dụng Swagger UI để kiểm thử các endpoints:

## 1. Truy cập Swagger UI

Sau khi chạy ứng dụng (với `dotnet run`), mở trình duyệt và truy cập:
```
https://localhost:<port>/swagger
```
(Thay `<port>` bằng port thực tế, thường là 5001 hoặc 7042)

## 2. Các endpoints hiển thị trên Swagger

Swagger sẽ hiển thị tất cả các endpoints từ `ProductsController`:

### a. GET /api/products
- **Chức năng**: Lấy danh sách tất cả sản phẩm
- **Cách test**:
  1. Nhấn vào endpoint
  2. Nhấn "Try it out"
  3. Nhấn "Execute"
  4. Xem kết quả trong phần "Response body"

### b. GET /api/products/{id}
- **Chức năng**: Lấy thông tin sản phẩm theo ID
- **Cách test**:
  1. Nhấn vào endpoint
  2. Nhấn "Try it out"
  3. Nhập ID sản phẩm (ví dụ: 1)
  4. Nhấn "Execute"
  5. Xem kết quả (404 nếu không tìm thấy)

### c. POST /api/products
- **Chức năng**: Thêm sản phẩm mới
- **Cách test**:
  1. Nhấn vào endpoint
  2. Nhấn "Try it out"
  3. Chỉnh sửa JSON mẫu trong Request body:
  ```json
  {
    "name": "Tên sản phẩm",
    "description": "Mô tả sản phẩm",
    "price": 100000,
    "discountPrice": 90000,
    "stock": 50,
    "category": "Danh mục",
    "image": "url_hình_ảnh",
    "isActive": true
  }
  ```
  4. Nhấn "Execute"
  5. Xem kết quả (201 Created nếu thành công)

### d. PUT /api/products/{id}
- **Chức năng**: Cập nhật sản phẩm
- **Cách test**:
  1. Nhấn vào endpoint
  2. Nhấn "Try it out"
  3. Nhập ID sản phẩm cần cập nhật
  4. Chỉnh sửa JSON trong Request body
  5. Nhấn "Execute"

### e. DELETE /api/products/{id}
- **Chức năng**: Xóa sản phẩm
- **Cách test**:
  1. Nhấn vào endpoint
  2. Nhấn "Try it out"
  3. Nhập ID sản phẩm cần xóa
  4. Nhấn "Execute"
  5. Kiểm tra status code (204 No Content nếu thành công)

## 3. Các tính năng hữu ích của Swagger

1. **Xem model schema**: Nhấn vào "Schemas" để xem cấu trúc object Product
2. **Thử nghiệm trực tiếp**: Tất cả endpoints đều có thể test ngay trên giao diện
3. **Xác thực**: Nếu API có authentication, có thể cấu hình ngay trên Swagger
4. **Tải xuống tài liệu**: Nhấn "Download" để export tài liệu API dạng JSON/YAML

## 4. Lưu ý quan trọng

1. Đảm bảo ứng dụng đang chạy trong môi trường Development (Swagger chỉ hiển thị ở Development mặc định)
2. Nếu không thấy Swagger, kiểm tra lại Program.cs có:
```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```
3. Các request từ Swagger là request thật, sẽ ảnh hưởng đến dữ liệu thực tế

## 5. Mở rộng Swagger (nếu cần)

Thêm mô tả chi tiết hơn bằng cách cấu hình trong Program.cs:
```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "API quản lý sản phẩm",
        Contact = new OpenApiContact
        {
            Name = "Tên của bạn",
            Email = "email@example.com"
        }
    });
});
```

Bạn có thể test ngay các endpoints mà không cần dùng Postman hay các công cụ khác. Swagger UI cung cấp giao diện trực quan và dễ sử dụng.
