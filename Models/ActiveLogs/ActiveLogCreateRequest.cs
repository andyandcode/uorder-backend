using Data.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.ActiveLogs
{
    public class ActiveLogCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.ActiveLog);

        [Required]
        public string EntityId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public EntityType EntityType { get; set; }

        [Required]
        public ActiveLogActionType ActiveLogActionType { get; set; }
    }
}