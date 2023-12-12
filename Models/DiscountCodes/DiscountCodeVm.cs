using Models.Dishes;

namespace Models.DiscountCodes
{
    public class DiscountCodeVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public int Percentage { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int MinDiscountAmount { get; set; }
        public int MinOrderAmountRequired { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AppliesToAllProducts { get; set; }
        public virtual List<DishVm>? ApplicableProductIds { get; set; }
        public bool IsAvailableForUse { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}