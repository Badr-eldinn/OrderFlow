using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Application.Interfaces;

public interface IDashboardReadModelService
{
    Task RefreshAsync(
        CancellationToken cancellationToken);
}
