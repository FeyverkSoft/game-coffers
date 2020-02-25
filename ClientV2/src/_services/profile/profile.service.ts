import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, IGamerInfo, GamerInfo } from '..';
import { Config } from '../../core';
import { authService } from '..';

export class profileService {
    /**
     * Добавить нового персонажа игроку
     * @param name 
     * @param className 
     */
    static async AddNewChar(name: string, className: string): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/profile/characters`), requestOptions)
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
     * @param name 
     * @param className 
     */
    static async DeleteChar(name: string): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/profile/characters`), requestOptions)
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
        return await fetch(Config.BuildUrl(`/gamers/current/profile`), requestOptions)
            .then<BaseResponse & IGamerInfo>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GamerInfo(data.userId, data.name,
                    Number(data.balance || 0),
                    Number(data.activeLoanAmount || 0),
                    Number(data.activePenaltyAmount || 0),
                    Number(data.activeExpLoanAmount || 0),
                    Number(data.activeLoanTaxAmount || 0),
                    Number(data.repaymentLoanAmount || 0),
                    Number(data.repaymentTaxAmount || 0),
                    data.rank, data.charCount || 0);
            })
            .catch(catchHandle);
    }
}
