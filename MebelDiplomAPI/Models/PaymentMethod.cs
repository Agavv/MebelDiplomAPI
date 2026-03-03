using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("MethodName", Name = "UQ__PaymentM__218CFB17C6DFBECF", IsUnique = true)]
public partial class PaymentMethod
{
    [Key]
    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [StringLength(100)]
    public string MethodName { get; set; } = null!;

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
