using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [Column("DeliveryMethodID")]
    public int DeliveryMethodId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [Column("AddressID")]
    public int AddressId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(20)]
    public string? ContactPhone { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Orders")]
    public virtual UserAddress Address { get; set; } = null!;

    [ForeignKey("DeliveryMethodId")]
    [InverseProperty("Orders")]
    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Orders")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatus Status { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
