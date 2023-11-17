using Data.Enums;

namespace Models.ActiveLogs
{
    public class ActiveLogVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public EntityType EntityType { get; set; }
        public ActiveLogActionType ActiveLogActionType { get; set; }
    }
}