using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid GuildId { get; internal set; }

        public DateTime CreateDate { get; internal set; }

        /// <summary>
        /// Дата когда игрок удалился из гильдии
        /// </summary>
        public DateTime? DeletedDate { get; internal set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; internal set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; internal set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; internal set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; internal set; }
        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; internal set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; internal set; }
        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; set; }

        public List<Loan> Loans { get; set; }

        public List<Penalty> Penalties { get; set; }

        internal Gamer() { }

        public void AddCharacters(String name, String className)
        {
            if (Characters == null)
                Characters = new List<Character>();

            var ch = Characters.FirstOrDefault(x =>
                x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) &&
                x.ClassName.Equals(className, StringComparison.CurrentCultureIgnoreCase));
            if (ch != null)
            {
                ch.Status = CharStatus.Active;
                return;
            }

            if (Characters.Exists(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && !x.ClassName.Equals(className, StringComparison.CurrentCultureIgnoreCase)))
                throw new ArgumentException($"Character {name} already exists");

            Characters.Add(new Character
            {
                Id = new Guid(),
                Status = CharStatus.Active,
                Name = name,
                ClassName = className
            });
            UpdateDate = DateTime.UtcNow;
        }

        public void SetStatus(GamerStatus status)
        {
            if (Status != status)
            {
                Status = status;
                UpdateDate = DateTime.UtcNow;
                if (status == GamerStatus.Banned || status == GamerStatus.Left)
                    DeletedDate = DateTime.UtcNow;
                else
                    DeletedDate = null;
            }
        }

        public void DeleteCharacter(String name)
        {
            if (Characters == null)
                Characters = new List<Character>();

            var ch = Characters.FirstOrDefault(x =>
                x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (ch != null)
            {
                ch.Status = CharStatus.Deleted;
            }
        }

        public void SetRank(GamerRank bindingRank)
        {
            Rank = bindingRank;
        }

        public void AddPenalty(Guid id, Decimal amount, String description)
        {
            if (Penalties == null)
                Penalties = new List<Penalty>();

            if (Penalties.Any(x => x.Id == Id && x.Amount == amount)) 
                return;
            Penalties.Add(new Penalty(id, amount, description));
        }

        public void AddLoan(Loan loan)
        {
            if (Loans == null)
                Loans = new List<Loan>();

            if (Penalties.Any(x => x.Id == loan.Id && x.Amount == loan.Amount))
                return;
            Loans.Add(loan);
        }
    }
}
