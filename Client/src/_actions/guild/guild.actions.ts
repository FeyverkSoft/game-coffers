
import { guildService, GuildInfo, GuildBalanceReport, GamerStatus, IGamersListView } from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance, ICallback } from '..';

interface GetGuildProps extends ICallback<any> {
    guildId: string;
}

interface GetGuildBalanceProps extends ICallback<any> {
    guildId: string;
}

interface GetGuildGamersProps extends ICallback<any> {
    guildId: string;
    dateFrom?:Date|string;
    dateTo?:Date|string;
    gamerStatuses?:Array<GamerStatus>
}

export class GuildActions {
    /**
     * Возвращает информацию о гильдии по её ID
     */
    GetGuild(guild: GetGuildProps): Function {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.getGuild(guild.guildId)
                .then(
                    data => {
                        dispatch(success(data));
                        if (guild.onSuccess)
                            guild.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                        if (guild.onFailure)
                            guild.onFailure(ex);
                    });
        }
        function request() { return { type: GuildActionsType.PROC_GET_GUILD } }
        function success(guildInfo: GuildInfo) { return { type: GuildActionsType.SUCC_GET_GUILD, guildInfo } }
        function failure() { return { type: GuildActionsType.FAILED_GET_GUILD } }
    }

    /**
     * Возвращает информацию о балансе гильдии по ID
     */
    GetGuildBalanceReport(guild: GetGuildBalanceProps): Function {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.GetGuildBalanceReport(guild.guildId)
                .then(
                    data => {
                        dispatch(success(data));
                        if (guild.onSuccess)
                            guild.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                        if (guild.onFailure)
                            guild.onFailure(ex);
                    });
        }
        function request() { return { type: GuildActionsType.PROC_GET_BALANCE_REPORT } }
        function success(BalanceInfo: GuildBalanceReport) { return { type: GuildActionsType.SUCC_GET_BALANCE_REPORT, BalanceInfo } }
        function failure() { return { type: GuildActionsType.FAILED_GET_BALANCE_REPORT } }
    }

     /**
     * This method return gamer list
     */
    GetGamers(filter: GetGuildGamersProps): Function {
        return (dispatch: Function) => {
            dispatch(request());
            guildService.GetGamers(filter.guildId)
                .then(
                    data => {
                        dispatch(success(data));
                        if (filter.onSuccess)
                        filter.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                        if (filter.onFailure)
                        filter.onFailure(ex);
                    });
        }
        function request() { return { type: GuildActionsType.PROC_GET_GUILD_GAMERS } }
        function success(GamersList: Array<IGamersListView>) { return { type: GuildActionsType.SUCC_GET_GUILD_GAMERS, GamersList } }
        function failure() { return { type: GuildActionsType.FAILED_GET_GUILD_GAMERS } }
    }
}

export const guildInstance = new GuildActions;