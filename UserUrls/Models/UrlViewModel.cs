namespace UserUrls.Models
{
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string[] Content { get; set; }

        public UrlViewModel()
        {
            Id = 0;
            Url = "";
            Content = new string[] { "" };
        }

        public UrlViewModel(int id, string url, string content)
        {
            Id = id;
            Url = url;
            Content = content.Split("_l_");
        }
    }
}
