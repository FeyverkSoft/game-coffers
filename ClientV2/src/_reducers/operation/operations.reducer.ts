import { OperationActionsType } from "../../_actions";
import { IAction, IHolded, IDictionary } from "../../core";
import { IOperationView } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { formatDateTime } from "../../_helpers";

export class IOperationsStore {
    operations: IDictionary<Array<IOperationView> & IHolded> = {};
}

export function operations(state: IOperationsStore = new IOperationsStore(), action: IAction<OperationActionsType>):
    IOperationsStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        case OperationActionsType.PROC_GET_OPERATIONS: {
            clonedState.operations[formatDateTime(action.date, 'm')] = [...clonedState.operations[action.date] || []];
            clonedState.operations[formatDateTime(action.date, 'm')].holding = true;
            return clonedState;
        }

        case OperationActionsType.SUCC_GET_OPERATIONS: {
            clonedState.operations[formatDateTime(action.date, 'm')] = action.operations;
            return clonedState;
        }

        case OperationActionsType.FAILED_GET_OPERATIONS: {
            clonedState.operations[formatDateTime(action.date, 'm')].holding = false;
            return clonedState;
        }

        default:
            return state
    }
}