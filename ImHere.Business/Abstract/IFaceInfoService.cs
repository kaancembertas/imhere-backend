using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IFaceInfoService
    {
        Task CreateFaceInfo(int userId, string faceEncoding);
        Task<List<FaceInfo>> GetFaceInfos();
    }
}
