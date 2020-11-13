using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IFaceInfoRepository
    {
        Task createFaceInfo(FaceInfo faceInfo); 
    }
}
