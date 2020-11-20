using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class FaceInfoRepository : IFaceInfoRepository
    {
        public async Task CreateFaceInfo(FaceInfo faceInfo)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                imHereDbContext.FaceInfos.Add(faceInfo);
                await imHereDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<FaceInfo>> GetFaceInfos()
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.FaceInfos.ToListAsync();
            }
        }
    }
}
