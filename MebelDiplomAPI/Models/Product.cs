using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("CategoryId", Name = "IX_Products_CategoryID")]
[Index("Name", Name = "IX_Products_Name")]
[Index("Price", Name = "IX_Products_Price")]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    [StringLength(100)]
    public string? Material { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Width { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Height { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Depth { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Product")]
    public virtual ProductImage? ProductImage { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
