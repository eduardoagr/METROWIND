namespace METROWIND.ViewModel
{

    public partial class SupportPageViewModel(IEmail email) : ObservableObject
    {

        [ObservableProperty]
        string? textToSend;

        [RelayCommand]
        async Task SendEmail()
        {
            if (email.IsComposeSupported)
            {

                string subject = "Hello friends!";
                string body = TextToSend!;
                string[] recipients = ["egomezr@outlook.com"];

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    BodyFormat = EmailBodyFormat.PlainText,
                    To = new List<string>(recipients)
                };

                await email.ComposeAsync(message);
            }
        }
    }
}