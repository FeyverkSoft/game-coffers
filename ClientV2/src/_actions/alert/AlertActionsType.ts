import { Alert } from "../../_services"

export namespace AlertActionsType {
    export const SUCCESS = (alert: Alert) => ({
        type: "SUCCESS_ALERT",
        alert: alert,
    } as const)
    export const ERROR = (alert: Alert) => ({
        type: "ERROR_ALERT",
        alert: alert,
    } as const)
    export const INFO = (alert: Alert) => ({
        type: "INFO_ALERT",
        alert: alert,
    } as const)

    export const CLEAR = () => ({
        type: "CLEAR_ALERT",
    } as const)

    export const DELETE = (id: string) => ({
        type: "DELETE-ALERT",
        id: id,
    } as const)
}

export type AlertActionsTypes =
    ReturnType<typeof AlertActionsType.SUCCESS> |
    ReturnType<typeof AlertActionsType.ERROR> |
    ReturnType<typeof AlertActionsType.INFO> |
    ReturnType<typeof AlertActionsType.CLEAR> |
    ReturnType<typeof AlertActionsType.DELETE>