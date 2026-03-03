using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

public partial class ProductImage
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsPrimary { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImage")]
    public virtual Product Product { get; set; } = null!;
}
