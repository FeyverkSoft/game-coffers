import { OperationActionsTypes } from "../../_actions";
import { IHolded, IDictionary } from "../../core";
import { IOperationView } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { formatDateTime } from "../../_helpers";
import { IAvailableDocument } from "../../_services/operation/IAvailableDocuments";

export class IOperationsStore {
    operations: IDictionary<Array<IOperationView>> & IHolded = {};
    documents: Array<IAvailableDocument> & IHolded = [];
}

export function operations(state: IOperationsStore = new IOperationsStore(), action: OperationActionsTypes & {
    operation: IOperationView;
    operations: Array<IOperationView>;
}):
    IOperationsStore {
    const clonedState = clonedeep(state);
    switch (action.type) {
        case 'PROC_GET_OPERATIONS': {
            let opData = formatDateTime(action.date, 'm');
            clonedState.operations[opData] = [...clonedState.operations[opData] || []];
            clonedState.operations.holding = true;
            return clonedState;
        }

        case 'SUCC_GET_OPERATIONS': {
            clonedState.operations[formatDateTime(action.date, 'm')] = action.operations;
            clonedState.operations.holding = false;
            return clonedState;
        }

        case 'FAILED_GET_OPERATIONS': {
            clonedState.operations.holding = false;
            return clonedState;
        }

        case 'SUCC_CREATE_OPERATION': {
            let opData = formatDateTime(action.operation.createDate, 'm');
            clonedState.operations[opData] = [...clonedState.operations[opData] || [], action.operation];
            clonedState.operations.holding = false;
            return clonedState;
        }


        case 'PROC_GET_DOCUMENTS': {
            clonedState.documents.holding = true;
            return clonedState;
        }

        case 'SUCC_GET_DOCUMENTS': {
            clonedState.documents = action.documents;
            return clonedState;
        }

        case 'FAILED_GET_DOCUMENTS': {
            clonedState.documents.holding = false;
            return clonedState;
        }

        case 'PROC_EDIT_OPERATION': {
            clonedState.operations.holding = true;
            return clonedState;
        }

        case 'SUCC_EDIT_OPERATION': {
            let operations = clonedState.operations[formatDateTime(action.operation.createDate, 'm')];
            operations.forEach(operation => {
                if (operation.id === action.operation.id) {
                    operation = action.operation;
                }
            });
            clonedState.operations.holding = false;
            return clonedState;
        }
        case 'FAILED_EDIT_OPERATION': {
            clonedState.operations.holding = false;
            return clonedState;
        }

        default:
            return state
    }
}