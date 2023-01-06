using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;
using System.Data;
using System.Data.Common;
using static PracticeAPI3_23.Model.BaseModel;

namespace PracticeAPI3_23.Repository
{
    public class UserRegistrationRepository:BaseAsyncRepository,IUserRegistrationRepository
    {
        IConfiguration configuration;
        public UserRegistrationRepository(IConfiguration configuration):base(configuration)
        {
            this.configuration = configuration;
        }
               
        public async Task<BaseResponseModel> GetAllUsersByPagination(int pageno, int pagesize, string? TextSearch)
        {
            BaseResponseModel baseResponse = new BaseResponseModel();
            Pagination paginations = new Pagination();
            List<UserRegistrationPaginationModel> userlist = new List<UserRegistrationPaginationModel>();

            var query = @"select u.Id,u.Name,u.userName,u.mobileNo,u.emailId,

               u.password,u.districtId,d.districtName,u.talukaId,t.talukaName, u.vilageId,v.villageName,

               u.createdBy,u.createDate,u.modifiedBy,u.modifiedDate
              
               from  tblUserRegistration u 

              left join tblDistrict d on u.districtId=d.Id

              left join tblTaluka t on u.talukaId=t.Id

              left join tblVillage v on u.vilageId=v.Id
                      
              where (u.userName LIKE '%' + @textSearch + '%' or @textSearch='')  and u.isDeleted=0

              order By u.Id desc

              offset(@pageno - 1) * @pagesize rows fetch next @pagesize rows only; 

              select @pageno as PageNo, Count(distinct (u.Id)) as TotalPages 

              from  tblUserRegistration u 

              left join tblDistrict d on u.districtId=d.Id

              left join tblTaluka t on u.talukaId=t.Id 

              left join tblVillage v on u.vilageId=v.Id

              where (u.userName LIKE '%' + @textSearch + '%' or @textSearch='') and u.isDeleted=0";

            using (DbConnection dbConnection = sqlwriterConnection)
            {

                if (pageno == 0)
                {
                    pageno = 1;
                }
                if (pagesize == 0)
                {
                    pagesize = 10;
                }
                if (TextSearch == null)
                {
                    TextSearch = "";
                }

                var result = await dbConnection.QueryMultipleAsync(query, new { pageno = pageno, pagesize = pagesize, textSearch = TextSearch });
                var dataList = await result.ReadAsync<UserRegistrationPaginationModel>();
                var pagination = await result.ReadAsync<Pagination>();
                userlist = dataList.ToList();
                paginations = pagination.FirstOrDefault();
                int PageCount = 0;
                int last = 0;
                int cnt = 0;
                cnt = paginations.TotalPages;
                last = paginations.TotalPages % pagesize;
                PageCount = paginations.TotalPages / pagesize;
                paginations.TotalPages = PageCount;
                paginations.PageCount = cnt;
                if (last > 0)
                {
                    paginations.TotalPages = PageCount + 1;
                }
                baseResponse.ResponseData1 = userlist;
                baseResponse.ResponseData2 = paginations;
                return baseResponse;
            }
        }


        public async Task<int> AddUser(UserRegistrationModel userRegistration)
        {
            userRegistration.isDeleted = false;
            userRegistration.createDate = DateTime.Now;
            // userRegistration.modifiedDate = DateTime.Now;
            int result = 0;
            int resultResult = 0;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var MobileNoCondition = await dbConnection.QueryAsync
                    (@"select Id from tblUserRegistration where mobileNo=@mobileNo and isDeleted=0",
                    new { mobileNo = userRegistration.mobileNo });
                var FirstDocumentTypeById = MobileNoCondition.FirstOrDefault();
                if (FirstDocumentTypeById != null)
                {
                    return -1;
                }
                var EmailIdCondition = await dbConnection.QueryAsync
                   (@"select Id from tblUserRegistration where emailId=@emailId and isDeleted=0",
                   new { emailId = userRegistration.emailId });
                var SecondDocumentTypeById = EmailIdCondition.FirstOrDefault();
                if (SecondDocumentTypeById != null)
                {
                    return -2;
                }
                result = await dbConnection.QuerySingleAsync<int>(@" insert into tblUserRegistration (Name,userName,mobileNo,emailId,districtId,
                                                                talukaId,vilageId,password,createdBy,createDate,modifiedBy
                                                                ,modifiedDate,isDeleted)
                                                                values (@Name,@userName,@mobileNo,@emailId,@districtId,@talukaId,@vilageId,
                                                                @password,@createdBy,@createDate,@modifiedBy,@modifiedDate,@isDeleted);
                                                                SELECT CAST(SCOPE_IDENTITY() as bigint); ", userRegistration);
                if (result >= 1)
                {
                    resultResult = result;
                }
                return resultResult;
            }
        }
        public async Task<int> UpdateUser(UserRegistrationModel userRegistration)
        {
            int result = 0;

            userRegistration.modifiedDate = DateTime.Now;

            var query = @"update tblUserRegistration set Name=@Name,userName=@userName,mobileNo=@mobileNo,
                          emailId=@emailId,districtId=@districtId,talukaId=@talukaId,vilageId=@vilageId,
                          password=@password,modifiedBy=@modifiedBy,modifiedDate=@modifiedDate
                          where Id=@Id and isDeleted=0";

            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var MobileNoCondition = await dbConnection.QueryAsync
                    (@"select Id from tblUserRegistration where mobileNo=@mobileNo and isDeleted=0 and Id != @Id",
                    new { mobileNo = userRegistration.mobileNo, Id = userRegistration.Id });
                var FirstDocumentTypeById = MobileNoCondition.FirstOrDefault();
                if (FirstDocumentTypeById != null)
                {
                    return -1;
                }
                var EmailIdCondition = await dbConnection.QueryAsync
                   (@"select Id from tblUserRegistration where emailId=@emailId and isDeleted=0 and Id != @Id",
                   new { emailId = userRegistration.emailId,Id=userRegistration.Id });
                var SecondDocumentTypeById = EmailIdCondition.FirstOrDefault();
                if (SecondDocumentTypeById != null)
                {
                    return -2;
                }
                result = await dbConnection.ExecuteAsync(query, userRegistration);

                return result;
            }
        }

        public async Task<int> DeleteUser(DeleteUserObject deleteUser)
        {
            int result = 0;

            var query = @"UPDATE tblUserRegistration 
                        SET IsDeleted = 1, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() 
                        WHERE Id = @Id AND IsDeleted = 0";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                result = await dbConnection.ExecuteAsync(query, deleteUser);
                return result;
            }
        }


    }
}

