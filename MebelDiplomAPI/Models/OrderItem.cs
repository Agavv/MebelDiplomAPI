using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("OrderId", "ProductId", Name = "UQ__OrderIte__08D097C0D85EEF1A", IsUnique = true)]
public partial class OrderItem
{
    [Key]
    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PriceAtPurchase { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderItems")]
    public virtual Product Product { get; set; } = null!;
}
