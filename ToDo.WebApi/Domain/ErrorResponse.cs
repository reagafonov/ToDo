namespace ToDo.WebApi.Domain;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public int Status { get; set; }
}