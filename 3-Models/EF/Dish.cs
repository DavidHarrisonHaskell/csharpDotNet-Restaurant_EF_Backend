using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public partial class Dish
{
    [Key]
    public int DishId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(50)]
    public string? ImageName { get; set; }

    [InverseProperty("Dish")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
