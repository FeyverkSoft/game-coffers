using System;
using Coffers.Types.Guilds;

namespace Coffers.Public.Queries.Guilds
{
    public class GuildView
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Название гильдии
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; set; }

        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; set; }

        /// <summary>
        /// Игроков на текущий момент в гильдии
        /// </summary>
        public Int32 GamersCount { get; set; }

        /// <summary>
        /// Персонажей в гильдии на текущий момент
        /// </summary>
        public Int32 CharactersCount { get; set; }

        /// <summary>
        /// Баланс гильдии
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Список тарифов гильдии
        /// </summary>
        public TariffsView Tariffs { get; set; }
    }

    public class TariffsView
    {
        /// <summary>
        /// Тариф для главы
        /// </summary>
        public TariffView Leader { get; set; }
        /// <summary>
        /// Тариф для офицера
        /// </summary>
        public TariffView Officer { get; set; }
        /// <summary>
        /// Тариф для ветерана
        /// </summary>
        public TariffView Veteran { get; set; }
        /// <summary>
        /// Тариф для солдата
        /// </summary>
        public TariffView Soldier { get; set; }
        /// <summary>
        /// Тариф для духа
        /// </summary>
        public TariffView Beginner { get; set; }
    }

    public class TariffView
    {
        /// <summary>
        /// Стоимость займа за 1 день в процентах
        /// </summary>
        public Decimal LoanTax { get; set; }
        /// <summary>
        /// Стоимость просрочки займа, за один день в процентах
        /// </summary>
        public Decimal ExpiredLoanTax { get; set; }
        /// <summary>
        /// Налог с 1го персонажа.
        /// Список. 1 персонаж, 2 персонажа, 3 итд.
        /// </summary>
        public Decimal[] Tax { get; set; }
    }
}
