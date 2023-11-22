using Data.Enums;

namespace Data.Entities
{
    public class ActiveLog
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public EntityType EntityType { get; set; }
        public ActiveLogActionType ActiveLogActionType { get; set; }
    }
}