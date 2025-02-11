﻿using System.ComponentModel.DataAnnotations;

namespace BytePress.Shared.Data;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
