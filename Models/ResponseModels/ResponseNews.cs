using LaboratoryWebAPI.Models.Entities;

namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class ResponseNews
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string PublicationDate { get; set; }
        public string NewsText;
        public byte[] ImagePreview;
        public ResponseNews(LaboratoryNews news)
        {
            Id = news.Id;
            Header = news.Header;
            Author = news.User.TypeOfUser.Name + " " + news.User.Name;
            PublicationDate = news.DateTime.ToString("yyyy-MM-dd hh:mm:ss");
            NewsText = news.Text;
            ImagePreview = news.ImagePreview;
        }
    }
}