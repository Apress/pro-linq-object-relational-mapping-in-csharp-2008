using System;
using System.Collections.Generic;
using BoP.Core.Domain;

namespace BoP.Core.DataInterfaces
{
    /// <summary>
    /// Since this extends the <see cref="IDao{TypeOfListItem, IdT}" /> behavior, it's a good idea to 
    /// place it in its own file for manageability.  In this way, it can grow further without
    /// cluttering up <see cref="IDaoFactory" />.
    /// </summary>
    public interface IOrderDao : IDao<Order, long>
    {
        List<Order> GetOrdersPlacedBetween(DateTime startDate, DateTime endDate);
    }
}
