using Microsoft.AspNetCore.SignalR;
namespace MallMedia.Application.ConnectHubs;

public class ContentHub : Hub
{
    // Gửi thông báo cập nhật đến tất cả người dùng kết nối
    public async Task SendUpdateNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveUpdate", message);
    }
}
