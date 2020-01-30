﻿using System;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanAlreadyExistsException : Exception
    {
        public Loan Detail { get; }
        public LoanAlreadyExistsException(Loan detail)
            : base("Loan already exists")
        {
            Detail = detail;
        }
    }
}
