﻿using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Loans.Entity
{
    internal sealed class LoanView
    {
        public static readonly String Sql = @"
select 
    l.Id,
    l.Description,
    l.LoanStatus as Status,
    l.CreateDate,
    l.Amount,
    coalesce(sum(o.Amount), 0.0) as Balance,
    l.UserId,
    l.ExpiredDate
from `Loan`l
left join `Operation` o on o.`DocumentId` = l.`Id` and o.`Type` = 'Loan'
where 1 = 1
    and l.Id = @LoanId
";

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTime ExpiredDate { get; }
        public DateTime CreateDate { get; }
        public LoanStatus Status { get; }
        public Decimal Amount { get; }
        public Decimal Balance { get; }
        public String Description { get; }
    }
}
