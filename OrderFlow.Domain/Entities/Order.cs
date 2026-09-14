using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }

    public string CustomerName { get; private set; }

    public decimal Total { get; private set; }

    public string Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public List<OrderItem> Items { get; private set; }

    private Order()
    {
        Items = new List<OrderItem>();
    }

    public Order(string customerName, List<OrderItem> items)
    {
        Id = Guid.NewGuid();

        CustomerName = customerName;

        Items = items;

        Total = Items.Sum(x => x.Quantity * x.UnitPrice);

        Status = "Pending";

        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = "Completed";
    }
}