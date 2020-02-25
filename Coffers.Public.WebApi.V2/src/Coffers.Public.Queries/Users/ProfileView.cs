using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Users
{
    public sealed class ProfileView
    {
        public Guid UserId { get; }
        public String Name { get; }
        public GamerRank Rank { get; }
        public Int32 CharCount { get; }
        public Decimal Balance { get; }
        public Decimal ActiveLoanAmount { get; }
        public Decimal ActivePenaltyAmount { get; }
        public Decimal ActiveExpLoanAmount { get; }
        public Decimal ActiveLoanTaxAmount { get; }
        public Decimal RepaymentLoanAmount { get; }
        public Decimal RepaymentTaxAmount { get; }

        public ProfileView(Guid userId, String name, GamerRank rank, Int32 charCount, Decimal balance,
            Decimal activeLoanAmount, Decimal activePenaltyAmount, Decimal activeExpLoanAmount,
            Decimal activeLoanTaxAmount, Decimal repaymentLoanAmount, Decimal repaymentTaxAmount)
        => (UserId, Name, Rank, CharCount, Balance,
                 ActiveLoanAmount, ActivePenaltyAmount, ActiveExpLoanAmount,
                 ActiveLoanTaxAmount, RepaymentLoanAmount, RepaymentTaxAmount)
            = (userId, name, rank, charCount, balance,
                activeLoanAmount, activePenaltyAmount, activeExpLoanAmount,
                activeLoanTaxAmount, repaymentLoanAmount, repaymentTaxAmount);
    }
}
