using System;
using System.Collections.Generic;

namespace ServerSideAPI.Model.SituationTb;

public partial class SituationTb
{
    public string Id { get; set; } = null!;

    public string? IconId { get; set; }

    public string? Message { get; set; }

    public string? MessageCode { get; set; }

    public string? MessageType { get; set; }

    public Boolean? Arkiverad { get; set; }

    public DateTime? CreationTime { get; set; }
}