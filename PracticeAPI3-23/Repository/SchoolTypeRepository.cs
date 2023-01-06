using Dapper;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;
using System.Data.Common;

namespace PracticeAPI3_23.Repository
{
    public class SchoolTypeRepository : BaseAsyncRepository, ISchoolTypeRepository
    {
        IConfiguration configuration;
        public SchoolTypeRepository(IConfiguration configuration) : base(configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<SchoolTypeModel>> GetAllSchoolType()
        {
            List<SchoolTypeModel> schoolType = new List<SchoolTypeModel>();
            var query = "select * from tblSchoolType";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var school = await dbConnection.QueryAsync<SchoolTypeModel>(query);
                schoolType = school.ToList();
                return schoolType;
            }
        }

        public async Task<SchoolTypeModel> GetAllSchoolTypeById(long Id)
        {
            SchoolTypeModel schoolType = null;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var schools = await dbConnection.QueryAsync<SchoolTypeModel>(@"select * from tblSchoolType where Id=@Id", new { Id });
                schoolType = schools.FirstOrDefault();
            }
            return schoolType;
        }

        public async Task<long> Add(SchoolTypeModel schoolType)
        {
            long result = 0;

            using (DbConnection dbConnection = sqlwriterConnection)
            {
                await dbConnection.OpenAsync();

                schoolType.CreatedDate = DateTime.Now;
                //schoolType.ModifiedDate = DateTime.Now;

                result = await dbConnection.QuerySingleAsync<long>(@"insert into tblSchoolType
                                          (SchoolType,M_SchoolType,CreatedBy,CreatedDate,IsDeleted,TimeStamp)
                                          values
                                          (@SchoolType,@M_SchoolType,@CreatedBy,@CreatedDate,@IsDeleted,
                                           @TimeStamp) 
                                            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", schoolType);
                return result;
            }
        }

        public async Task<long> Update(SchoolTypeModel schoolType)
        {
            int result = 0;

            schoolType.ModifiedDate = DateTime.Now;

            var query = @"update tblSchoolType set SchoolType=@SchoolType,M_SchoolType=@M_SchoolType,
                          modifiedBy=@modifiedBy,modifiedDate=@modifiedDate,TimeStamp=@TimeStamp
                          where Id=@Id and isDeleted=0";

            using (DbConnection dbConnection = sqlreaderConnection)
            {

                result = await dbConnection.ExecuteAsync(query, schoolType);

                return result;
            }
        }
        public async Task<long> Delete(DeleteUserObjects deleteUser)
        {

            int result = 0;

            var query = @"UPDATE tblSchoolType 
                        SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() 
                        WHERE Id = @Id";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                result = await dbConnection.ExecuteAsync(query, deleteUser);
                return result;
            }
        }      
      
    }
}
