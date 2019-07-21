﻿using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {

        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; internal set; }

        public Guid TariffId { get; internal set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; internal set; }

        /// <summary>
        /// Дата создания займа
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; internal set; }
        /// <summary>
        /// Уже было выплаченно в пользу займа
        /// </summary>
        public Decimal RepaymentAmount { get; internal set; }
        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; internal set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; internal set; }

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; internal set; }
        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; internal set; }

        public LoanStatus LoanStatus { get; internal set; }


        public Loan(Guid id, Guid tariffId, Decimal amount, Decimal taxAmount,
            String description, DateTime borrowDate, DateTime expiredDate)
        {
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            LoanStatus = LoanStatus.Active;
            Id = id;
            TariffId = tariffId;
            Amount = amount;
            TaxAmount = taxAmount;
            Description = description;
            BorrowDate = borrowDate;
            ExpiredDate = expiredDate;
        }
    }


}