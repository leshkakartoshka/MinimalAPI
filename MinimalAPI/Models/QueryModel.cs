namespace MinimalAPI.Models
{
    public class UpdateCreateCommand
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public List<UpdateCreateParameter>? Parameters { get; set; }
    }

    public class UpdateCreateParameter
    {
        public string? ParameterName { get; set; }
        public string? ParameterDescription { get; set; }
    }
}
