namespace Models.Menus
{
    public class RemoveDishFromMenuRequest
    {
        public string MenuId { get; set; }
        public string DishId { get; set; }
    }
}