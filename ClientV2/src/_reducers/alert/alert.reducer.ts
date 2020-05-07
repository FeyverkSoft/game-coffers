import { AlertActionsTypes } from '../../_actions';
import { Alert } from '../../_services/alert/Alert';
const count = 5;//количество отображаемых сообщений

export class AlertState {
    messages: Alert[];
    constructor(arr: Alert[] = new Array<Alert>()) {
        this.messages = arr;
    }
}

export function alerts(state: AlertState = new AlertState(), action: AlertActionsTypes): AlertState {
    switch (action.type) {
        case 'SUCCESS_ALERT':
        case 'ERROR_ALERT':
        case 'INFO_ALERT':
            return new AlertState([action.alert, ...state.messages].slice(0, count));
        case 'DELETE-ALERT':
            return new AlertState(state.messages.filter(x => x.id !== action.id))
        case 'CLEAR_ALERT':
            return new AlertState();
        default:
            return state
    }
}