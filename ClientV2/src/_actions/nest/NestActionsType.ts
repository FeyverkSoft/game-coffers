import { Nest } from "../../_services/nest/Nest";
import { Contract } from "../../_services/nest/Contract";
import { IDictionary } from "../../core/Dictionary";

export namespace NestActionType {
    export const PROC_GET_NEST_LIST = () => ({
        type: "PROC_GET_NEST_LIST",
    } as const);
    export const SUCC_GET_NEST_LIST = (nests: Array<Nest>) => ({
        type: "SUCC_GET_NEST_LIST",
        nests: nests,
    } as const);
    export const FAILED_GET_NEST_LIST = () => ({
        type: "FAILED_GET_NEST_LIST",
    } as const);


    export const PROC_ADD_CONTRACT = () => ({
        type: "PROC_ADD_CONTRACT",
    } as const);
    export const SUCC_ADD_CONTRACT = (contract: Contract) => ({
        type: "SUCC_ADD_CONTRACT",
        contract: contract,
    } as const);
    export const FAILED_ADD_CONTRACT = () => ({
        type: "FAILED_ADD_CONTRACT",
    } as const);


    export const PROC_DELETE_CONTRACT = (id: string) => ({
        type: "PROC_DELETE_CONTRACT",
        id: id,
    } as const);
    export const SUCC_DELETE_CONTRACT = (id: string) => ({
        type: "SUCC_DELETE_CONTRACT",
        id: id,
    } as const);
    export const FAILED_DELETE_CONTRACT = (id: string) => ({
        type: "FAILED_DELETE_CONTRACT",
        id: id,
    } as const);


    export const PROC_GET_MY_CONTRACTS = () => ({
        type: "PROC_GET_MY_CONTRACTS",
    } as const);
    export const SUCC_GET_MY_CONTRACTS = (contracts: Array<Contract>) => ({
        type: "SUCC_GET_MY_CONTRACTS",
        userContracts: contracts,
    } as const);
    export const FAILED_GET_MY_CONTRACTS = () => ({
        type: "FAILED_GET_MY_CONTRACTS",
    } as const);


    export const PROC_GET_GUILD_CONTRACTS = () => ({
        type: "PROC_GET_GUILD_CONTRACTS",
    } as const);
    export const SUCC_GET_GUILD_CONTRACTS = (guildContracts: IDictionary<Contract>) => ({
        type: "SUCC_GET_GUILD_CONTRACTS",
        guildContracts: guildContracts,
    } as const);
    export const FAILED_GET_GUILD_CONTRACTS = () => ({
        type: "FAILED_GET_GUILD_CONTRACTS",
    } as const);
}

export type NestActionTypes =
    ReturnType<typeof NestActionType.PROC_GET_NEST_LIST> |
    ReturnType<typeof NestActionType.SUCC_GET_NEST_LIST> |
    ReturnType<typeof NestActionType.FAILED_GET_NEST_LIST> |

    ReturnType<typeof NestActionType.PROC_ADD_CONTRACT> |
    ReturnType<typeof NestActionType.SUCC_ADD_CONTRACT> |
    ReturnType<typeof NestActionType.FAILED_ADD_CONTRACT> |

    ReturnType<typeof NestActionType.PROC_DELETE_CONTRACT> |
    ReturnType<typeof NestActionType.SUCC_DELETE_CONTRACT> |
    ReturnType<typeof NestActionType.FAILED_DELETE_CONTRACT> |

    ReturnType<typeof NestActionType.PROC_GET_MY_CONTRACTS> |
    ReturnType<typeof NestActionType.SUCC_GET_MY_CONTRACTS> |
    ReturnType<typeof NestActionType.FAILED_GET_MY_CONTRACTS> |

    ReturnType<typeof NestActionType.PROC_GET_GUILD_CONTRACTS> |
    ReturnType<typeof NestActionType.SUCC_GET_GUILD_CONTRACTS> |
    ReturnType<typeof NestActionType.FAILED_GET_GUILD_CONTRACTS>
    ;