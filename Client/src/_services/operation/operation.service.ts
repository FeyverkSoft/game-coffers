import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, OperationType, IOperationView, OperationView } from '..';
import { Config, Dictionary } from '../../core';
import { authService } from '..';

export class operationService {

    /**
     * Получить список опеаций по id документа и типу документа
     */
    static async GetOperations(documentId: string, type: OperationType): Promise<Array<IOperationView>> {
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
        return await fetch(Config.BuildUrl(`/Operations`, { documentId: documentId, type: type }), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new OperationView(
                    _.id,
                    _.amount,
                    _.documentId,
                    _.type,
                    _.description,
                    _.createDate
                ));
            })
            .catch(catchHandle);
    }


    /**
     * Получить список опеаций по id пользователя
     *  /Operations/guild/{userId}
     */
    static async GetOperationsByUserId(userId: string, dateMonth?: string): Promise<Array<IOperationView>> {
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
        return await fetch(Config.BuildUrl(`/Operations/gamer/${userId}`, { dateMonth: dateMonth }), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new OperationView(
                    _.id,
                    _.amount,
                    _.documentId,
                    _.type,
                    _.description,
                    _.createDate
                ));
            })
            .catch(catchHandle);
    }

    /**
 * Получить список опеаций по id пользователя
 *  /Operations/guild/{userId}
 */
    static async GetOperationsByGuildId(guildId: string, dateMonth?: string): Promise<Array<IOperationView>> {
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
        return await fetch(Config.BuildUrl(`/Operations/guild/${guildId}`, { dateMonth: dateMonth }), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new OperationView(
                    _.id,
                    _.amount,
                    _.documentId,
                    _.type,
                    _.description,
                    _.createDate
                ));
            })
            .catch(catchHandle);
    }

    /**
     * Создать новую операцию
     */
    static async CreateOperation(id: string, type: OperationType, amount: number, description: string,
        fromUserId?: string, toUserId?: string, penaltyId?: string, loanId?: string):
        Promise<Array<IOperationView>> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'PUT',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            },
            body: JSON.stringify({
                id: id,
                fromUserId: fromUserId,
                toUserId: toUserId,
                type: type,
                amount: amount,
                description: description,
                penaltyId: penaltyId,
                loanId: loanId
            })
        };
        return await fetch(Config.BuildUrl(`/Operations`), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }
}
