using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterPreview.Service.Base;
using WaterPreview.Service.Interface;

namespace WaterPreview.Service.Service
{
    public class QualityMeterService:BaseService<QualityMeter_t>,IQualityMeterService
    {
        public List<Object> GetQualityMeterStatusAndArea()
        {
            IQualityMeterStatusService pms_service = new QualityMeterStatusService();
            IAreaService area_service = new AreaService();
            List<Object> qmsalist = new List<object>();
            List<QualityMeter_t> qmlist = FindAll();
            foreach (var qmsa_item in qmlist)
            {
                object item = new
                {
                    qualitymeter = FindAll().Where(p => p.QM_UId == qmsa_item.QM_UId),
                    status = pms_service.GetPressureMeterStatusByUid(qmsa_item.QM_UId),
                    area = area_service.GetAreaByDeviceUid(qmsa_item.QM_UId)
                };
                qmsalist.Add(item);
            }
            return qmsalist;
        }
    }
}
