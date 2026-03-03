using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("CartId", "ProductId", Name = "UQ__CartItem__9AFC1BF8523CD30F", IsUnique = true)]
public partial class CartItem
{
    [Key]
    [Column("CartItemID")]
    public int CartItemId { get; set; }

    [Column("CartID")]
    public int CartId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartItems")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("CartItems")]
    public virtual Product Product { get; set; } = null!;
}
