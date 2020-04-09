
import { OperationType, operationService, IOperationView } from '../../_services';
import { alertInstance, ICallback } from '..';
import { OperationActionsType } from './OperationActionsType';

interface CreateOperationProps {
    id: string;
    type: OperationType;
    amount: number;
    description: string;
    userId: string;
    documentId?: string;
    parentOperationId?: string;
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
            operationService.createOperation(
                props.id,
                props.type,
                props.amount,
                props.description,
                props.userId,
                props.documentId,
                props.parentOperationId,
            )
                .then(
                    data => {
                        dispatch(operationsInstance.GetOperations(new Date()));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.id, props.type));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(id: string, otype: OperationType) { return { type: OperationActionsType.PROC_CREATE_OPERATION, id, otype } }
        function success(id: string, otype: OperationType, operation: IOperationView) { return { type: OperationActionsType.SUCC_CREATE_OPERATION, id, otype, operation } }
        function failure(id: string, otype: OperationType) { return { type: OperationActionsType.FAILED_CREATE_OPERATION, id, otype } }
    }
}

export const operationsInstance = new OperationActions();