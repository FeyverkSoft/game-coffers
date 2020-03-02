
import { IProfile } from '../../_services';
import { ProfileActionsType } from '../profile/ProfileActionsType';
import { alertInstance, ICallback } from '..';
import { profileService } from '../../_services/profile/profile.service';


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
}

export const profileInstance = new ProfileActions();