using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Table("OrderStatusHistory")]
public partial class OrderStatusHistory
{
    [Key]
    [Column("HistoryID")]
    public int HistoryId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ChangedAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderStatusHistories")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("OrderStatusHistories")]
    public virtual OrderStatus Status { get; set; } = null!;
}
