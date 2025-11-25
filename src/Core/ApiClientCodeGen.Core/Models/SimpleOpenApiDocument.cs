namespace Rapicgen.Core.Models
{
    public class SimpleOpenApiDocument
    {
        public string OpenApi { get; set; }
        public string Swagger { get; set; }
        public SimpleOpenApiInfo Info { get; set; }
        public string DocumentPath { get; set; }
    }

    public class SimpleOpenApiInfo
    {
        public string Title { get; set; }
    }
}
