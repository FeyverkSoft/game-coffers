import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, OperationType, IOperationView, OperationView } from '..';
import { Config } from '../../core';
import { authService } from '..';

export class operationService {

    /**
     * Получить список опеаций
     */
    static async GetOperations(DateMonth: Date): Promise<Array<IOperationView>> {
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
        return await fetch(Config.BuildUrl(`/Guilds/current/operations`, { DateMonth: DateMonth.toISOString() }), requestOptions)
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
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
            })
            .catch(catchHandle);
    }
}
