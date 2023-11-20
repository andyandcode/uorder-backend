namespace Data.Entities
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}