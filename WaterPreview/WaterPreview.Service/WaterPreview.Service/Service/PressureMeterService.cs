using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterPreview.Service.Base;
using WaterPreview.Service.Interface;

namespace WaterPreview.Service.Service
{
    public class PressureMeterService:BaseService<PressureMeter_t>,IPressureMeterService
    {
        public IEnumerable<PressureMeter_t> GetAllPressureMeter()
        {
            return FindAll();
        }

        public List<Object> GetPressureMeterStatusAndArea()
        {
            IPressureMeterStatusService pms_service = new PressureMeterStatusService();
            IAreaService area_service = new AreaService();
            List<Object> pmsalist = new List<object>();
            List<PressureMeter_t> pmlist = FindAll();
            foreach (var pmsa_item in pmlist)
            {
                object item = new
                {
                    pressuremeter = FindAll().Where(p => p.PM_UId == pmsa_item.PM_UId),
                    status = pms_service.GetPressureMeterStatusByUid(pmsa_item.PM_UId),
                    area = area_service.GetAreaByDeviceUid(pmsa_item.PM_UId)
                };
                pmsalist.Add(item);
            }
            return pmsalist;
        }
    }
}
