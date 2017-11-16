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
    }
}
