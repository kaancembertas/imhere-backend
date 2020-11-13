using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class FaceInfoRepository : IFaceInfoRepository
    {
        public async Task createFaceInfo(FaceInfo faceInfo)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                imHereDbContext.FaceInfos.Add(faceInfo);
                await imHereDbContext.SaveChangesAsync();
            }
        }
    }
}
