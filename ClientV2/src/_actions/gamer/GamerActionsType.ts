import { IGamersListView, ILoanView, IPenaltyView } from "../../_services"
import { GamerRank, GamerStatus } from "../../_services"

export namespace GamerActionsType {
    export const PROC_GET_GUILD_GAMERS = (date: Date) => ({
        type: "PROC_GET_GUILD_GAMERS",
        date: date,
    } as const)
    export const SUCC_GET_GUILD_GAMERS = (date: Date, gamersList: Array<IGamersListView>) => ({
        type: "SUCC_GET_GUILD_GAMERS",
        date: date,
        gamersList: gamersList,
    } as const)
    export const FAILED_GET_GUILD_GAMERS = (date: Date) => ({
        type: "FAILED_GET_GUILD_GAMERS",
        date: date,
    } as const)


    export const PROC_SET_GAMER_STATUS = (userId: string) => ({
        type: "PROC_SET_GAMER_STATUS",
        userId: userId,
    } as const)
    export const SUCC_SET_GAMER_STATUS = (userId: string, status: GamerStatus) => ({
        type: "SUCC_SET_GAMER_STATUS",
        userId: userId,
        status: status,
    } as const)
    export const FAILED_SET_GAMER_STATUS = (userId: string) => ({
        type: "FAILED_SET_GAMER_STATUS",
        userId: userId,
    } as const)


    export const PROC_SET_GAMER_RANK = (userId: string) => ({
        type: "PROC_SET_GAMER_RANK",
        userId: userId,
    } as const)
    export const SUCC_SET_GAMER_RANK = (userId: string, rank: GamerRank) => ({
        type: "SUCC_SET_GAMER_RANK",
        userId: userId,
        rank: rank,
    } as const)
    export const FAILED_SET_GAMER_RANK = (userId: string) => ({
        type: "FAILED_SET_GAMER_RANK",
        userId: userId,
    } as const)


    export const PROC_ADD_NEW_CHARS = (userId: string) => ({
        type: "PROC_ADD_NEW_CHARS",
        userId: userId
    } as const)
    export const SUCC_ADD_NEW_CHARS = (userId: string, characterId: string, name: string, className: string, isMain: boolean) => ({
        type: "SUCC_ADD_NEW_CHARS",
        userId: userId,
        characterId: characterId,
        name: name,
        className: className,
        isMain: isMain,
    } as const)
    export const FAILED_ADD_NEW_CHARS = (userId: string) => ({
        type: "FAILED_ADD_NEW_CHARS",
        userId: userId,
    } as const)


    export const PROC_DELETE_CHARS = (userId: string) => ({
        type: "PROC_DELETE_CHARS",
        userId: userId
    } as const)
    export const SUCC_DELETE_CHARS = (userId: string, characterId: string) => ({
        type: "SUCC_DELETE_CHARS",
        userId: userId,
        characterId: characterId,
    } as const)
    export const FAILED_DELETE_CHARS = (userId: string) => ({
        type: "FAILED_DELETE_CHARS",
        userId: userId,
    } as const)


    export const PROC_ADD_GAMER_LOAN = (userId: string) => ({
        type: "PROC_ADD_GAMER_LOAN",
        userId: userId,
    } as const)
    export const SUCC_ADD_GAMER_LOAN = (userId: string, loan: ILoanView) => ({
        type: "SUCC_ADD_GAMER_LOAN",
        userId: userId,
        loan: loan,
    } as const)
    export const FAILED_ADD_GAMER_LOAN = (userId: string) => ({
        type: "FAILED_ADD_GAMER_LOAN",
        userId: userId,
    } as const)


    export const PROC_CANCEL_GAMER_LOAN = (loanId: string) => ({
        type: "PROC_CANCEL_GAMER_LOAN",
        loanId: loanId,
    } as const)
    export const SUCC_CANCEL_GAMER_LOAN = (loanId: string) => ({
        type: "SUCC_CANCEL_GAMER_LOAN",
        loanId: loanId,
    } as const)
    export const FAILED_CANCEL_GAMER_LOAN = (loanId: string) => ({
        type: "FAILED_CANCEL_GAMER_LOAN",
        loanId: loanId,
    } as const)


    export const PROC_ADD_GAMER_PENALTY = (userId: string) => ({
        type: "PROC_ADD_GAMER_PENALTY",
        userId: userId,
    } as const)
    export const SUCC_ADD_GAMER_PENALTY = (userId: string, penalty: IPenaltyView) => ({
        type: "SUCC_ADD_GAMER_PENALTY",
        userId: userId,
        penalty: penalty,
    } as const)
    export const FAILED_ADD_GAMER_PENALTY = (userId: string) => ({
        type: "FAILED_ADD_GAMER_PENALTY",
        userId: userId,
    } as const)


    export const PROC_CANCEL_GAMER_PENALTY = (penaltyId: string) => ({
        type: "PROC_CANCEL_GAMER_PENALTY",
        penaltyId: penaltyId,
    } as const)
    export const SUCC_CANCEL_GAMER_PENALTY = (penaltyId: string, penalty: IPenaltyView) => ({
        type: "SUCC_CANCEL_GAMER_PENALTY",
        penaltyId: penaltyId,
    } as const)
    export const FAILED_CANCEL_GAMER_PENALTY = (penaltyId: string) => ({
        type: "FAILED_CANCEL_GAMER_PENALTY",
        penaltyId: penaltyId,
    } as const)

}

export type GamerActionsType =
    ReturnType<typeof GamerActionsType.PROC_GET_GUILD_GAMERS> |
    ReturnType<typeof GamerActionsType.SUCC_GET_GUILD_GAMERS> |
    ReturnType<typeof GamerActionsType.FAILED_GET_GUILD_GAMERS> |

    ReturnType<typeof GamerActionsType.PROC_SET_GAMER_STATUS> |
    ReturnType<typeof GamerActionsType.SUCC_SET_GAMER_STATUS> |
    ReturnType<typeof GamerActionsType.FAILED_SET_GAMER_STATUS> |

    ReturnType<typeof GamerActionsType.PROC_SET_GAMER_RANK> |
    ReturnType<typeof GamerActionsType.SUCC_SET_GAMER_RANK> |
    ReturnType<typeof GamerActionsType.FAILED_SET_GAMER_RANK> |

    ReturnType<typeof GamerActionsType.PROC_ADD_NEW_CHARS> |
    ReturnType<typeof GamerActionsType.SUCC_ADD_NEW_CHARS> |
    ReturnType<typeof GamerActionsType.FAILED_ADD_NEW_CHARS> |

    ReturnType<typeof GamerActionsType.PROC_DELETE_CHARS> |
    ReturnType<typeof GamerActionsType.SUCC_DELETE_CHARS> |
    ReturnType<typeof GamerActionsType.FAILED_DELETE_CHARS> |

    ReturnType<typeof GamerActionsType.PROC_ADD_GAMER_LOAN> |
    ReturnType<typeof GamerActionsType.SUCC_ADD_GAMER_LOAN> |
    ReturnType<typeof GamerActionsType.FAILED_ADD_GAMER_LOAN> |

    ReturnType<typeof GamerActionsType.PROC_CANCEL_GAMER_LOAN> |
    ReturnType<typeof GamerActionsType.SUCC_CANCEL_GAMER_LOAN> |
    ReturnType<typeof GamerActionsType.FAILED_CANCEL_GAMER_LOAN> |

    ReturnType<typeof GamerActionsType.PROC_ADD_GAMER_PENALTY> |
    ReturnType<typeof GamerActionsType.SUCC_ADD_GAMER_PENALTY> |
    ReturnType<typeof GamerActionsType.FAILED_ADD_GAMER_PENALTY> 
