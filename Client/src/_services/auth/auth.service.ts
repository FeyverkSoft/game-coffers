import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { SessionInfo, BaseResponse } from '.././entity';
import { store } from '../../_helpers';
import { Config } from '../../core';
import { Lang } from '../.';

export class authService {
    ///Возвращает текущую сессию пользователя
    static getCurrentSession(): SessionInfo {
        try {
            let { session } = store.getState();

            if (!session || !session.isActive()) {
                let tempSession: any = localStorage.getItem('session');
                if (tempSession)
                    return new SessionInfo(tempSession);
            }
            return session;
        }
        catch (ex) {
            console.warn(ex);
            return new SessionInfo();
        }
    };

    static async logIn(username: string, password: string): Promise<SessionInfo> {
        const requestOptions: RequestInit = {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ userName: username, password: password })
        };
        return fetch(Config.BuildUrl('/auth'), requestOptions)
            .then<BaseResponse & SessionInfo>(getResponse)
            .then((data: any) => {
                if (data && data.error)
                    throw new Error(Lang(data.error.code).format(data.error.detail || ''));
                let session: SessionInfo = new SessionInfo(data);
                localStorage.setItem('session', JSON.stringify(session));
                return data;

            })
            .catch(catchHandle);
    };

    static async logOut(): Promise<any> {
        let session = authService.getCurrentSession();
        if (session) {
            return fetch(`${Config.BuildUrl('/auth')}/${session.sessionId}`, { method: 'DELETE', headers: { 'token': session.sessionId } })
                .then<BaseResponse>(getResponse)
                .then((data: any) => {
                    authService.clearLocalSession();

                    if (data && data.error)
                        return errorHandle(data);

                    return data;
                })
                .catch(catchHandle);
        }
        return Promise.resolve();
    };

    static clearLocalSession(isException: boolean = false): Promise<any> {
        if (isException) {
            let session = authService.getCurrentSession();
            let currentDate: number = new Date().valueOf();
            if ((currentDate - session.initDate.valueOf()) / 1000 < 45) {//тайм-лаг для ассинхроных запросов
                return Promise.reject('{}');
            }
        }
        if (localStorage.removeItem && typeof (localStorage.removeItem) === 'function')
            localStorage.removeItem('session');
        return Promise.resolve('{}');
    };
}
