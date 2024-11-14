﻿
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
        new Location { Floor = 1 , Department = "Khu vực Cổng chính" },
        new Location { Floor = 1 , Department = "Khu vực Thời trang - phụ kiện" },
        new Location { Floor = 1 , Department = "Khu vực Đồ ăn & nước uống" },
        new Location { Floor = 1 , Department = "Khu vực Tủ đồ thông minh E-Locker" },
        new Location { Floor = 1 , Department = "Khu vực thang máy" },
        new Location { Floor = 1 , Department = "Khu vực thang cuốn" },
        new Location { Floor = 2 , Department = "Khu vực Thời trang - phụ kiện" },
        new Location { Floor = 2 , Department = "Khu vực thang máy" },
        new Location { Floor = 2 , Department = "Khu vực thang cuốn" },
        new Location { Floor = 3 , Department = "Khu vực Đồ ăn & nước uống" },
        new Location { Floor = 3 , Department = "Khu vực vui chơi giải trí" },
        new Location { Floor = 3 , Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 3 , Department = "Khu vực thang máy" },
        new Location { Floor = 3 , Department = "Khu vực thang cuốn" },
        new Location { Floor = 4 , Department = "Khu vực thời trang - phụ kiện" },
        new Location { Floor = 4 , Department = "Khu vực cửa hàng chuyên dụng" },
        new Location { Floor = 4 , Department = "Khu vực thang máy" },
        new Location { Floor = 4 , Department = "Khu vực thang cuốn" },
    };
}
public static class DeviceData
{
    public static readonly List<Device> Devices = new List<Device>
{

             new Device
    {
        DeviceName = "DP-F4.1",
        LocationId = 15,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP ,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
             new Device
    {
        DeviceName = "DP-F4.2",
        LocationId = 16,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP ,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                    new Device
    {
        DeviceName = "LCD-F4.1",
        LocationId = 17,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD ,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                             new Device
    {
        DeviceName = "LCD-F4.2",
        LocationId = 18,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD ,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
        new Device
    {
        DeviceName = "DP-F3.1",
        LocationId = 11,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP ,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
             new Device
    {
        DeviceName = "DP-F3.2",
        LocationId = 12,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP ,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                    new Device
    {
        DeviceName = "LCD-F3.1",
        LocationId = 13,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD ,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                             new Device
    {
        DeviceName = "LCD-F3.2",
        LocationId = 14,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD ,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },

    new Device
    {
        DeviceName = "LED-F1",
        LocationId = 1,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LED,
            Size = DeviceSize.LED,
            Resolution = DeviceResolution.LED
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
        new Device
    {
        DeviceName = "LED-F2",
        LocationId = 7,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LED,
            Size = DeviceSize.LED,
            Resolution = DeviceResolution.LED
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
            new Device
    {
        DeviceName = "LED-F3",
        LocationId = 10,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LED,
            Size = DeviceSize.LED,
            Resolution = DeviceResolution.LED
        },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "DP-F1.1",
        LocationId = 2,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "DP-F1.2",
        LocationId = 2,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "DP-F1.3",
        LocationId = 3,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
    new Device
    {
        DeviceName = "DP-F1.4",
        LocationId = 4,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
      new Device
    {
        DeviceName = "LCD-F1.1",
        LocationId = 5,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
            new Device
    {
        DeviceName = "LCD-F1.2",
        LocationId = 6,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.LCD,
            Size = DeviceSize.LCD,
            Resolution = DeviceResolution.LCD },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },

       new Device
    {
        DeviceName = "DP-F2.1",
        LocationId = 7,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },

       new Device
    {
        DeviceName = "DP-F2.2",
        LocationId =7,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },

       new Device
    {
        DeviceName = "DP-F2.3",
        LocationId =8,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
          new Device
    {
        DeviceName = "DP-F2.4",
        LocationId =8,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                 new Device
    {
        DeviceName = "LCD-F2.1",
        LocationId =9,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
                        new Device
    {
        DeviceName = "LCD-F2.2",
        LocationId =10,
        Configuration = new DeviceConfiguration {
            DeviceType = DeviceType.DP,
            Size = DeviceSize.DP,
            Resolution = DeviceResolution.DP },
        Status = "Active",
        CreatedAt = DateTime.UtcNow
    },
};
}
