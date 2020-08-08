import { SessionActionsTypes } from "../../_actions/SessionActionsType";
import { SessionInfo } from "../../_services";

let localStorageSession: string | null = localStorage.getItem('session');
let sessionInfo: string | undefined;
try {
    sessionInfo = JSON.parse(localStorageSession || "");
} catch (ex) { }
const initialState: SessionInfo = sessionInfo ? new SessionInfo(sessionInfo) : new SessionInfo();

export function session(state = initialState, action: SessionActionsTypes): SessionInfo {
    switch (action.type) {
        case 'CREATE_SESSION':
            return new SessionInfo({ holding: true });

        case 'CREATE_SESSION_SUCCESS':
            return new SessionInfo(action.session);

        case 'CREATE_SESSION_FAILURE':
            return new SessionInfo();

        case 'CLOSING_SESSION':
            return new SessionInfo({ holding: true });

        case 'CLOSED_SESSION':
            return new SessionInfo();

        case 'CREATE_NEW_USER':
            return new SessionInfo();

        case 'CREATE_NEW_USER_SUCCESS':
            var s = new SessionInfo();
            s.isNew = true;
            return s;

        case 'CREATE_NEW_USER_SUCCESS':
            return new SessionInfo();

            

        case 'CHECK_CODE_PROCESS':
            return new SessionInfo();

        case 'CHECK_CODE_SUCCESS':
            var s = new SessionInfo();
            s.ConfirmCodeState = true;
            return s;

        case 'CHECK_CODE_FAILURE':
            var s = new SessionInfo();
            s.ConfirmCodeState = false;
            return s;

        default:
            return state
    }
}