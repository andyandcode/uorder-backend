using Swashbuckle.AspNetCore.Annotations;

namespace Models.SystemSettings
{
    public class SystemSettingUpdateRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }

        public int ChefCount { get; set; }
        public string Domain { get; set; }
    }
}