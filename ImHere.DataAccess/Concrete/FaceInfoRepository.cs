using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<FaceInfo>> GetFaceInfos(string lectureCode)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                List<int> userIds = await (from userLectures in imHereDbContext.UserLectures
                                           where userLectures.lecture_code == lectureCode
                                           select userLectures.user_id).ToListAsync();

                return await imHereDbContext.FaceInfos
                    .Where(fi => userIds.Contains(fi.user_id))
                    .ToListAsync();
            }
        }
    }
}
