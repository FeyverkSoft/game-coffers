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
        default:
            return state
    }
}