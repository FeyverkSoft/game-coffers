
import { guildService, GuildInfo, GuildBalanceReport, GamerStatus, IGamersListView, GamerStatusList, GamerRank } from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance } from '..';

export class GuildActions {
    /**
     * Возвращает информацию о гильдии
     */
    GetGuild() {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.getGuild()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: GuildActionsType.PROC_GET_GUILD } }
        function success(guildInfo: GuildInfo) { return { type: GuildActionsType.SUCC_GET_GUILD, guildInfo } }
        function failure() { return { type: GuildActionsType.FAILED_GET_GUILD } }
    }

    /**
     * Возвращает информацию о балансе гильдии по ID
     */
    GetGuildBalanceReport(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.GetGuildBalanceReport()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: GuildActionsType.PROC_GET_BALANCE_REPORT } }
        function success(BalanceInfo: GuildBalanceReport) { return { type: GuildActionsType.SUCC_GET_BALANCE_REPORT, BalanceInfo } }
        function failure() { return { type: GuildActionsType.FAILED_GET_BALANCE_REPORT } }
    }
}

export const guildInstance = new GuildActions();