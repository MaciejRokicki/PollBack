namespace PollBack.Web.ViewModels
{
    public class PollOptionViewModel
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public int Votes { get; set; }

        public PollOptionViewModel(string option)
        {
            Option = option;
        }
    }
}
