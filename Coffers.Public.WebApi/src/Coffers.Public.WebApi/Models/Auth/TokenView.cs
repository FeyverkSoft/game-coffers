using System;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class TokenView
    {
        /// <summary>
        /// Авторизационный токен
        /// </summary>
        public Guid Token { get; set; }
    }
}