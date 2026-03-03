using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("RoleName", Name = "UQ__Roles__8A2B616005E05E14", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
