using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Infrastructure.Data.ReadModels;

public class DashboardOrderReadModel
{
    public Guid OrderId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int ItemCount { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
