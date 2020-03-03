using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Users
{
    public sealed class ProfileView
    {
        public Guid UserId { get; }
        public String Name { get; }
        public GamerRank Rank { get; }
        public String CharacterName { get; }
        public Int32 CharCount { get; }
        public Decimal Balance { get; }
        public Decimal ActiveLoanAmount { get; }
        public Decimal ActivePenaltyAmount { get; }
        public Decimal ActiveLoanTaxAmount { get; }
        public DateTime DateOfBirth { get; }

        public ProfileView(Guid userId, String name, GamerRank rank, String characterName, Int32 charCount, Decimal balance,
            Decimal activeLoanAmount, Decimal activePenaltyAmount, Decimal activeLoanTaxAmount, DateTime dateOfBirth)
        => (UserId, Name, Rank, CharacterName, CharCount, Balance,
                 ActiveLoanAmount, ActivePenaltyAmount, ActiveLoanTaxAmount, DateOfBirth)
            = (userId, name, rank, characterName, charCount, balance,
                activeLoanAmount, activePenaltyAmount, activeLoanTaxAmount, dateOfBirth);
    }
}
