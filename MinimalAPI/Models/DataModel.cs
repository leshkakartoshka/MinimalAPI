namespace MinimalAPI.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public List<Parameters>? Parameters { get; set; }
    }

    public class Parameters
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string? ParameterName { get; set; }
        public string? ParameterDescription { get; set; }
    }

}
