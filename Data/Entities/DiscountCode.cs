namespace Data.Entities
{
    public class DiscountCode
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public int Percentage { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int MinDiscountAmount { get; set; }
        public int MinOrderAmountRequired { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AppliesToAllProducts { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<DiscountProduct>? ApplicableProductIds { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public bool IsDeleted { get; set; }
    }
}