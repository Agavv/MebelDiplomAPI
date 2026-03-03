using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

[Index("PermissionName", Name = "UQ__Permissi__0FFDA357970912AE", IsUnique = true)]
public partial class Permission
{
    [Key]
    [Column("PermissionID")]
    public int PermissionId { get; set; }

    [StringLength(100)]
    public string PermissionName { get; set; } = null!;

    [ForeignKey("PermissionId")]
    [InverseProperty("Permissions")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
