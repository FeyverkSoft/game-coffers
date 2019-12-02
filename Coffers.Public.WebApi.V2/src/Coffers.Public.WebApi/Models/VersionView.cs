using System;

namespace Coffers.Public.WebApi.Models
{
    /// <summary>
    /// Возвращает версию сервиса
    /// </summary>
    public class VersionView
    {
        /// <summary>
        /// Текущая версия сервиса
        /// </summary>
        public String Version { get; set; }
    }
}
