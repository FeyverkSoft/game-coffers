import { history } from '../_helpers';
import { authService, SessionInfo } from "../_services";
import { alertInstance, SessionActionsType } from '.';

export class SessionActions {
    /**
     * Авторизировать пользователя по логину и парлю
     * @param {*} username 
     * @param {*} password 
     */
    logIn(username: string, password: string): Function {
        return (dispatch: Function) => {
            dispatch(request());
            authService.logIn(username, password)
                .then(
                    data => {
                        dispatch(success(data));
                        history.push('/');
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        this.clearLocalSession();
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: SessionActionsType.CREATE_SESSION } }
        function success(session: SessionInfo) { return { type: SessionActionsType.CREATE_SESSION_SUCCESS, session } }
        function failure() { return { type: SessionActionsType.CREATE_SESSION_FAILURE } }
    }
    /**
     * Завершить сессию пользователя
     */
    logOut(): Function {
        return (dispatch: Function) => {
            dispatch(request());
            authService.logOut()
                .then(
                    data => {
                        dispatch(success());
                        this.clearLocalSession();
                        history.push('/');
                    })
                .catch(
                    error => {
                        dispatch(alertInstance.error(error));
                        dispatch(success());
                    });
        }
        function request() { return { type: SessionActionsType.CLOSING_SESSION } }
        function success() { return { type: SessionActionsType.CLOSED_SESSION } }
    }
    /**
     * Очистить локальную сессию пользователя, при этом на удалёном сервере сессия не будет завершена
     */
    clearLocalSession(isException: boolean = false): any {
        return (dispatch: Function) => {
            authService.clearLocalSession(isException)
                .then(x => {
                    dispatch({ type: SessionActionsType.CLOSED_SESSION })
                }).catch(e => {
                    console.log(e);
                    dispatch({ type: SessionActionsType.CLOSED_SESSION })
                });
        }
    }

}

export const sessionInstance = new SessionActions();