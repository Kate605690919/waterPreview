using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterPreview.Service.Base;
using WaterPreview.Service.Interface;

namespace WaterPreview.Service.Service
{
    public class FlowMeterService:BaseService<FlowMeter_t>,IFlowMeterService
    {
        public IEnumerable<FlowMeter_t> GetAllFlowMeter()
        {
            return FindAll();
        }

        public List<Object> GetFlowMeterStatusAndArea()
        {
            IFlowMeterStatusService fms_service = new FlowMeterStatusService();
            IAreaService area_service = new AreaService();
            List<Object> fmsalist = new List<object>();
            List<FlowMeter_t> fmlist = FindAll();
            foreach(var fmsa_item in fmlist){
                object item = new
                {
                    flowmeter = FindAll().Where(p=>p.FM_UId==fmsa_item.FM_UId),
                    status = fms_service.GetFlowMeterStatusByUid(fmsa_item.FM_UId),
                    area = area_service.GetAreaByDeviceUid(fmsa_item.FM_UId)
                };
                fmsalist.Add(item);
            }
            return fmsalist;
        }
    }
}
