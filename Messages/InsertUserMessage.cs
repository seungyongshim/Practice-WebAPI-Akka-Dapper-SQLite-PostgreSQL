using Domain;

namespace Messages
{
    public class InsertUserMessage
    {
        public InsertUserMessage(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}