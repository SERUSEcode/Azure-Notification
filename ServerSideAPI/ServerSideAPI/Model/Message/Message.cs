using System;
using System.Collections.Generic;

namespace ServerSideAPI.Model.Message;

public partial class Message
{
    public string Id { get; set; }

	public string? SituationId { get; set; }

    public string? UserId { get; set; }

    public string? MessageText { get; set; }

    public string? MessageType { get; set; }

    public DateTime? CreationTime { get; set; }
}
