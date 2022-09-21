
using AutoMapper;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.Models.Entities;

namespace Yun.Share.Voice
{
    public class VoiceApplicationAutoMapperProfile : Profile
    {
        // Map映射将写在这里
        public VoiceApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Practice, PracticeDto>(MemberList.Source);
                //.ForMember(x => x.CarType, s=>s.Ignore());
            CreateMap<SubjectType, SubjectTypeDto>(MemberList.Source);
            CreateMap<CarType, CarTypeDto>(MemberList.Source);
            CreateMap<PracticeImage, PracticeImageDto>(MemberList.Source);

        }
    }
}
