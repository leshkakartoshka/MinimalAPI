namespace MinimalAPI.Models
{
    public class CreateCommand
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public List<CreateParameter>? Parameters { get; set; }
    }

    public class UpdateCommand
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public List<CreateParameter>? Parameters { get; set; }
    }

    public class CreateParameter
    {
        public string? ParameterName { get; set; }
        public string? ParameterDescription { get; set; }
    }
}
