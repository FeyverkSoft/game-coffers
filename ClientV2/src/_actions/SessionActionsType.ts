import { SessionInfo } from "../_services";

export namespace SessionActionsType {
    export const CREATE_SESSION = () => ({
        type: "CREATE_SESSION",
    } as const);
    export const CREATE_SESSION_SUCCESS = (session: SessionInfo) => ({
        type: "CREATE_SESSION_SUCCESS",
        session: session,
    } as const);
    export const CREATE_SESSION_FAILURE = () => ({
        type: "CREATE_SESSION_FAILURE",
    } as const);

    export const CLOSING_SESSION = () => ({
        type: "CLOSING_SESSION",
    } as const);
    
    export const CLOSED_SESSION = () => ({
        type: "CLOSED_SESSION",
    } as const);
}


export type SessionActionsTypes =
    ReturnType<typeof SessionActionsType.CREATE_SESSION_SUCCESS> |
    ReturnType<typeof SessionActionsType.CREATE_SESSION> |
    ReturnType<typeof SessionActionsType.CREATE_SESSION_FAILURE> |

    ReturnType<typeof SessionActionsType.CLOSING_SESSION> |
    ReturnType<typeof SessionActionsType.CLOSED_SESSION>;