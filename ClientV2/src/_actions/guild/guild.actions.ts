
import { guildService, GuildInfo, GuildBalanceReport, GamerStatus, IGamersListView, GamerStatusList, GamerRank, ITariff } from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance } from '..';
import { IDictionary } from '../../core';

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

    /**
     * Возвращает список тарифов гильдии
     */
    GetGuildTariffs(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.GetGuildTariffs()
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
        function request() { return { type: GuildActionsType.PROC_GET_TARIFFS } }
        function success(tariffs: Array<ITariff>) { return { type: GuildActionsType.SUCC_GET_TARIFFS, tariffs } }
        function failure() { return { type: GuildActionsType.FAILED_GET_TARIFFS } }
    }
}

export const guildInstance = new GuildActions();