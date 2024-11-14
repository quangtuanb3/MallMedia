using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.ConnectHubs
{
    public class ContentHub : Hub
    {
        public async Task SendContentUpdate(string Id, string Tittle)
        {
            // Gửi cập nhật nội dung đến một thiết bị cụ thể bằng deviceId
            await Clients.All.SendAsync("ReceiveContentUpdate", Id, Tittle);
        }
        // Gửi thông báo cập nhật đến tất cả người dùng kết nối
        public async Task SendUpdateNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
        // Send Notification to a Group
        public async Task SendGroupNotification(string groupName, string Message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupNotification", Message);
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessage(string user, string message)
        => await Clients.All.SendAsync("ReceiveMessage", user, message);

        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        public async Task SendMessageToGroup(string user, string message)
            => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
    }
}
