using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ApiController]
public class DishController : ControllerBase, IDisposable
{
    private readonly DishService _dishService;

    public DishController(DishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet("api/dishes")]
    public IActionResult GetAllDishes()
    {
        List<Dish> dishes = _dishService.GetAllDishes();
        return Ok(dishes);
    }

    [HttpGet("api/dishes/{id}")]
    public IActionResult GetOneDish([FromRoute] int id)
    {
        Dish? dbDish = _dishService.GetOneDish(id);
        if (dbDish == null) return NotFound(new ResourceNotFound(id));
        return Ok(dbDish);
    }


    [HttpGet("api/dishes/images/{imageName}")]
    public IActionResult GetImage([FromRoute] string imageName)
    {
        byte[] imageBytes = ImageHelper.GetImageBytes(imageName);
        return File(imageBytes, "image/jpeg");
    }

    [Authorize(Roles = "Admin,Cheff")]
    [HttpPost("api/dishes")]
    public IActionResult AddProduct([FromForm] Dish dish)
    {
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError()));
        Dish dbDish = _dishService.AddDish(dish!);
        return Created("api/dishes/" + dbDish.DishId, dbDish);
    }

    [Authorize(Roles = "Admin,Cheff")]
    [HttpPut("api/dishes/{id}")]
    public IActionResult UpdateFullDish([FromRoute] int id, [FromForm] Dish dish)
    {
        dish.DishId = id;
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetFirstError())); // custom Validation Error extensions
        Dish? dbDish = _dishService.UpdateFullDish(dish);
        if (dbDish == null) return NotFound(new ResourceNotFound(id));
        return Ok(dbDish);
    }

    [Authorize(Roles = "Admin,Cheff")]
    [HttpDelete("api/dishes/{id}")]
    public IActionResult DeleteDish([FromRoute] int id)
    {
        bool success = _dishService.DeleteDish(id);
        if (!success) return NotFound(new ResourceNotFound(id));
        return NoContent();
    }
    
    public void Dispose()
    {
        
    }
}
