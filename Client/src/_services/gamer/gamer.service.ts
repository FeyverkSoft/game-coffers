import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, IGamerInfo, GamerInfo, IGamersListView, GamersListView } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { GamerStatus } from './GamerStatus';
import { GamerRank } from './GamerRank';

export class gamerService {

    /**
     * Добавить игроку новый штраф
     * @param gamerId 
     * @param id 
     * @param amount 
     * @param description 
     */
    static async AddPenalty(gamerId: string, id: string, amount: number, description: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PUT',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id, amount, description })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/penalties`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Добавить игроку новый займ
     * @param gamerId 
     * @param id 
     * @param amount 
     * @param description 
     * @param borrowDate 
     * @param expiredDate 
     */
    static async AddLoan(gamerId: string, id: string, amount: number, description: string, borrowDate: Date, expiredDate: Date): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PUT',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id, amount, description, borrowDate, expiredDate })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/loans`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Добавить нового персонажа игроку
     * @param gamerId 
     * @param name 
     * @param className 
     */
    static async AddNewChar(gamerId: string, name: string, className: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PUT',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ name: name, className: className })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/characters`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Удалить персонажа у игрока
     * @param gamerId 
     * @param name 
     * @param className 
     */
    static async DeleteChar(gamerId: string, name: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'DELETE',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ name: name })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/characters`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Получить информацию об игроке текущем владельце сессии
     */
    static async getCurrentGamer(): Promise<IGamerInfo> {
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
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/${guildId}/gamers`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map((g: any) => new GamersListView(
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
                'Content-Type': 'application/json',
                'accept': 'application/json',
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
                'Content-Type': 'application/json',
                'accept': 'application/json',
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

    /**
     * Отменить ещё не штраф займ у игрока
     * @param gamerId 
     * @param id 
     * @param amount 
     * @param description 
     */
    static async CancelPenalty(gamerId: string, id: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'DELETE',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/penalties`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Отменить ещё не оплаченный займ у игрока
     * @param gamerId 
     * @param id 
     * @param amount 
     * @param description 
     * @param borrowDate 
     * @param expiredDate 
     */
    static async CancelLoan(gamerId: string, id: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'DELETE',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id })
        };
        return await fetch(Config.BuildUrl(`/Gamers/${gamerId}/loans`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }
}
