import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, IGamerInfo, GamerInfo, IGamersListView, GamersListView } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { GamerStatus } from './GamerStatus';
import { GamerRank } from './GamerRank';

export class gamerService {

    /**
     * Получить информацию об игроке текущем владельце сессии
     */
    static async getCurrentGamer(): Promise<IGamerInfo> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Gamers/current`), requestOptions)
            .then<BaseResponse & IGamerInfo>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GamerInfo(data.userId, data.name, data.balance || 0, data.activeLoanAmount || 0,
                    data.activePenaltyAmount || 0, data.activeExpLoanAmount || 0, data.activeLoanTaxAmount || 0,
                    data.repaymentLoanAmount || 0, data.rank, data.charCount || 0);
            })
            .catch(catchHandle);
    }

    /**
     * Возвращает список игроков в гильдии удовлетворяющих условию
     */
    static async GetGamers(guildId: string): Promise<Array<IGamersListView>> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/${guildId}/gamers`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map((g: IGamersListView) => new GamersListView(
                    g.id,
                    g.characters,
                    Number(g.balance),
                    g.penalties,
                    g.loans,
                    g.rank,
                    g.status
                ));
            })
            .catch(catchHandle);
    }


    /**
     * This method changed gamer status
     */
    static async SetStatus(gamerId: string, status: GamerStatus): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PATCH',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ status: status })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/status`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * This method changed gamer rank
     */
    static async SetRank(gamerId: string, rank: GamerRank): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PATCH',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ rank: rank })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/rank`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }
}
