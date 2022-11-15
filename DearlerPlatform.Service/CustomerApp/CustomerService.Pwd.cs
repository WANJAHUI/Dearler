using System.Threading.Tasks;
using DearlerPlatform.Common.Md5Module;
using DearlerPlatform.Service.CustomerApp.Dto;

namespace DearlerPlatform.Service.CustomerApp
{
    public partial class CustomerService
    {
    
        public async Task<bool> CheckPassword(CustomerLoginDto dto){
            var res = await CustomerPwdRepo.GetAsync(m=>m.CustomerNo == dto.CustomerNo && m.CustomerPwd1 == dto.Password.ToMd5());
            if(res !=null) 
            {
                return true;
            }
            else{
                return false;
            }
        }
    }
}