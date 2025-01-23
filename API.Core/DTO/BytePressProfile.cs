using API.Core.DTO.Task;
using API.Core.DTO.User;
using AutoMapper;
using BytePress.Shared.Data.Domain;

namespace API.Core.DTO;

public class BytePressProfile : Profile
{
    public BytePressProfile()
    {
        CreateMap<ApplicationUser, BaseUserDto>();

        CreateMap<AddTaskDto, BytePress.Shared.Data.Domain.Task>();
        CreateMap<BytePress.Shared.Data.Domain.Task, TaskDto>();
    }
}
