import { ProfileActionsTypes } from "../../_actions";
import { IHolded } from "../../core";
import { IProfile, Profile, ITax, UserTax } from "../../_services";
import clonedeep from 'lodash.clonedeep';
import { ICharacter } from "../../_services/profile/ICharacter";

export class IProfileStore {
    profile: IProfile & IHolded = new Profile();
    tax: ITax & IHolded = new UserTax();
    characters: Array<ICharacter & IHolded> & IHolded = [];
}

export function profile(state: IProfileStore = new IProfileStore(), action: ProfileActionsTypes):
    IProfileStore {
    const clonedState = clonedeep(state);
    switch (action.type) {
        /**
        * Секция обработчика события получения профиля
        */
        case 'PROC_GET_CURRENT_GAMER': {
            clonedState.profile.holding = true;
            return clonedState;
        }
        case 'SUCC_GET_CURRENT_GAMER': {
            clonedState.profile = { ...clonedState.profile, ...action.profile, holding: false }
            return clonedState;
        }
        case 'FAILED_GET_CURRENT_GAMER': {
            clonedState.profile.holding = false;
            return clonedState;
        }

        /**
        * Секция обработчика события получения налога
        */
        case 'PROC_GET_CURRENT_TAX': {
            clonedState.tax.holding = true;
            return clonedState;
        }
        case 'SUCC_GET_CURRENT_TAX': {
            clonedState.tax = { ...clonedState.tax, ...action.tax, holding: false }
            return clonedState;
        }
        case 'FAILED_GET_CURRENT_TAX': {
            clonedState.tax.holding = false;
            return clonedState;
        }

        /**
        * Секция обработчика события получения списка персонажей
        */
        case 'PROC_GET_CURRENT_CHARACTERS': {
            clonedState.characters.holding = true;
            return clonedState;
        }
        case 'SUCC_GET_CURRENT_CHARACTERS': {
            clonedState.characters = [...action.chars];
            clonedState.characters.holding = false;
            return clonedState;
        }
        case 'FAILED_GET_CURRENT_CHARACTERS': {
            clonedState.characters.holding = false;
            return clonedState;
        }

        /**
        * Секция обработчика события установки перса как основы
        */
        case 'PROC_CURRENT_SET_MAIN': {
            clonedState.characters.forEach(ch => {
                if (ch.id === action.id) {
                    ch.holding = true;
                }
            });
            return clonedState;
        }
        case 'SUCC_CURRENT_SET_MAIN': {
            clonedState.characters.forEach(ch => {
                if (ch.id === action.id) {
                    ch.isMain = true;
                    ch.holding = false;
                    clonedState.profile.characterName = ch.name;
                }
                else
                    ch.isMain = false;
            });
            return clonedState;
        }
        case 'FAILED_CURRENT_SET_MAIN': {
            clonedState.characters.forEach(ch => {
                if (ch.id === action.id) {
                    ch.holding = false;
                }
            });
            return clonedState;
        }

        /**
        * Секция обработчика события удаления перса
        */
        case 'PROC_CURRENT_DELETE_CHAR': {
            clonedState.characters.forEach(ch => {
                if (ch.id === action.id) {
                    ch.holding = true;
                }
            });
            return clonedState;
        }
        case 'SUCC_CURRENT_DELETE_CHAR': {

            if (clonedState.characters.filter(ch => ch.id === action.id)[0].name === clonedState.profile.characterName)
                clonedState.profile.characterName = '';
            clonedState.characters = clonedState.characters.filter(ch => ch.id !== action.id);
            clonedState.profile.charCount--;

            return clonedState;
        }
        case 'FAILED_CURRENT_DELETE_CHAR': {
            clonedState.characters.forEach(ch => {
                if (ch.id === action.id) {
                    ch.holding = false;
                }
            });
            return clonedState;
        }


        /**
        * Секция обработчика события добавления нового персонажа
        */
        case 'PROC_CURRENT_ADD_NEW_CHAR': {
            clonedState.characters.holding = true;
            return clonedState;
        }
        case 'SUCC_CURRENT_ADD_NEW_CHAR': {
            clonedState.characters.holding = false;
            clonedState.profile.charCount++;
            clonedState.characters.push(action.char);
            if (action.char.isMain) {
                clonedState.characters.forEach(ch => {
                    if (ch.id === action.char.id) {
                        ch.isMain = true;
                    } else {
                        ch.isMain = false;
                    }
                });
            }
            return clonedState;
        }
        case 'FAILED_CURRENT_ADD_NEW_CHAR': {
            clonedState.characters.holding = false;
            return clonedState;
        }

        default:
            return state
    }
}