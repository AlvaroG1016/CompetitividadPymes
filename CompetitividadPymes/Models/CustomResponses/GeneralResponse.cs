namespace CompetitividadPymes.Models.CustomResponses
{
    public class GeneralResponse
    {
        public bool Ok { get; set; }
        public List<Object> Data { get; set; }
        public string Message { get; set; }
    }
}
