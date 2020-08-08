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

    export const CREATE_NEW_USER = () => ({
        type: "CREATE_NEW_USER",
    } as const);

    export const CREATE_NEW_USER_SUCCESS = () => ({
        type: "CREATE_NEW_USER_SUCCESS",
    } as const);

    export const CREATE_NEW_USER_FAILURE = () => ({
        type: "CREATE_NEW_USER_FAILURE",
    } as const);


    export const CHECK_CODE_PROCESS = () => ({
        type: "CHECK_CODE_PROCESS",
    } as const);

    export const CHECK_CODE_SUCCESS = () => ({
        type: "CHECK_CODE_SUCCESS",
    } as const);

    export const CHECK_CODE_FAILURE = () => ({
        type: "CHECK_CODE_FAILURE",
    } as const);
}

export type SessionActionsTypes =
    ReturnType<typeof SessionActionsType.CREATE_SESSION_SUCCESS> |
    ReturnType<typeof SessionActionsType.CREATE_SESSION> |
    ReturnType<typeof SessionActionsType.CREATE_SESSION_FAILURE> |

    ReturnType<typeof SessionActionsType.CLOSING_SESSION> |
    ReturnType<typeof SessionActionsType.CLOSED_SESSION> |

    ReturnType<typeof SessionActionsType.CREATE_NEW_USER> |
    ReturnType<typeof SessionActionsType.CREATE_NEW_USER_SUCCESS> |
    ReturnType<typeof SessionActionsType.CREATE_NEW_USER_FAILURE> |

    ReturnType<typeof SessionActionsType.CHECK_CODE_PROCESS> |
    ReturnType<typeof SessionActionsType.CHECK_CODE_SUCCESS> |
    ReturnType<typeof SessionActionsType.CHECK_CODE_FAILURE>;