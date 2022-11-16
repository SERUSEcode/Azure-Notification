using System;
using System.Collections.Generic;

namespace ServerSideAPI.Models;

public partial class SituationTb
{
    public string Id { get; set; } = null!;

    public string? IconId { get; set; }

    public string? Message { get; set; }

    public string? MessageCode { get; set; }

    public string? MessageType { get; set; }

    public DateTime? CreationTime { get; set; }
}
