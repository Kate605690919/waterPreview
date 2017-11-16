﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterPreview.Service.Base;
using WaterPreview.Service.Service;

namespace WaterPreview.Service.Interface
{
    public class AreaService:BaseService<Area_t>,IAreaService
    {
        public Area_t GetAreaByUserUid(Guid useruid)
        {
            IAreaUserService areauser_service = new AreaUserService();
            AreaUser_t areauser = areauser_service.GetAllAreaUser().Where(p => p.AU_UserUId == useruid).SingleOrDefault();
            return FindAll().Where(p => p.Ara_UId == areauser.AU_AreaUId).SingleOrDefault();
        }

        public IEnumerable<Area_t> GetAllArea()
        {
            return FindAll();
        }
    }
}
