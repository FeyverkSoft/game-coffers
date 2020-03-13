
import { OperationType, operationService, IOperationView } from '../../_services';
import { alertInstance, ICallback } from '..';
import { OperationActionsType } from './OperationActionsType';

interface GetOperationsProps extends ICallback<any> {
    documentId: string;
    type: OperationType;
}

interface CreateOperationProps extends ICallback<any> {
    id: string;
    fromUserId?: string;
    toUserId?: string;
    type: OperationType;
    amount: number;
    description: string;
    penaltyId?: string;
    loanId?: string;
}

class OperationActions {

    /**
     * Метод возвращает список операций по первичному документу
     * @param props 
     */
    GetOperations(date: Date): Function {
        return (dispatch: Function) => {
            dispatch(request(date));
            operationService.GetOperations(date)
                .then(
                    data => {
                        dispatch(success(date, data));
                    })
                .catch(
                    ex => {
                        dispatch(failure(date));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(date: Date) { return { type: OperationActionsType.PROC_GET_OPERATIONS, date } }
        function success(date: Date, operations: Array<IOperationView>) { return { type: OperationActionsType.SUCC_GET_OPERATIONS, date, operations } }
        function failure(date: Date) { return { type: OperationActionsType.FAILED_GET_OPERATIONS, date } }
    }

    /**
     * Метод cсоздаёт новую операцию
     * @param props
     */
    CreateOperation(props: CreateOperationProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.id, props.type));
            operationService.CreateOperation(
                props.id,
                props.type,
                props.amount,
                props.description,
                props.fromUserId,
                props.toUserId,
                props.penaltyId,
                props.loanId
            )
                .then(
                    data => {
                        dispatch(success(props.id, props.type, {} as IOperationView));
                        if (props.onSuccess)
                            props.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.id, props.type));
                        dispatch(alertInstance.error(ex));
                        if (props.onFailure)
                            props.onFailure(ex);
                    });
        }
        function request(id: string, otype: OperationType) { return { type: OperationActionsType.PROC_CREATE_OPERATION, id, otype } }
        function success(id: string, otype: OperationType, operation: IOperationView) { return { type: OperationActionsType.SUCC_CREATE_OPERATION, id, otype, operation } }
        function failure(id: string, otype: OperationType) { return { type: OperationActionsType.FAILED_CREATE_OPERATION, id, otype } }
    }
}

export const operationsInstance = new OperationActions();