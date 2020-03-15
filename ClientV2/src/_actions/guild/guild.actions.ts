
import { guildService, GuildInfo, GuildBalanceReport, GamerStatus, IGamersListView, GamerStatusList, GamerRank } from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance, ICallback } from '..';
import { gamerInstance } from '../gamer/gamer.actions';

interface GetGuildProps extends ICallback<any> {
    guildId: string;
}
interface AddUserProps extends ICallback<any> {
    guildId: string;
    id: string;
    name: string,
    rank: GamerRank,
    status: GamerStatus,
    dateOfBirth: Date,
    login: string
}
interface GetGuildBalanceProps extends ICallback<any> {
    guildId: string;
}

export class GuildActions {
    /**
     * Возвращает информацию о гильдии по её ID
     */
    GetGuild(guild: GetGuildProps) {
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
}

export const guildInstance = new GuildActions;