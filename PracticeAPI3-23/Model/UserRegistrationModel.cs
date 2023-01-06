using System.Xml.Linq;

namespace PracticeAPI3_23.Model
{
    public class UserRegistrationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? userName { get; set; }
        public string? mobileNo { get; set; }
        public string? emailId { get; set; }
        public int districtId { get; set; }
        public int talukaId { get; set; }
        public int vilageId { get; set; }
        public string? password { get; set; }
        public int createdBy { get; set; }
        public DateTime createDate { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isDeleted { get; set; }
    }

    public class DeleteUserObject
    {
        public long Id { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
    public class UserRegistrationPaginationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? userName { get; set; }
        public string? mobileNo { get; set; }
        public string? emailId { get; set; }
        public int districtId { get; set; }
        public string? districtName { get; set; }
        public int talukaId { get; set; }
        public string? talukaName { get; set; }
        public int vilageId { get; set; }
        public string? villageName { get; set; }
        public string? password { get; set; }
        public int createdBy { get; set; }
        public DateTime createDate { get; set; }
        public int modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
