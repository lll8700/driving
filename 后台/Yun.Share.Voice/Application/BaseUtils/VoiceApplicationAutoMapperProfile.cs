
using AutoMapper;
using Yun.Share.Voice.IApplication.Dtos;
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
            CreateMap<Practice, PracticeDto>(MemberList.None)
                .ForMember(x => x.ChoiceTyopeEnmName, s => s.Ignore())
                .ForMember(x => x.CarType, s=>s.Ignore())
                .ForMember(x => x.SubjectType, s => s.Ignore())
                .ForMember(x => x.PracticeImages, s => s.Ignore())
                .ForMember(x => x.Options, s => s.Ignore())
                ;
            CreateMap<SubjectType, SubjectTypeDto>(MemberList.Source);
            CreateMap<CarType, CarTypeDto>(MemberList.Source);
            CreateMap<PracticeImage, PracticeImageDto>(MemberList.None);
            CreateMap<User, UserDto>(MemberList.Source);
            CreateMap<Option, OptionDto>(MemberList.None);
            CreateMap<ErrorPracticeId, ErrorPracticeIdDto>(MemberList.None);
            CreateMap<ErrorPracticeLog, ErrorPracticeLogDto>(MemberList.None)
                   .ForMember(x => x.ErrorPracticeIdDtos, s => s.Ignore());
        }
    }
}
