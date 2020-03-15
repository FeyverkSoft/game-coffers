import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, GuildInfo, GuildBalanceReport} from '..';
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
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/${guildId}`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GuildInfo(data.id, data.name, data.status, data.recruitmentStatus,
                    Number(data.charactersCount),
                    Number(data.gamersCount),
                    Number(data.balance),
                    {
                        Soldier: data.tariffs.soldier,
                        Beginner: data.tariffs.beginner,
                        Officer: data.tariffs.officer,
                        Veteran: data.tariffs.veteran,
                        Leader: data.tariffs.leader
                    });
            })
            .catch(catchHandle);
    }

    /**
     * Возвращает информацию о балансе гильдии по ID
     */
    static async GetGuildBalanceReport(guildId: string): Promise<GuildBalanceReport> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/${guildId}/balance`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GuildBalanceReport(
                    Number(data.balance),
                    Number(data.expectedTaxAmount),
                    Number(data.taxAmount),
                    Number(data.activeLoansAmount),
                    Number(data.repaymentLoansAmount),
                    Number(data.gamersBalance));
            })
            .catch(catchHandle);
    }

}
