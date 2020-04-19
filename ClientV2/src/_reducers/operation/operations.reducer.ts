import { OperationActionsType } from "../../_actions";
import { IAction, IHolded, IDictionary } from "../../core";
import { IOperationView } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { formatDateTime } from "../../_helpers";
import { IAvailableDocument } from "../../_services/operation/IAvailableDocuments";

export class IOperationsStore {
    operations: IDictionary<Array<IOperationView>> & IHolded = {};
    documents: Array<IAvailableDocument> & IHolded = [];
}

export function operations(state: IOperationsStore = new IOperationsStore(), action: IAction<OperationActionsType>):
    IOperationsStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case OperationActionsType.PROC_GET_OPERATIONS: {
            clonedState.operations[formatDateTime(action.date, 'm')] = [...clonedState.operations[action.date] || []];
            clonedState.operations.holding = true;
            return clonedState;
        }

        case OperationActionsType.SUCC_GET_OPERATIONS: {
            clonedState.operations[formatDateTime(action.date, 'm')] = action.operations;
            clonedState.operations.holding = false;
            return clonedState;
        }

        case OperationActionsType.FAILED_GET_OPERATIONS: {
            clonedState.operations.holding = false;
            return clonedState;
        }


        case OperationActionsType.SUCC_CREATE_OPERATION: {
            clonedState.operations[formatDateTime(action.date, 'm')] = [...clonedState.operations[action.date] || [], action.operation];
            clonedState.operations.holding = false;
            return clonedState;
        }


        case OperationActionsType.PROC_GET_DOCUMENTS: {
            clonedState.documents.holding = true;
            return clonedState;
        }

        case OperationActionsType.SUCC_GET_DOCUMENTS: {
            clonedState.documents = action.documents;
            return clonedState;
        }

        case OperationActionsType.FAILED_GET_DOCUMENTS: {
            clonedState.documents.holding = false;
            return clonedState;
        }
        default:
            return state
    }
}