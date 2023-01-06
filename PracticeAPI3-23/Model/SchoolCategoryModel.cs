namespace PracticeAPI3_23.Model
{
    public class SchoolCategoryModel
    {
        public long Id { get; set; }
        public string? CategoryName { get; set; }
        public string? M_CategoryName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

