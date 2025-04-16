namespace Simple_REST_API.Models
{
    public record ProductDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = null!;
        public decimal? Price { get; set; }
        public string? Description { get; set; } = null!;
    }
}
