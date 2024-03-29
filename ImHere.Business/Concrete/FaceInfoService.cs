﻿// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.DataAccess.Concrete;
using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Concrete
{
    public class FaceInfoService : IFaceInfoService
    {
        private IFaceInfoRepository _faceInfoRepository;

        public FaceInfoService(IFaceInfoRepository faceInfoRepository)
        {
            _faceInfoRepository = faceInfoRepository;
        }

        public async Task CreateFaceInfo(int userId, string faceEncoding)
        {
            FaceInfo faceInfo = new FaceInfo();
            faceInfo.user_id = userId;
            faceInfo.face_encoding = faceEncoding;
            await _faceInfoRepository.CreateFaceInfo(faceInfo);
        }

        public async Task<List<FaceInfo>> GetFaceInfos(string lectureCode)
        {
            return await _faceInfoRepository.GetFaceInfos(lectureCode);
        }
    }
}
