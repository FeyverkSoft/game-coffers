import { guildService} from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance } from '..';

/**
 * Попытка тут сделать саги
 */
export class GuildActions {
    /**
     * Возвращает информацию о гильдии
     */
    GetGuild() {
        return (dispatch: Function) => {
            dispatch(GuildActionsType.PROC_GET_GUILD());
            guildService.getGuild()
                .then(
                    data => {
                        dispatch(GuildActionsType.SUCC_GET_GUILD(data));
                    })
                .catch(
                    ex => {
                        dispatch(GuildActionsType.FAILED_GET_GUILD());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Возвращает информацию о балансе гильдии по ID
     */
    GetGuildBalanceReport(): Function {
        return (dispatch: Function) => {
            dispatch(GuildActionsType.PROC_GET_BALANCE_REPORT());
            guildService.GetGuildBalanceReport()
                .then(
                    data => {
                        dispatch(GuildActionsType.SUCC_GET_BALANCE_REPORT(data));
                    })
                .catch(
                    ex => {
                        dispatch(GuildActionsType.FAILED_GET_BALANCE_REPORT());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Возвращает список тарифов гильдии
     */
    GetGuildTariffs(): Function {
        return (dispatch: Function) => {
            dispatch(GuildActionsType.PROC_GET_TARIFFS());
            guildService.GetGuildTariffs()
                .then(
                    data => {
                        dispatch(GuildActionsType.SUCC_GET_TARIFFS(data));
                    })
                .catch(
                    ex => {
                        dispatch(GuildActionsType.FAILED_GET_TARIFFS());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }
}

export const guildInstance = new GuildActions();