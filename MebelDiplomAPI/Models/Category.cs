using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("ParentCategoryId", Name = "IX_Categories_Parent")]
public partial class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string CategoryName { get; set; } = null!;

    [Column("ParentCategoryID")]
    public int? ParentCategoryId { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("ParentCategory")]
    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    [ForeignKey("ParentCategoryId")]
    [InverseProperty("InverseParentCategory")]
    public virtual Category? ParentCategory { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
