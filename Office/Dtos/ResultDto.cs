namespace Office.Dtos
{
    public class ResultDto
    {
        public string Code { get; set; }
        public string Error { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public ResultDto()
        {
            Code = "200";
            Error = null;
            Msg = null;
            Data = null;
        }
    }
}
