using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("StatusName", Name = "UQ__OrderSta__05E7698A6AFB8436", IsUnique = true)]
public partial class OrderStatus
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    [InverseProperty("Status")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
