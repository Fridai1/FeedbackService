namespace FeedbackService
{
    public class Feedback
    {
        public Feedback()
        {
            
        }

        public Feedback(int id, string titel, string name, string message)
        {
            Id = id;
            Title = titel;
            Name = name;
            Message = message;
        }

        public string Title { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }
    }
}