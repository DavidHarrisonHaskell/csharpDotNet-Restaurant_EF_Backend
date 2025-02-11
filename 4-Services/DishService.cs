using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Talent;

public class DishService : IDisposable
{
    private readonly RestaurantContext _db;

    public DishService(RestaurantContext db)
    {
        _db = db;
    }

    public List<Dish> GetAllDishes()
    {
        return _db.Dishes.Include(o => o.Orders).AsNoTracking().ToList();
    }

    public Dish? GetOneDish(int id)
    {
        Dish? dish = _db.Dishes.Include(o => o.Orders).AsNoTracking().SingleOrDefault(d => d.DishId == id);
        return dish;
    }

    public Dish AddDish(Dish dish)
    {
        if (dish.Image != null) dish.ImageName = ImageHelper.SaveImage(dish.Image);
        _db.Dishes.Add(dish);
        _db.SaveChanges();
        return dish;
    }

    public Dish? UpdateFullDish(Dish dish)
    {
        Dish? dbDish = GetOneDish(dish.DishId);
        if (dbDish == null) return null;
        if (dish.Image != null) dish.ImageName = ImageHelper.UpdateImage(dish.Image, dbDish.ImageName);
        _db.Dishes.Attach(dish);
        _db.Entry(dish).State = EntityState.Modified;
        _db.SaveChanges();
        return dish;
    }

    public bool DeleteDish(int id)
    {
        Dish? dbDish = GetOneDish(id);
        if (dbDish == null) return false;
        ImageHelper.DeleteImage(dbDish.ImageName);
        _db.Dishes.Remove(dbDish);
        _db.SaveChanges();
        return true;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
