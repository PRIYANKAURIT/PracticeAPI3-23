using PracticeAPI3_23.Model;

namespace PracticeAPI3_23.Repository.Interface
{
    public interface IUserRegistrationRepository
    {
        public Task<int> AddUser(UserRegistrationModel userRegistration);
        public Task<int> UpdateUser(UserRegistrationModel userRegistration);
        public Task<int> DeleteUser(DeleteUserObject deleteUser);
        public Task<BaseResponseModel> GetAllUsersByPagination(int pageno, int pagesize, string? TextSearch);
    }
}
