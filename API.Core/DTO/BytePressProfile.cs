using API.Core.DTO.Task;
using API.Core.DTO.User;
using AutoMapper;
using BytePress.Shared.Data.Domain;

namespace API.Core.DTO;

public class BytePressProfile : Profile
{
    public BytePressProfile()
    {
        CreateMap<ApplicationUser, BaseUserDto>()
            .ForMember(dto => dto.Role, opt => opt.MapFrom(au => au.UserRoles.First().Role.Name));

        CreateMap<ApplicationUser, UserOverviewDto>()
            .ForMember(dto => dto.TotalTaskCount, opt => opt.MapFrom(u => u.Tasks.Count))
            .ForMember(dto => dto.CompletedTaskCount, opt => opt.MapFrom(u => u.Tasks.Count(t => t.IsCompleted)));

        CreateMap<AddTaskDto, BytePress.Shared.Data.Domain.Task>();
        CreateMap<BytePress.Shared.Data.Domain.Task, TaskDto>();
    }
}
