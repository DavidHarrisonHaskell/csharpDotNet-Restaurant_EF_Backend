using Microsoft.EntityFrameworkCore;

namespace Talent;

public class OrderService : IDisposable
{
    private readonly RestaurantContext _db;

    public OrderService(RestaurantContext db)
    {
        _db = db;
    }

    public List<Order> GetAllOrders()
    {
        return _db.Orders.Include(d => d.Dish).AsNoTracking().ToList();
    }

    public Order? GetOneOrder(int id)
    {
        Order? order = _db.Orders.Include(d => d.Dish).AsNoTracking().SingleOrDefault(o => o.OrderId == id);
        return order;
    }

    public Order AddOrder(Order order)
    {
        _db.Orders.Add(order);
        _db.SaveChanges();
        return order;
    }

    public bool DeleteOrder(int id)
    {
        Order? dbOrder = GetOneOrder(id);
        if (dbOrder == null) return false;
        _db.Orders.Remove(dbOrder);
        _db.SaveChanges();
        return true;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
