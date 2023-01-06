namespace PracticeAPI3_23.Model
{
    public class VillageModel
    {
        public int Id { get; set; }
        public string? villageName { get; set; }
        public int talukaId { get; set; }
        public int districtId { get; set; }
        public bool isDeleted { get; set; }
    }
}
