using PracticeAPI3_23.Model;

namespace PracticeAPI3_23.Repository.Interface
{
    public interface ISchoolTypeRepository
    {
        public Task<List<SchoolTypeModel>> GetAllSchoolType();
        public Task<SchoolTypeModel> GetAllSchoolTypeById(long Id);    
        public Task<long> Add(SchoolTypeModel schoolType);
        public Task<long> Update(SchoolTypeModel schoolType);
        public Task<long> Delete(DeleteUserObjects deleteUser);
    }
}
