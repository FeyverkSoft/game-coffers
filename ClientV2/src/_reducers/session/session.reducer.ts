import { SessionActionsType } from "../../_actions";
import { IAction } from "../../core";
import { SessionInfo } from "../../_services";

let localStorageSession: string | null = localStorage.getItem('session');
let sessionInfo: string | undefined;
try {
    sessionInfo = JSON.parse(localStorageSession || "");
} catch (ex) { }
const initialState: SessionInfo = sessionInfo ? new SessionInfo(sessionInfo) : new SessionInfo();

export function session(state = initialState, action: IAction<SessionActionsType>): SessionInfo {
    switch (action.type) {
        case SessionActionsType.CREATE_SESSION:
            return new SessionInfo({ holding: true });

        case SessionActionsType.CREATE_SESSION_SUCCESS:
            return new SessionInfo(action.session);

        case SessionActionsType.CREATE_SESSION_FAILURE:
            return new SessionInfo();

        case SessionActionsType.CLOSING_SESSION:
            return new SessionInfo({ holding: true });

        case SessionActionsType.CLOSED_SESSION:
            return new SessionInfo();
        default:
            return state
    }
}