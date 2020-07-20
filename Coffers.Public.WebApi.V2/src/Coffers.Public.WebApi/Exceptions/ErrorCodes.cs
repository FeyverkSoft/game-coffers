using System;

namespace Coffers.Public.WebApi.Exceptions
{
    public static class ErrorCodes
    {
        public const String InternalServerError = "internal_server_error";
        public const String GuildNotFound = "guild_not_found";
        public const String GuildAlreadyExists = "guild_already_exists";
        public const String GamerAlreadyExists = "gamer_already_exists";
        public const String GamerNotFound = "gamer_not_found";
        public const String Forbidden = "forbidden";
        public const String Unauthorized = "unauthorized";
        public const String CharacterAlreadyExists = "character_already_exists";
        public const String PenaltyNotFound = "penalty_not_found";
        public const String LoanNotFound = "loan_not_found";
        public const String PenaltyAlreadyProcessing = "penalty_already_processing";
        public const String IncorrectOperation = "incorrect_operation";
        public const String OperationAlreadyExists = "operation_already_exists";
        public const String CharacterNotFound = "character_not_found";
        public const String PenaltyAlreadyExists = "penalty_already_exists";
        public const String LoanAlreadyExists = "loan_already_exists";
        public const String DocumentNotFound = "document_not_found";
        public const String OperationNotFound = "operation_not_found";
        public const String ContractAlreadyExists = "contract_already_exists";
        public const String NestNotFound = "nest_not_found";
        public const String ContractNotFound  = "contract_not_found";
    }
}