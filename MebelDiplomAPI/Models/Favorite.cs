using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("UserId", "ProductId", Name = "UQ__Favorite__DCC800C361D5B0C1", IsUnique = true)]
public partial class Favorite
{
    [Key]
    [Column("FavoriteID")]
    public int FavoriteId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Favorites")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Favorites")]
    public virtual User User { get; set; } = null!;
}
