namespace ChatApplication.ViewModels.MessagesViewModels;

public class UserMessagesListVM
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }

    public bool IsCurrentUserSentMessage { get; set; }
}