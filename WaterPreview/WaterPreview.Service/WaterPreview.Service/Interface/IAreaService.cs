using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterPreview.Service.Interface
{
    public interface IAreaService
    {
        Area_t GetAreaByUserUid(Guid useruid);

        List<Area_t> GetAllArea();
    }

}
