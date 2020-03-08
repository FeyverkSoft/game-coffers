
import { IProfile, ITax } from '../../_services';
import { ProfileActionsType } from '../profile/ProfileActionsType';
import { alertInstance, ICallback } from '..';
import { profileService } from '../../_services/profile/profile.service';
import { ICharacter } from '../../_services/profile/ICharacter';


export class ProfileActions {
    /**
     * Возвращает информацию о текущем игроке
     */
    Get(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            profileService.getCurrentGamer()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: ProfileActionsType.PROC_GET_PROFILE } }
        function success(profile: IProfile) { return { type: ProfileActionsType.SUCC_GET_PROFILE, profile } }
        function failure() { return { type: ProfileActionsType.FAILED_GET_PROFILE } }
    }

    /**
     * Возвращает текущий размер налога у пользователя, и тариф по которому расчитывался налог
     */
    GetTax(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            profileService.GetTax()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: ProfileActionsType.PROC_GET_TAX } }
        function success(tax: ITax) { return { type: ProfileActionsType.SUCC_GET_TAX, tax } }
        function failure() { return { type: ProfileActionsType.FAILED_GET_TAX } }
    }

    /**
     * Возвращает список персов игрока
     */
    GetChars(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            profileService.GetCharList()
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: ProfileActionsType.PROC_GET_CHARACTERS } }
        function success(chars: Array<ICharacter>) { return { type: ProfileActionsType.SUCC_GET_CHARACTERS, chars } }
        function failure() { return { type: ProfileActionsType.FAILED_GET_CHARACTERS } }
    }

    /**
     * Устанавливает перса как основу
     */
    SetMainChar(charId: string): Function {
        return (dispatch: Function) => {
            dispatch(request(charId));
            profileService.SetMainChar(charId)
                .then(
                    data => {
                        dispatch(success(charId));
                    })
                .catch(
                    ex => {
                        dispatch(failure(charId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(id: string) { return { type: ProfileActionsType.PROC_SET_MAIN, id } }
        function success(id: string) { return { type: ProfileActionsType.SUCC_SET_MAIN, id } }
        function failure(id: string) { return { type: ProfileActionsType.FAILED_SET_MAIN, id } }
    }
}

export const profileInstance = new ProfileActions();