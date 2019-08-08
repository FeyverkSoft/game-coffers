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

        /// <summary>
        /// Дата когда игрок удалился из гильдии
        /// </summary>
        public DateTime? DeletedDate { get; internal set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; internal set; }
        
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
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; set; }

        public List<Loan> Loans { get; set; }

        public List<Penalty> Penalties { get; set; }

        internal Gamer() { }

        /// <summary>
        /// Данный метод добавляет нового персонажа игроку, если такого не было
        /// </summary>
        /// <param name="name"></param>
        /// <param name="className"></param>
        public void AddCharacters(String name, String className)
        {
            if (Characters == null)
                Characters = new List<Character>();
            var _name = name.Trim();
            var _className = className.Trim();
            var ch = Characters.FirstOrDefault(x =>
                x.Name.Equals(_name, StringComparison.CurrentCultureIgnoreCase) &&
                x.ClassName.Equals(className, StringComparison.CurrentCultureIgnoreCase));
            if (ch != null)
            {
                ch.Status = CharStatus.Active;
                return;
            }

            //Если персонаж был ранее но с другим классом. То удаляем запись о старом.
            //Пока что так....
            if (Characters.Exists(x =>
                x.Name.Equals(_name, StringComparison.CurrentCultureIgnoreCase) &&
                !x.ClassName.Equals(_className, StringComparison.CurrentCultureIgnoreCase)))
                Characters.Remove(Characters.FirstOrDefault(_ =>
                    _.Name.Equals(_name, StringComparison.CurrentCultureIgnoreCase)));

            Characters.Add(new Character
            {
                Id = new Guid(),
                Status = CharStatus.Active,
                Name = _name,
                ClassName = _className
            });
            UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Устанавливает игроку новый статус
        /// </summary>
        /// <param name="status"></param>
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

        /// <summary>
        /// "Удаляет" персонажа у игрока, по факту только скрывает :)
        /// </summary>
        /// <param name="name"></param>
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

        /// <summary>
        /// Добавляет ноый штраф игроку, с указаными параметрами
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        public void AddPenalty(Guid id, Decimal amount, String description)
        {
            if (Penalties == null)
                Penalties = new List<Penalty>();

            if (Penalties.Any(x => x.Id == Id && x.Amount == amount))
                return;
            Penalties.Add(new Penalty(id, amount, description));
        }

        /// <summary>
        /// Добавляет игроку новый займ
        /// </summary>
        /// <param name="loan"></param>
        public void AddLoan(Loan loan)
        {
            if (Loans == null)
                Loans = new List<Loan>();

            if (Loans.Any(x => x.Id == loan.Id && x.Amount == loan.Amount))
                return;

            Loans.Add(loan);
        }

        /// <summary>
        /// Метод отменяет ещё не оплаченный штраф у игрока
        /// </summary>
        /// <param name="id"></param>
        public void CancelPenalty(Guid id)
        {
            if (Penalties == null)
                Penalties = new List<Penalty>();
            var p = Penalties.FirstOrDefault(_ => _.Id == id);

            if (p == null)
                throw new KeyNotFoundException(id.ToString());

            if (p.PenaltyStatus == PenaltyStatus.Active)
                p.PenaltyStatus = PenaltyStatus.Canceled;
        }

        /// <summary>
        /// Метод отменяет ещё неоплаченный займ у игрока
        /// </summary>
        /// <param name="id"></param>
        public void CancelLoan(Guid id)
        {
            if (Loans == null)
                Loans = new List<Loan>();

            var l = Loans.FirstOrDefault(_ => _.Id == id);

            if (l == null)
                throw new KeyNotFoundException(id.ToString());

            if (l.LoanStatus != LoanStatus.Paid)
                l.LoanStatus = LoanStatus.Canceled;
        }
    }
}
