using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

public partial class UserAddress
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Country { get; set; } = null!;

    [StringLength(100)]
    public string City { get; set; } = null!;

    [StringLength(200)]
    public string Street { get; set; } = null!;

    [StringLength(20)]
    public string? PostalCode { get; set; }

    public bool? IsPrimary { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("UserAddresses")]
    public virtual User User { get; set; } = null!;
}
