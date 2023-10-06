using Swashbuckle.AspNetCore.Annotations;
using Utilities.Common;

namespace Models.SystemSettings
{
    public class SystemSettingCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Dish);

        public int ChefCount { get; set; }
        public string Domain { get; set; }
    }
}