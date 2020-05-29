using AutoMapper;

namespace FundAnalysis.DTO
{
    public class MyProfile: Profile
    {
        public MyProfile()
        {
            CreateMap<FundDto, LookDto>();
        }
    }
}