import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { IProfile, Profile } from './IProfile';
import { ITax, UserTax } from './ITax';
import { ICharacter, Character } from './ICharacter';

export class profileService {

    /**
     * Добавить нового персонажа игроку
     * @param name 
     * @param className 
     */
    static async AddNewChar(name: string, className: string, isMain: boolean): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/characters`), requestOptions)
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
     * @param id 
     */
    static async DeleteChar(id: string): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/characters/${id}`), requestOptions)
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
                    data.charCount || 0,
                    data.dateOfBirth
                );
            })
            .catch(catchHandle);
    }

    static async GetTax(): Promise<ITax> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/tax`), requestOptions)
            .then<BaseResponse & ITax>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new UserTax(
                    data.userId,
                    data.taxAmount,
                    data.taxTariff
                );
            })
            .catch(catchHandle);
    }

    static async GetCharList(): Promise<Array<ICharacter>> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/characters`), requestOptions)
            .then<BaseResponse & Array<ICharacter>>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new Character(
                    _.id,
                    _.name,
                    _.className,
                    _.isMain
                )).sort((x, y) => (x === y) ? 0 : x ? -1 : 1);
    })
            .catch(catchHandle);
}
}
