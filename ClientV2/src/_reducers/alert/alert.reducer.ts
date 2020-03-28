import { AlertActionsType } from '../../_actions';
import { IAction } from '../../core';
import { Alert } from '../../_services/alert/Alert';
const count = 5;//количество отображаемых сообщений

export class AlertState {
    messages: Alert[];
    constructor(arr: Alert[] = new Array<Alert>()) {
        this.messages = arr;
    }
}

export function alerts(state: AlertState = new AlertState(), action: IAction<AlertActionsType>): AlertState {
    if ((action.alert && !action.alert.message) &&
        [AlertActionsType.CLEAR, AlertActionsType.DELETE].indexOf(action.type) <= 0)
        return state;
    switch (action.type) {
        case AlertActionsType.SUCCESS:
        case AlertActionsType.ERROR:
        case AlertActionsType.INFO:
            return new AlertState([action.alert, ...state.messages].slice(0, count));
        case AlertActionsType.DELETE:
            return new AlertState(state.messages.filter(x => x.id !== action.id))
        case AlertActionsType.CLEAR:
            return new AlertState();
        default:
            return state
    }
}