﻿namespace Coffers.Types.Account
{
    /// <summary>
    /// Тип доступных операций
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Налог
        /// </summary>
        Tax,
        /// <summary>
        /// Продажа в пользу ги
        /// </summary>
        Sell,
        /// <summary>
        /// Штраф
        /// </summary>
        Penalty,
        /// <summary>
        /// Займ
        /// </summary>
        Loan,
        /// <summary>
        /// Обмен между своими персонажами
        /// </summary>
        Exchange,
        /// <summary>
        /// Эмиссия игровых средств в систему
        /// </summary>
        Emission,
        /// <summary>
        /// Операция вывод средств из гильдии во внешнюю систему в пользу игрока
        /// Через зачисление на гильдийский счёт, и последуюещем автоматическим исписанием с него
        /// В пользувнещней системы
        /// </summary>
        Output,
        /// <summary>
        /// Операция вывод средств из гильдии во внешнюю систему в пользу игрока
        /// Через зачисление на гильдийский счёт, и последуюещем автоматическим исписанием с него
        /// В пользувнещней системы
        /// </summary>
        InternalOutput,
        /// <summary>
        /// Списание игровых средств в пользу гильдии
        /// с личного счёта игрока
        /// </summary>
        InternalEmission,
        /// <summary>
        /// Иное
        /// </summary>
        Other,
        /// <summary>
        /// Сделка между двумя игроками через склад гильдии
        /// </summary>
        Deal
    }
}
