using Swashbuckle.AspNetCore.Annotations;
using Utilities.Common;

namespace Models.Medias
{
    public class MediaCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Menu);

        public string Desc { get; set; }
        public string Path { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;
    }
}