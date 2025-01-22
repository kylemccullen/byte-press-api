using API.Core.DTO.Task;
using AutoMapper;

namespace API.Core.DTO;

public class BytePressProfile : Profile
{
    public BytePressProfile()
    {
        CreateMap<AddTaskDto, BytePress.Shared.Data.Domain.Task>();
        CreateMap<BytePress.Shared.Data.Domain.Task, TaskDto>();
    }
}
