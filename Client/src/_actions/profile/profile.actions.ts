
import { GamerInfo } from '../../_services';
import { GamerActionsType } from '../gamer/GamerActionsType';
import { alertInstance, ICallback } from '..';
import { profileService } from '../../_services/profile/profile.service';

interface AddCharProps extends ICallback<any> {
    name: string;
    className: string;
    gamerId: string;
    isMain: boolean;
}

interface DeleteCharProps extends ICallback<any> {
    name: string;
    gamerId: string;
}


export class ProfileActions {
    /**
     * Метод добавляет игроку нового персонажа
     */
    AddCharacters(props: AddCharProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.gamerId));
            profileService.AddNewChar(props.name, props.className, props.isMain)
                .then(
                    data => {
                        dispatch(success(props.gamerId, props.name, props.className, props.isMain));
                        if (props.onSuccess)
                            props.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.gamerId));
                        dispatch(alertInstance.error(ex));
                        if (props.onFailure)
                            props.onFailure(ex);
                    });
        }
        function request(gamerId: string) { return { type: GamerActionsType.PROC_ADD_NEW_CHARS, gamerId } }
        function success(gamerId: string, name: string, className: string, isMain: boolean) { return { type: GamerActionsType.SUCC_ADD_NEW_CHARS, gamerId, name, className, isMain } }
        function failure(gamerId: string) { return { type: GamerActionsType.FAILED_ADD_NEW_CHARS, gamerId } }
    }

    /**
     * Метод удаляет у игрока персонажа
     */
    DeleteCharacters(props: DeleteCharProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.gamerId));
            profileService.DeleteChar(props.name)
                .then(
                    data => {
                        dispatch(success(props.gamerId, props.name));
                        if (props.onSuccess)
                            props.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.gamerId));
                        dispatch(alertInstance.error(ex));
                        if (props.onFailure)
                            props.onFailure(ex);
                    });
        }
        function request(gamerId: string) { return { type: GamerActionsType.PROC_DELETE_CHARS, gamerId } }
        function success(gamerId: string, name: string) { return { type: GamerActionsType.SUCC_DELETE_CHARS, gamerId, name } }
        function failure(gamerId: string) { return { type: GamerActionsType.FAILED_DELETE_CHARS, gamerId } }
    }

    /**
     * Возвращает информацию о текущем игроке
     */
    GetCurrentGamer(): Function {
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
        function request() { return { type: GamerActionsType.PROC_GET_CURRENT_GAMER } }
        function success(currentGamer: GamerInfo) { return { type: GamerActionsType.SUCC_GET_CURRENT_GAMER, currentGamer } }
        function failure() { return { type: GamerActionsType.FAILED_GET_CURRENT_GAMER } }
    }
}

export const profileInstance = new ProfileActions();