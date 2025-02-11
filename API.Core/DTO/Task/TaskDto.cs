﻿using API.Core.DTO.User;

namespace API.Core.DTO.Task;

public class TaskDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsCompleted { get; set; }

    public BaseUserDto User { get; set; }
}
