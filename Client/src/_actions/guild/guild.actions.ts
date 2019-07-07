
import { guildService, GuildInfo } from '../../_services';
import { GuildActionsType } from './GuildActionsType';
import { alertInstance, ICallback } from '..';

interface GetGuildProps extends ICallback<any> {
    guildId: string;
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
}

export const guildInstance = new GuildActions;