using Domain;

namespace WebApp.Messages
{
    internal class InsertUserMessage
    {
        public InsertUserMessage(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}