namespace NewYearResolutionsAPI
{
    public class Resolution
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get;set; }
    }
}
