namespace ChatApplication.ViewModels.MessagesViewModels;

public class ChatVM
{
    public ChatVM()
    {
        Messages = new List<UserMessagesListVM>();
    }
    public string CurrentUserId { get; set; }
    public string RecevierId { get; set; }
    public string RecevierUserName { get; set; }
    public IEnumerable<UserMessagesListVM> Messages { get; set; }
}
