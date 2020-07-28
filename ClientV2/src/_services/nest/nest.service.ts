import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse } from '..';
import { Config, IDictionary } from '../../core';
import { authService } from '..';
import { Contract } from './Contract';
import { Nest } from './Nest';

export class nestService {

    /**
     * Добавить новый контракт игроку
     */
    static async addContract(id: string, nestId: string, characterName: string, reward: string): Promise<Contract> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({ id: id, nestId: nestId, characterName: characterName, reward: reward })
        };
        return await fetch(Config.BuildUrl(`/gamers/current/contracts`), requestOptions)
            .then<BaseResponse & Contract>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return new Contract(data.id, data.nestName, data.characterName, data.reward);
            })
            .catch(catchHandle);
    }

    /**
     * Удалить контракт
     * @param id 
     */
    static async deleteContract(id: string): Promise<void> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/contracts/${id}`), requestOptions)
            .then<BaseResponse>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }

    /**
     * Получить список доступных логовов
     */
    static async getNestList(): Promise<Array<Nest>> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'default',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/guilds/current/nests`), requestOptions)
            .then<BaseResponse & Array<Nest>>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new Nest(_.id, _.name));
            })
            .catch(catchHandle);
    }

    /**
    * Получить список контрактов игрока
    */
    static async getMyContracts(): Promise<Array<Contract>> {
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
        return await fetch(Config.BuildUrl(`/gamers/current/contracts`), requestOptions)
            .then<BaseResponse & Array<Contract>>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map((c) => new Contract(c.id, c.nestName, c.characterName, c.reward));
            })
            .catch(catchHandle);
    }

    /**
     * Получить список контрактов в гильдии
     */
    static async getGuildContracts(props: {}): Promise<IDictionary<Array<Contract>>> {
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
        return await fetch(Config.BuildUrl(`/guilds/current/contracts`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                let result: IDictionary<Array<Contract>> = {};
                Object.keys(data).forEach(_ => {
                    result[_] = data[_].map((c: any) => new Contract(c.id, _, c.characterName, c.reward));
                })
                return result;
            })
            .catch(catchHandle);
    }
}
