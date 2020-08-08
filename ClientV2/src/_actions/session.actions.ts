import { history } from '../_helpers';
import { authService } from "../_services";
import { alertInstance } from './alert/alert.actions';
import { SessionActionsType } from './SessionActionsType';

export class SessionActions {

    /**
     * проверка кода на емайле
     * @param {*} username 
     * @param {*} password 
     */
    checkCode(props: { code: string; }): Function {
        return (dispatch: Function) => {
            dispatch(SessionActionsType.CHECK_CODE_PROCESS());
            authService.checkCode(props.code)
                .then(
                    data => {
                        dispatch(SessionActionsType.CHECK_CODE_SUCCESS());
                        history.push('/');
                    })
                .catch(
                    ex => {
                        dispatch(SessionActionsType.CHECK_CODE_FAILURE());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Авторизировать пользователя по логину и парлю
     * @param {*} username 
     * @param {*} password 
     */
    logIn(username: string, password: string): Function {
        return (dispatch: Function) => {
            dispatch(SessionActionsType.CREATE_SESSION());
            authService.logIn(username, password)
                .then(
                    data => {
                        dispatch(SessionActionsType.CREATE_SESSION_SUCCESS(data));
                        history.push('/');
                    })
                .catch(
                    ex => {
                        dispatch(SessionActionsType.CREATE_SESSION_FAILURE());
                        this.clearLocalSession();
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * зарегистрировать
     */
    reg(props: { id: string; guildId: string; username: string; email: string; password: string; }): Function {
        return (dispatch: Function) => {
            dispatch(SessionActionsType.CREATE_NEW_USER());
            authService.reg(props.id, props.guildId, props.username, props.email, props.password)
                .then(
                    data => {
                        dispatch(SessionActionsType.CREATE_NEW_USER_SUCCESS());
                        history.push('/');
                    })
                .catch(
                    ex => {
                        dispatch(SessionActionsType.CREATE_NEW_USER_FAILURE());
                        this.clearLocalSession();
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
    * Авторизировать пользователя по логину и парлю
    * @param {*} username 
    * @param {*} password 
    */
    logInByEmail(guildId: string, email: string, password: string): Function {
        return (dispatch: Function) => {
            dispatch(SessionActionsType.CREATE_SESSION());
            authService.logInByEmail(guildId, email, password)
                .then(
                    data => {
                        dispatch(SessionActionsType.CREATE_SESSION_SUCCESS(data));
                        history.push('/');
                    })
                .catch(
                    ex => {
                        dispatch(SessionActionsType.CREATE_SESSION_FAILURE());
                        this.clearLocalSession();
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Завершить сессию пользователя
     */
    logOut(): Function {
        return (dispatch: Function) => {
            dispatch(SessionActionsType.CLOSING_SESSION());
            authService.logOut()
                .then(
                    data => {
                        dispatch(SessionActionsType.CLOSED_SESSION());
                        this.clearLocalSession();
                    })
                .catch(
                    error => {
                        dispatch(alertInstance.error(error));
                        dispatch(SessionActionsType.CLOSED_SESSION());
                    });
        }
    }

    /**
     * Очистить локальную сессию пользователя, при этом на удалёном сервере сессия не будет завершена
     */
    clearLocalSession(isException: boolean = false): any {
        history.push('/');
        return (dispatch: Function) => {
            authService.clearLocalSession(isException)
                .then(x => {
                    dispatch(SessionActionsType.CLOSED_SESSION())
                }).catch(e => {
                    console.log(e);
                    dispatch(SessionActionsType.CLOSED_SESSION())
                });
        }
    }

}

export const sessionInstance = new SessionActions();