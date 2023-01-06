using Dapper;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;
using System.Data.Common;

namespace PracticeAPI3_23.Repository
{
    public class SchoolCategoryRepository:BaseAsyncRepository,ISchoolCategoryRepository
    {
        IConfiguration configuration;
        public SchoolCategoryRepository(IConfiguration configuration):base(configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<SchoolCategoryModel>> GetAllSchoolCategory()
        {
            List<SchoolCategoryModel> schoolCategories = new List<SchoolCategoryModel>();
            var query = "select * from tblSchoolCategory";
            using(DbConnection dbConnection =sqlreaderConnection)
            {
                var school = await dbConnection.QueryAsync<SchoolCategoryModel>(query);
                schoolCategories=school.ToList();               
            }
            return schoolCategories;
        }

        public async Task<SchoolCategoryModel> GetSchoolCategoryById(int id)
        {
            SchoolCategoryModel schoolCategory = null;
            var query = "select * from tblSchoolCategory where Id=@Id";
            using (DbConnection dbConnection=sqlreaderConnection)
            {
                var school= await dbConnection.QueryAsync<SchoolCategoryModel>(query,new { id });
                schoolCategory=school.FirstOrDefault();               
            }
            return schoolCategory;
        }

        public async Task<long> Add(SchoolCategoryModel schoolCategory)
        {
          long result=0;
            schoolCategory.CreatedDate=DateTime.Now;

            var query = @"insert into tblSchoolCategory (CategoryName,M_CategoryName,CreatedBy,CreatedDate,IsDeleted,TimeStamp)
                       values 
                       (@CategoryName,@M_CategoryName,@CreatedBy,@CreatedDate,@IsDeleted,@TimeStamp) 
                       SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
          using (DbConnection dbConnection=sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                result = await dbConnection.QuerySingleAsync<long>(query, schoolCategory);              
            }
            return result;
        }

        public async Task<long> Update(SchoolCategoryModel schoolCategory)
        {
            long result = 0;

            schoolCategory.ModifiedDate = DateTime.Now;

            var query = @"update tblSchoolCategory
                         set CategoryName=@CategoryName,M_CategoryName=@M_CategoryName,
                         ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,TimeStamp=@TimeStamp
                         where Id=@Id and IsDeleted=0";

            using (DbConnection dbConnection = sqlreaderConnection)
            {

                result = await dbConnection.ExecuteAsync(query, schoolCategory);

                return result;
            }
        }

        public async Task<long> Delete(DeleteUserObjects deleteUser)
        {

            int result = 0;

            var query = @"SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() 
                          WHERE Id = @Id";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                result = await dbConnection.ExecuteAsync(query, deleteUser);
                return result;
            }
        }
             
    }
}
