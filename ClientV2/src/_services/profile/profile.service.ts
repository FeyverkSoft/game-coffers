import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { IProfile, Profile } from './IProfile';

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
    static async getCurrentGamer(): Promise<IProfile> {
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
            .then<BaseResponse & IProfile>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new Profile(
                    data.userId,
                    data.name,
                    data.characterName,
                    Number(data.balance || 0),
                    Number(data.activeLoanAmount || 0),
                    Number(data.activePenaltyAmount || 0),
                    data.rank,
                    data.charCount || 0);
            })
            .catch(catchHandle);
    }
}
