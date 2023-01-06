using Dapper;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;

namespace PracticeAPI3_23.Repository
{
    public class MasterDropdownRepository : BaseAsyncRepository, IMasterDropdownRepository
    {       
        public MasterDropdownRepository(IConfiguration configuration) : base(configuration)
        { }
    
        public async Task<List<DistrictModel>> GetAllDistricts()
        {
            var query = @"select Id,districtName from tblDistrict where isDeleted=0";
            using (var con = sqlwriterConnection)
            {
                var result = await con.QueryAsync<DistrictModel>(query);
                return result.ToList();

            }
        }

        public async Task<List<TalukaModel>> GetAllTaluka(int districtId)
        {
            var query = @"select Id,talukaName,districtId from tblTaluka where districtId=@districtId and isDeleted=0";
            using (var con = sqlwriterConnection)
            {
                var result = await con.QueryAsync<TalukaModel>(query, new { districtId});
                return result.ToList();

            }

        }

        public async Task<List<VillageModel>> GetAllVillages(int talukaId)
        {
            var query = @"select Id,VillageName,talukaId,districtId from tblVillage where talukaId=@talukaId and isDeleted=0";
            using (var con = sqlwriterConnection)
            {
                var result = await con.QueryAsync<VillageModel>(query, new { talukaId });

                return result.ToList();

            }

        }
    }
}
