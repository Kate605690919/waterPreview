﻿using System;
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
    }
}
