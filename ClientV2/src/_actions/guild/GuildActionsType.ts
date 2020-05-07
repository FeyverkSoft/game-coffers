import { GuildInfo, ITariff } from "../../_services/guild/GuildInfo"
import { GuildBalanceReport } from "../../_services/guild/GuildBalanceReport"

export namespace GuildActionsType {
    export const PROC_GET_GUILD = () => ({
        type: "PROC_GET_GUILD",
    } as const)
    export const SUCC_GET_GUILD = (guildInfo: GuildInfo) => ({
        type: "SUCC_GET_GUILD",
        guildInfo: guildInfo,
    } as const)
    export const FAILED_GET_GUILD = () => ({
        type: "FAILED_GET_GUILD",
    } as const)


    export const PROC_GET_BALANCE_REPORT = () => ({
        type: "PROC_GET_BALANCE_REPORT",
    } as const)
    export const SUCC_GET_BALANCE_REPORT = (BalanceInfo: GuildBalanceReport) => ({
        type: "SUCC_GET_BALANCE_REPORT",
        balanceInfo: BalanceInfo,
    } as const)
    export const FAILED_GET_BALANCE_REPORT = () => ({
        type: "FAILED_GET_BALANCE_REPORT",
    } as const)


    export const PROC_GET_TARIFFS = () => ({
        type: "PROC_GET_TARIFFS",
    } as const)
    export const SUCC_GET_TARIFFS = (tariffs: Array<ITariff>) => ({
        type: "SUCC_GET_TARIFFS",
        tariffs: tariffs,
    } as const)
    export const FAILED_GET_TARIFFS = () => ({
        type: "FAILED_GET_TARIFFS",
    } as const)
}


export type GuildActionsTypes =
    ReturnType<typeof GuildActionsType.PROC_GET_GUILD> |
    ReturnType<typeof GuildActionsType.SUCC_GET_GUILD> |
    ReturnType<typeof GuildActionsType.FAILED_GET_GUILD> |

    ReturnType<typeof GuildActionsType.PROC_GET_BALANCE_REPORT> |
    ReturnType<typeof GuildActionsType.SUCC_GET_BALANCE_REPORT> |
    ReturnType<typeof GuildActionsType.FAILED_GET_BALANCE_REPORT> |

    ReturnType<typeof GuildActionsType.PROC_GET_TARIFFS> |
    ReturnType<typeof GuildActionsType.SUCC_GET_TARIFFS> |
    ReturnType<typeof GuildActionsType.FAILED_GET_TARIFFS> 