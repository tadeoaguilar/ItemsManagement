namespace eItems.Catalog.Data.Model.Companies.Dto
{
    public record CompanyDto
    {
        public Guid TenantID { get; set; }
        public Guid CountryID { get; set; }
        public string CompanyCD { get; set; } = default!;
        public string? Description { get; set; }
        public bool Active { get; set; }
    }

    public record CompanyResponseDto
    {
        public Guid Id { get; set; }
        public string CompanyCD { get; set; } = default!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        
        // Include only needed properties from navigation entities
        public string? TenantName { get; set; }
        public string? CountryName { get; set; }
    }
}