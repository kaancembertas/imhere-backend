using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IFaceInfoService
    {
        Task createFaceInfo(int userId, string faceEncoding);
    }
}
