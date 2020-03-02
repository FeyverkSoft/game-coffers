import { ProfileActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import { IProfile, Profile } from "../../_services";
import clonedeep from 'lodash.clonedeep';

export class IProfileStore {
    profile: IProfile & IHolded = new Profile();
}

export function profile(state: IProfileStore = new IProfileStore(), action: IAction<ProfileActionsType>):
    IProfileStore {
    var clonedState = clonedeep(state);
    switch (action.type) {
        /**
        * Секция обработчика события получения профиля
        */
        case ProfileActionsType.PROC_GET_PROFILE: {
            clonedState.profile.holding = true;
            return clonedState;
        }
        case ProfileActionsType.SUCC_GET_PROFILE: {
            clonedState.profile = { ...clonedState.profile, ...action.profile, holding: false }
            return clonedState;
        }
        case ProfileActionsType.FAILED_GET_PROFILE: {
            clonedState.profile.holding = false;
            return clonedState;
        }

        default:
            return state
    }
}