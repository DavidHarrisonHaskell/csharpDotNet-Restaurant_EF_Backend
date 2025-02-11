using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ModelMetadataType(typeof(DishMetaData))]
public partial class Dish
{

    [JsonIgnore] // don't return back to frontend in the url
    [NotMapped]// not related to the database
    public IFormFile? Image { get; set; } // Image File

    public string? ImageUrl
    {
        get
        {
            return ImageName == null ? null : AppConfig.ImageUrl + ImageName;
        }
    } // Image url (only want to return this to the user, not the file name)

}

public class DishMetaData
{
    [Required(ErrorMessage = "Missing Name.")]
    [MaxLength(50, ErrorMessage = "Name can't exceed 50 chars.")]
    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "Missing Description.")]
    [MaxLength(100, ErrorMessage = "Description can't exceed 100 chars.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Missing Price.")]
    [Range(1,1000, ErrorMessage = "Price must be in between 1 and 1000.")]
    public decimal Price { get; set; }

    [MaxLength(50, ErrorMessage = "ImageName can't exceed 50 chars.")]
    [JsonIgnore]
    public string? ImageName { get; set; }

}
