using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("UserId", "ProductId", Name = "UQ__Reviews__DCC800C37D4C2582", IsUnique = true)]
public partial class Review
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public bool IsModerated { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Reviews")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User User { get; set; } = null!;
}
