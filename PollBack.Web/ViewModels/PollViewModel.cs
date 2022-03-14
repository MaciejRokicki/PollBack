namespace PollBack.Web.ViewModels
{
    public class PollViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool IsDraft { get; set; }
        public DateTime Created { get; set; }
        public DateTime End { get; set; }
        public int TotalVotes { get; set; }
        public ICollection<PollOptionViewModel> Options { get; set; } = Array.Empty<PollOptionViewModel>();

        public PollViewModel(string question)
        {
            Question = question;
        }
    }
}
