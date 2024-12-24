using ChatApplication.Data;
using ChatApplication.Helper;
using ChatApplication.Interface;
using ChatApplication.ViewModels.MessagesViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public MessageService(ApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    public async Task<ChatVM> GetChatAsync(string selectedUserId)
    {
        var currentUserId = _currentUserService.UserId;
        var selectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == selectedUserId);
        var selectedUserName = "";
        if (selectedUser != null)
            selectedUserName = selectedUser.UserName;
        var Chat = new ChatVM
        {
            CurrentUserId = currentUserId,
            RecevierId = selectedUserId,
            RecevierUserName = selectedUserName,
        };

        var messages = _context.Messages
              .Where(m => (m.SenderId == currentUserId && m.ReceiverId == selectedUserId) ||
                        (m.SenderId == selectedUserId && m.ReceiverId == currentUserId))
           //.Where(m => (m.SenderId == currentUserId || m.SenderId == selectedUserId) &&
           //            (m.ReceiverId == currentUserId || m.ReceiverId == selectedUserId))
           .Select(m => new UserMessagesListVM
           {
               Id = m.Id,
               Text = m.Text,
               Date = m.Date.ToShortDateString(),
               Time = m.Date.ToShortTimeString(),
               IsCurrentUserSentMessage = m.SenderId == currentUserId
           });
        Chat.Messages = messages;
        return Chat;
      
    }

    public async Task<IEnumerable<MessageUsersListVM>> GetUsersListAsync()
    {
        var currentUserId = _currentUserService.UserId;

        var users = await _context.Users
            .Where(u => u.Id != currentUserId)
            .Select(u => new MessageUsersListVM
            {
                Id = u.Id,
                UserName = u.UserName,
                LastMessage = _context.Messages
                    .Where(m => (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                               (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                    .OrderByDescending(m => m.Id)
                    .Select(m => m.Text)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return users;
    }

}
