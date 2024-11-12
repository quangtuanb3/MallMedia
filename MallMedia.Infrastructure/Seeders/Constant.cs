
using MallMedia.Domain.Entities;
using MallMedia.Domain.Constants;

namespace MallMedia.Infrastructure.Seeders;

public static class CategoryData
{
    public static readonly List<Category> Categories = new List<Category>
    {
        new Category { Name = "Thời trang" },
        new Category { Name = "Mỹ phẩm" },
        new Category { Name = "Thực phẩm" },
        new Category { Name = "Điện tử" },
        new Category { Name = "Trang trí nhà cửa" },
        new Category { Name = "Trang sức" },
        new Category { Name = "Thể thao" },
        new Category { Name = "Giày dép" },
        new Category { Name = "Sách & Văn phòng phẩm" },
        new Category { Name = "Sức khỏe" },
        new Category { Name = "Đồ chơi" },
        new Category { Name = "Thời trang trẻ em" },
        new Category { Name = "Nội thất" },
        new Category { Name = "Nhà hàng" },
        new Category { Name = "Giải trí" },
        new Category { Name = "Đồ gia dụng" },
        new Category { Name = "Dịch vụ du lịch" },
        new Category { Name = "Quà tặng và lưu niệm" },
        new Category { Name = "Đồ dùng thú cưng" },
        new Category { Name = "Hàng hiệu" }
    };
}

public static class LocationData
{
    public static readonly List<Location> Locations = new List<Location>
    {
        new Location {Floor = 1, Department = "Sảnh chính"},
        new Location { Floor = 1, Department = "Khu vực ẩm thực" },
        new Location { Floor = 1, Department = "Khu vực thời trang - phụ kiện" },
        new Location { Floor = 1, Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 1, Department = "Khu vực thang máy" },
        new Location { Floor = 1, Department = "Khu vực thang cuốn" },
        new Location { Floor = 2, Department = "Khu vực thời trang - phụ kiện" },
        new Location { Floor = 2, Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 2, Department = "Khu vực thang máy" },
        new Location { Floor = 2, Department = "Khu vui thang cuốn" },
        new Location { Floor = 3, Department = "Khu vực ẩm thực" },
        new Location { Floor = 3, Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 3, Department = "Khu vui chơi - giải trí" },
        new Location { Floor = 3, Department = "Khu vực thang máy" },
        new Location { Floor = 3, Department = "Khu vực thang cuốn" },
        new Location { Floor = 4, Department = "Khu vực thời trang - phụ kiện" },
        new Location { Floor = 4, Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 4, Department = "Khu vực thang máy" },
        new Location { Floor = 4, Department = "Khu vực thang cuốn" },
    };
}

public static class DeviceData
{
    public static readonly List<Device> Devices = new List<Device>
{
    new Device
    {
        DeviceName = "Samsung-UA8",
        DeviceType = "TV",
        LocationId = 1,
        Configuration = new DeviceConfiguration { Size = "55 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "LG-50U",
        DeviceType = "TV",
        LocationId = 2,
        Configuration = new DeviceConfiguration { Size = "50 inches", Resolution = "1920x1080" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "Sony-X950H",
        DeviceType = "LED",
        LocationId = 3,
        Configuration = new DeviceConfiguration { Size = "65 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "TCL-75C1",
        DeviceType = "LED",
        LocationId = 4,
        Configuration = new DeviceConfiguration { Size = "75 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "Vizio-P-Series",
        DeviceType = "TV",
        LocationId = 5,
        Configuration = new DeviceConfiguration { Size = "65 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "Philips-55PUS",
        DeviceType = "LED",
        LocationId = 6,
        Configuration = new DeviceConfiguration { Size = "55 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "Hisense-H65",
        DeviceType = "TV",
        LocationId = 7,
        Configuration = new DeviceConfiguration { Size = "43 inches", Resolution = "1920x1080" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {

        DeviceName = "Sharp-LC-65",
        DeviceType = "LED",
        LocationId = 8,
        Configuration = new DeviceConfiguration { Size = "65 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "Samsung-Q60T",
        DeviceType = "LED",
        LocationId = 9,
        Configuration = new DeviceConfiguration { Size = "55 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "LG-BX",
        DeviceType = "TV",
        LocationId = 10,
        Configuration = new DeviceConfiguration { Size = "48 inches", Resolution = "3840x2160" },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
};
}
