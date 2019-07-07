using System;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class TokenView
    {
        /// <summary>
        /// Авторизационный токен
        /// </summary>
        public Guid Token { get; set; }
        /// <summary>
        /// Доступные роли у пользователя
        /// </summary>
        public String[] Roles { get; set; }
        /// <summary>
        /// Гильдия которой принадлежит игрок
        /// </summary>
        public Guid GuildId { get; set; }
    }
}