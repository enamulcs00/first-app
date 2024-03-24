namespace first_app.Models
{
    public interface IGeneralResponse
    {
       public string Message { get; set; }
      public  string[] Data { get; set; }
    }
}
