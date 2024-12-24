using ChatApplication.Data;
using ChatApplication.Helper;
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ChatHub(ApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task SendMessage(string receiverId, string message)
    {
        //var Now = DateTime.Now;
        //var Date = Now.ToShortDateString();
        //var Time = Now.ToShortTimeString();

        var senderId = _currentUserService.UserId;
        var newMessage = new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Text = message,
            Date = DateTime.Now
        };

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        await Clients.User(receiverId).SendAsync("ReceiveMessage", newMessage);
    }
}
