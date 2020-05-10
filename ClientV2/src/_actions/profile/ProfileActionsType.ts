import { ITax, IProfile } from "../../_services";
import { ICharacter } from "../../_services/profile/ICharacter";

export namespace ProfileActionsType {
    export const PROC_GET_CURRENT_GAMER = () => ({
        type: "PROC_GET_CURRENT_GAMER",
    } as const);
    export const SUCC_GET_CURRENT_GAMER = (profile: IProfile) => ({
        type: "SUCC_GET_CURRENT_GAMER",
        profile: profile,
    } as const);
    export const FAILED_GET_CURRENT_GAMER = () => ({
        type: "FAILED_GET_CURRENT_GAMER",
    } as const);

    export const PROC_GET_CURRENT_TAX = () => ({
        type: "PROC_GET_CURRENT_TAX",
    } as const);
    export const SUCC_GET_CURRENT_TAX = (tax: ITax) => ({
        type: "SUCC_GET_CURRENT_TAX",
        tax: tax,
    } as const);
    export const FAILED_GET_CURRENT_TAX = () => ({
        type: "FAILED_GET_CURRENT_TAX",
    } as const);

    export const PROC_GET_CURRENT_CHARACTERS = () => ({
        type: "PROC_GET_CURRENT_CHARACTERS",
    } as const);
    export const SUCC_GET_CURRENT_CHARACTERS = (chars: Array<ICharacter>) => ({
        type: "SUCC_GET_CURRENT_CHARACTERS",
        chars: chars,
    } as const);
    export const FAILED_GET_CURRENT_CHARACTERS = () => ({
        type: "FAILED_GET_CURRENT_CHARACTERS",
    } as const);

    export const PROC_CURRENT_SET_MAIN = (id: string) => ({
        type: "PROC_CURRENT_SET_MAIN",
        id: id,
    } as const);
    export const SUCC_CURRENT_SET_MAIN = (id: string) => ({
        type: "SUCC_CURRENT_SET_MAIN",
        id: id,
    } as const);
    export const FAILED_CURRENT_SET_MAIN = (id: string) => ({
        type: "FAILED_CURRENT_SET_MAIN",
        id: id,
    } as const);

    export const PROC_CURRENT_DELETE_CHAR = (id: string) => ({
        type: "PROC_CURRENT_DELETE_CHAR",
        id: id,
    } as const);
    export const SUCC_CURRENT_DELETE_CHAR = (id: string) => ({
        type: "SUCC_CURRENT_DELETE_CHAR",
        id: id,
    } as const);
    export const FAILED_CURRENT_DELETE_CHAR = (id: string) => ({
        type: "FAILED_CURRENT_DELETE_CHAR",
        id: id,
    } as const);

    export const PROC_CURRENT_ADD_NEW_CHAR = () => ({
        type: "PROC_CURRENT_ADD_NEW_CHAR",
    } as const);
    export const SUCC_CURRENT_ADD_NEW_CHAR = (char: ICharacter) => ({
        type: "SUCC_CURRENT_ADD_NEW_CHAR",
        char: char
    } as const);
    export const FAILED_CURRENT_ADD_NEW_CHAR = () => ({
        type: "FAILED_CURRENT_ADD_NEW_CHAR",
    } as const);
}

export type ProfileActionsTypes =
    ReturnType<typeof ProfileActionsType.PROC_GET_CURRENT_GAMER> |
    ReturnType<typeof ProfileActionsType.SUCC_GET_CURRENT_GAMER> |
    ReturnType<typeof ProfileActionsType.FAILED_GET_CURRENT_GAMER> |

    ReturnType<typeof ProfileActionsType.PROC_GET_CURRENT_TAX> |
    ReturnType<typeof ProfileActionsType.SUCC_GET_CURRENT_TAX> |
    ReturnType<typeof ProfileActionsType.FAILED_GET_CURRENT_TAX> |

    ReturnType<typeof ProfileActionsType.PROC_GET_CURRENT_CHARACTERS> |
    ReturnType<typeof ProfileActionsType.SUCC_GET_CURRENT_CHARACTERS> |
    ReturnType<typeof ProfileActionsType.FAILED_GET_CURRENT_CHARACTERS> |

    ReturnType<typeof ProfileActionsType.PROC_CURRENT_DELETE_CHAR> |
    ReturnType<typeof ProfileActionsType.SUCC_CURRENT_DELETE_CHAR> |
    ReturnType<typeof ProfileActionsType.FAILED_CURRENT_DELETE_CHAR> |

    ReturnType<typeof ProfileActionsType.PROC_CURRENT_SET_MAIN> |
    ReturnType<typeof ProfileActionsType.SUCC_CURRENT_SET_MAIN> |
    ReturnType<typeof ProfileActionsType.FAILED_CURRENT_SET_MAIN> |

    ReturnType<typeof ProfileActionsType.PROC_CURRENT_ADD_NEW_CHAR> |
    ReturnType<typeof ProfileActionsType.SUCC_CURRENT_ADD_NEW_CHAR> |
    ReturnType<typeof ProfileActionsType.FAILED_CURRENT_ADD_NEW_CHAR>;