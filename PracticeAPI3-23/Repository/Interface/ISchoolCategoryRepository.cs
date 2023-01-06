using PracticeAPI3_23.Model;

namespace PracticeAPI3_23.Repository.Interface
{
    public interface ISchoolCategoryRepository
    {
        public Task<List<SchoolCategoryModel>> GetAllSchoolCategory();
        public Task<SchoolCategoryModel> GetSchoolCategoryById(int id);
        public Task<long> Add(SchoolCategoryModel schoolCategory);
        public Task<long> Update(SchoolCategoryModel schoolCategory);
        public Task<long> Delete(DeleteUserObjects deleteUser);
    }
}
