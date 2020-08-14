import { getResponse, catchHandle, errorHandle, formatDateTime } from '../../_helpers';
import { BaseResponse, GamerRank } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { GamerStatus } from './GamerStatus';
import { GamersListView, IGamersListView, ILoanView, IPenaltyView } from './GamersListView';

export class gamerService {

    static async addUser(id: string, name: string, rank: string, status: string, dateOfBirth: Date, login: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({
                id: String(id),
                name: String(name),
                rank,
                status,
                dateOfBirth: new Date(dateOfBirth),
                login: String(login)
            })
        };
        return await fetch(Config.BuildUrl(`/gamers/guilds/current`), requestOptions)
            .then<BaseResponse & IPenaltyView>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data;
            })
            .catch(catchHandle);
    }

    /**
     * Возвращает список игроков в гильдии удовлетворяющих условию
     */
    static async GetGamers(dateMonth: Date, gamerStatuses?: Array<GamerStatus>): Promise<Array<IGamersListView>> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
        };
        return await fetch(Config.BuildUrl(`/gamers/guilds/current`, { dateMonth: formatDateTime(dateMonth, 'm'), gamerStatuses: gamerStatuses }), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map((g: any) => new GamersListView(
                    g.id,
                    g.characters,
                    g.balance,
                    g.penalties,
                    g.loans,
                    g.rank,
                    g.status,
                    g.dateOfBirth,
                    g.name
                ));
            })
            .catch(catchHandle);
    }

    /**
       * Удалить персонажа у игрока
       * @param gamerId 
       * @param name 
       * @param className 
       */
    static async DeleteChar(gamerId: string, characterId: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'DELETE',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
        };
        return await fetch(Config.BuildUrl(`/gamers/${gamerId}/characters/${characterId}`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }


    /**
     * Добавить игроку новый штраф
     * @param gamerId 
     * @param id 
     * @param amount 
     * @param description 
     */
    static async addPenalty(userId: string, id: string, amount: number, description: string): Promise<IPenaltyView> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id: String(id), amount: Number(amount), description: String(description) })
        };
        return await fetch(Config.BuildUrl(`/users/${userId}/penalties`), requestOptions)
            .then<BaseResponse & IPenaltyView>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data;
            })
            .catch(catchHandle);
    }

    /**
     * Добавить игроку новый займ
     * @param userId 
     * @param id 
     * @param amount 
     * @param description 
     */
    static async addLoan(userId: string, id: string, amount: number, description: string): Promise<ILoanView> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({
                loanId: String(id),
                userId: String(userId),
                amount: Number(amount),
                description: String(description)
            })
        };
        return await fetch(Config.BuildUrl(`/loans`), requestOptions)
            .then<BaseResponse & ILoanView>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data;
            })
            .catch(catchHandle);
    }

    /**
     * Добавить нового персонажа игроку
     * @param gamerId 
     * @param name 
     * @param className 
     */
    static async addNewChar(userId: string, characterId: string, name: string, className: string, isMain: boolean): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({
                id: String(characterId),
                name: String(name),
                className: String(className),
                isMain: Boolean(isMain)
            })
        };
        return await fetch(Config.BuildUrl(`/gamers/${userId}/characters`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * This method changed gamer status
     */
    static async setStatus(userId: string, status: GamerStatus): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/gamers/${userId}/status`), requestOptions)
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
    static async setRank(userId: string, rank: GamerRank): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/gamers/${userId}/rank`), requestOptions)
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
     * @param id 
     */
    static async cancelLoan(id: string): Promise<void> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
        };
        return await fetch(Config.BuildUrl(`/loans/${id}/cancel`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Продлить ещё не оплаченный займ у игрока
     * @param id 
     */
    static async prolongLoan(id: string): Promise<ILoanView> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
        };
        return await fetch(Config.BuildUrl(`/loans/${id}/prolong`), requestOptions)
            .then<BaseResponse & ILoanView>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }
}
