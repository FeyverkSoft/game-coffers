import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, GuildInfo } from '..';
import { Config } from '../../core';
import { authService } from '..';

export class guildService {

    /**
     * Получить информацию о гильдии по её id
     * @param {*} guildId идентификатор гильдии для которой будет возвращена информация
     */
    static async getGuild(guildId: string): Promise<GuildInfo> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return fetch(Config.BuildUrl(`/Guilds/${guildId}`), requestOptions)
            .then<BaseResponse & GuildInfo>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GuildInfo(data.id, data.name, data.status, data.recruitmentStatus, data.tariffs);
            })
            .catch(catchHandle);
    }
}
