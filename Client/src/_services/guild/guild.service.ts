import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, GuildInfo, GuildBalanceReport, IGamersListView, GamersListView } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { GamerStatus } from '../gamer/GamerStatus';
import { GamerRank } from '../gamer/GamerRank';

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
                    Number(data.activeLoansAmount));
            })
            .catch(catchHandle);
    }

    /**
    * Добавляет нового пользователя в гильдию
    */
    static async AddUser(guildId: string,
        id: string,
        name: string,
        rank: GamerRank,
        status: GamerStatus,
        dateOfBirth: Date,
        login: string
    ): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id: id, name: name, rank: rank, status: status, dateOfBirth: dateOfBirth, login: login })
        };
        return await fetch(Config.BuildUrl(`/Guilds/${guildId}/Gamers`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return;
            })
            .catch(catchHandle);
    }

}
