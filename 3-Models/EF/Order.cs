using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int? DishId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderTime { get; set; }

    [ForeignKey("DishId")]
    [InverseProperty("Orders")]
    public virtual Dish? Dish { get; set; }
}
