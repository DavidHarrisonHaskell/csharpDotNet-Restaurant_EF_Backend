using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ModelMetadataType(typeof(OrderMetaData))]
public partial class Order
{

}

public class OrderMetaData
{

    [Required(ErrorMessage = "Missing DishId.")]
    public int DishId { get; set; }

}