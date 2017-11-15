using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterPreview.Base;
using WaterPreview.Other;
using WaterPreview.Redis;
using WaterPreview.Service;
using WaterPreview.Service.Interface;

namespace WaterPreview.Controllers
{
    public class AreaController:BaseController
    {
        private static IAreaService areaService;
        private static IFlowMeterService flowmeterService;


        public AreaController(IAreaService arService, IFlowMeterService fmService)
        {
            this.AddDisposableObject(arService);
            areaService = arService;
            this.AddDisposableObject(fmService);
            flowmeterService = fmService;
        }



        public JsonResult AreaTree()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Guid areauid = new Guid();
            if (UserContext.account.Usr_Type == 3)
            {
                areauid = UserContext.GetAreaByUserUid(UserContext.account.Usr_UId);
            }
            else
            {
                areauid = UserContext.areaSourceUid;
            }
            IAreaService area_service = new AreaService();
            List<Area_t> all = DBHelper.get<Area_t>(area_service.GetAllArea(),UserContext.allArea);

            Area_t area = all.First(p => p.Ara_UId == areauid);
            var list = new
            {
                text = area.Ara_Name,
                description = area.Ara_Description,
                id = area.Ara_UId,
                children = GetChild(area.Ara_UId, all)
            };
            result.Data = list;
            return result;
        }

        private dynamic GetChild(Guid uid, List<Area_t> all)
        {
            return all.Where(p => p.Ara_Up == uid)
                .OrderBy(p => p.Ara_Code).ToList().Select(p =>
                new
                {
                    text = p.Ara_Name,
                    description = p.Ara_Description,
                    //code=p.Ara_Short,
                    //isleaf = p.Ara_IsLeaf,
                    id = p.Ara_UId,
                    children = GetChild(p.Ara_UId, all)
                });
        }

    }
}