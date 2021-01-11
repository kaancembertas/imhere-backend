// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IFaceInfoRepository
    {
        Task CreateFaceInfo(FaceInfo faceInfo);
        Task<List<FaceInfo>> GetFaceInfos(string lectureCode);
    }
}
