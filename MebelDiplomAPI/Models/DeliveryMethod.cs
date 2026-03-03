using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("MethodName", Name = "UQ__Delivery__218CFB17637C7A3A", IsUnique = true)]
public partial class DeliveryMethod
{
    [Key]
    [Column("DeliveryMethodID")]
    public int DeliveryMethodId { get; set; }

    [StringLength(100)]
    public string MethodName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [InverseProperty("DeliveryMethod")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
