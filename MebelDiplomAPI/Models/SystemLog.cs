using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("LogDate", Name = "IX_SystemLogs_LogDate")]
[Index("UserId", Name = "IX_SystemLogs_UserID")]
public partial class SystemLog
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    [StringLength(50)]
    public string? EntityType { get; set; }

    [Column("EntityID")]
    public int? EntityId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SystemLogs")]
    public virtual User? User { get; set; }
}
