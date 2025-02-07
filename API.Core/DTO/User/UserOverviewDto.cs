namespace API.Core.DTO.User;

public class UserOverviewDto : BaseUserDto
{
    public int CompletedTaskCount { get; set; }

    public int TotalTaskCount { get; set; }
}
