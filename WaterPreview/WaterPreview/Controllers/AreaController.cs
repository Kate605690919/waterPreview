using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private static IPressureMeterService pressuremeterService;
        private static IQualityMeterService qualitymeterService;


        public AreaController(IAreaService arService, IFlowMeterService fmService,IPressureMeterService pmService,IQualityMeterService qmService)
        {
            this.AddDisposableObject(arService);
            areaService = arService;
            this.AddDisposableObject(fmService);
            flowmeterService = fmService;

            this.AddDisposableObject(pmService);
            pressuremeterService = pmService;
            this.AddDisposableObject(qmService);
            qualitymeterService = qmService;
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
            Func<List<Area_t>> func = () =>area_service.GetAllArea().ToList();
            List<Area_t> all = DBHelper.get<Area_t>(func, UserContext.allArea);

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


        public JsonResult GetFlowMeterByAreaUid(Guid areaUid)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Func<List<object>> fmAndStatusArea = () => flowmeterService.GetFlowMeterStatusAndArea();
            List<object> fmstatusAndAreaList = DBHelper.get<object>(fmAndStatusArea,UserContext.allFlowMeterStatusAndArea);
            //result.Data = new
            //{
            //    type = "GetFlowMeterByAreaUid",
            //    data = fmstatusAndAreaList
            //};
            result.Data = fmstatusAndAreaList;
            return result;
        }

        public JsonResult GetPressureMeterByAreaUid(Guid areaUid)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Func<List<object>> pmAndStatusArea = () => pressuremeterService.GetPressureMeterStatusAndArea();
            List<object> pmstatusAndAreaList = DBHelper.get<object>(pmAndStatusArea, UserContext.allPressureMeterStatusAndArea);
            result.Data = pmstatusAndAreaList;
            //result.Data = new
            //{
            //    type = "GetPressureMeterByAreaUid",
            //    data = pmstatusAndAreaList
            //};
            return result;
        }

        public JsonResult GetQualityMeterByAreaUid(Guid areaUid)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Func<List<object>> qmAndStatusArea = () => qualitymeterService.GetQualityMeterStatusAndArea();
            List<object> qmstatusAndAreaList = DBHelper.get<object>(qmAndStatusArea, UserContext.allQualityMeterStatusAndArea);
            result.Data = qmstatusAndAreaList;
            //result.Data = new
            //{
            //    type = "GetQualityMeterByAreaUid",
            //    data = qmstatusAndAreaList
            //};
            return result;
        }
    }
}