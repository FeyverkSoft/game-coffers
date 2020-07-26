
import { ProfileActionsType } from '../profile/ProfileActionsType';
import { alertInstance } from '../alert/alert.actions';
import { profileService } from '../../_services/profile/profile.service';
import { Character } from '../../_services/profile/ICharacter';


export class ProfileActions {
    /**
     * Возвращает информацию о текущем игроке
     */
    Get(): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_GET_CURRENT_GAMER());
            profileService.getCurrentGamer()
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_GET_CURRENT_GAMER(data));
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_GET_CURRENT_GAMER());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Возвращает текущий размер налога у пользователя, и тариф по которому расчитывался налог
     */
    GetTax(): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_GET_CURRENT_TAX());
            profileService.GetTax()
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_GET_CURRENT_TAX(data));
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_GET_CURRENT_TAX());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Возвращает список персов игрока
     */
    GetChars(): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_GET_CURRENT_CHARACTERS());
            profileService.GetCharList()
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_GET_CURRENT_CHARACTERS(data));
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_GET_CURRENT_CHARACTERS());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Устанавливает перса как основу
     */
    SetMainChar(charId: string): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_CURRENT_SET_MAIN(charId));
            profileService.SetMainChar(charId)
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_CURRENT_SET_MAIN(charId));
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_CURRENT_SET_MAIN(charId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Удалить выбранного персонажа
     */
    DeleteChar(charId: string): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_CURRENT_DELETE_CHAR(charId));
            profileService.DeleteChar(charId)
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_CURRENT_DELETE_CHAR(charId));
                        dispatch(profileInstance.GetTax());
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_CURRENT_DELETE_CHAR(charId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Метод регистрации нового персонажа у игрока
     * @param name 
     * @param className 
     * @param isMain 
     */
    AddChar(props: { id: string, name: string, className: string, isMain: boolean }): Function {
        return (dispatch: Function) => {
            dispatch(ProfileActionsType.PROC_CURRENT_ADD_NEW_CHAR());
            profileService.AddNewChar(props.id, props.name, props.className, props.isMain)
                .then(
                    data => {
                        dispatch(ProfileActionsType.SUCC_CURRENT_ADD_NEW_CHAR(new Character(props.id, props.name, props.className, props.isMain)));
                        dispatch(profileInstance.GetTax());
                    })
                .catch(
                    ex => {
                        dispatch(ProfileActionsType.FAILED_CURRENT_ADD_NEW_CHAR());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }
}

export const profileInstance = new ProfileActions();