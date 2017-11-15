using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterPreview.Service.Base;
using WaterPreview.Service.Interface;

namespace WaterPreview.Service.Service
{
    public class AccountService : BaseService<User_t>,IAccountService
    {
        public bool GetAccount()
        {
            FindAll();
            return true;
        }

    }
}
