namespace PracticeAPI3_23.Model
{
    public class SchoolTypeModel
    {
        public long Id { get; set; }
        public string? SchoolType { get; set; }
        public string? M_SchoolType { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeStamp { get; set; }

    }
    public class DeleteUserObjects
    {
        public long Id { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
