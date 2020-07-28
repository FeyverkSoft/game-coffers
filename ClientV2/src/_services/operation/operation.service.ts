import { getResponse, catchHandle, errorHandle, formatDateTime } from '../../_helpers';
import { BaseResponse, OperationType, IOperationView, OperationView } from '..';
import { Config } from '../../core';
import { authService } from '..';
import { IAvailableDocument, AvailableDocument } from './IAvailableDocuments';

export class operationService {

    /**
     * Получить список опеаций
     */
    static async getOperations(DateMonth: Date): Promise<Array<IOperationView>> {
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
        return await fetch(Config.BuildUrl(`/Guilds/current/operations`, { DateMonth: formatDateTime(DateMonth, 'm') }), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new OperationView(
                    _.id,
                    _.amount,
                    _.documentId,
                    _.documentAmount,
                    _.documentDescription,
                    _.userId,
                    _.userName,
                    _.type,
                    _.description,
                    _.createDate,
                    {
                        description: (_.parrentOperation || {}).description,
                        id: (_.parrentOperation || {}).id
                    }
                ));
            })
            .catch(catchHandle);
    }

    /**
     * Создать новую операцию
     */
    static async createOperation(id: string, type: OperationType, amount: number, description: string,
        userId: string, documentId?: string, parentOperationId?: string):
        Promise<IOperationView> {
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
                id: id,
                userId: userId,
                parentOperationId: parentOperationId,
                type: type,
                amount: amount,
                description: description,
                documentId: documentId,
            })
        };
        return await fetch(Config.BuildUrl(`/Operations`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data.traceId) {
                    return errorHandle(data);
                }
                return new OperationView(
                    data.id,
                    data.amount,
                    data.documentId,
                    data.documentAmount,
                    data.documentDescription,
                    data.userId,
                    data.userName,
                    data.type,
                    data.description,
                    data.createDate
                );
            })
            .catch(catchHandle);
    }

    /**
     * Обновить текущую операцию
     */
    static async editOperation(id: string, type: string, documentId: string): Promise<IOperationView> {
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
                type: type,
                documentId: documentId,
            })
        };
        return await fetch(Config.BuildUrl(`/operations/${id}/document`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if (data.traceId) {
                    return errorHandle(data);
                }
                return new OperationView(
                    data.id,
                    data.amount,
                    data.documentId,
                    data.documentAmount,
                    data.documentDescription,
                    data.userId,
                    data.userName,
                    data.type,
                    data.description,
                    data.createDate
                );
            })
            .catch(catchHandle);
    }

    /**
     * Получить список опеаций
     */
    static async getDocuments(): Promise<Array<IAvailableDocument>> {
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
        return await fetch(Config.BuildUrl(`/Guilds/current/operations/documents`), requestOptions)
            .then<BaseResponse & Array<IAvailableDocument>>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new AvailableDocument(
                    _.id,
                    _.userId,
                    _.description,
                    _.documentType
                ));
            })
            .catch(catchHandle);
    }
}
