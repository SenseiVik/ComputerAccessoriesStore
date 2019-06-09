namespace ComputerAccessoriesStore.Domain.Entities
{
    public class EmailSettings
    {
        public string MailToAddress { get; set; }
        public string MailFromAddress { get; set; }
        public bool UseSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
        public bool WriteAsFile { get; set; }
        public string FileLocation { get; set; }

        public EmailSettings()
        {
            MailToAddress = "order@example.com";
            MailFromAddress = "computeracessories@example.com";
            UseSsl = true;
            Username = "mySmtpName";
            Password = "mySmtpPassword";
            ServerName = "smtp.example.com";
            ServerPort = 587;
            WriteAsFile = false;
            FileLocation = "cartinfo.txt";
        }
    }
}
