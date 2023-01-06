using PracticeAPI3_23.Model;

namespace PracticeAPI3_23.Repository.Interface
{
    public interface IMasterDropdownRepository
    {
        public Task<List<DistrictModel>> GetAllDistricts();
        public Task<List<TalukaModel>> GetAllTaluka(int districtId);
        public Task<List<VillageModel>> GetAllVillages(int talukaId);

    }
}
