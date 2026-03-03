using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("Email", Name = "UQ__Users__A9D105342A62D825", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(150)]
    public string FullName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public string? AvatarUrl { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    public bool IsBlocked { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("User")]
    public virtual Cart? Cart { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<SystemLog> SystemLogs { get; set; } = new List<SystemLog>();

    [InverseProperty("User")]
    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
