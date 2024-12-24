using ChatApplication.ViewModels.MessagesViewModels;

namespace ChatApplication.Interface;


public interface IMessageService
{
    public Task<IEnumerable<MessageUsersListVM>> GetUsersListAsync();
    public Task<ChatVM> GetChatAsync(string selectedUserId);
}
