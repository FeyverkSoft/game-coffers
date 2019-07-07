import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { SessionInfo, BaseResponse } from '../.';
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
                'Content-Type': 'application/json',
                'accept': 'application/json'
            },
            body: JSON.stringify({ login: username, password: password })
        };
        return fetch(Config.BuildUrl('/Session'), requestOptions)
            .then<BaseResponse & SessionInfo>(getResponse)
            .then((data: any) => {
                if (data && data.type || data.traceId){
                    authService.clearLocalSession();
                    throw new Error(Lang(data.type).format(data.errors || ''));
                }
                let session: SessionInfo = new SessionInfo(data);
                localStorage.setItem('session', JSON.stringify(session));
                localStorage.setItem('Coffer_apiUrl', Config.GetUrl());
                return data;

            })
            .catch(catchHandle);
    };

    static async logOut(): Promise<any> {
        let session = authService.getCurrentSession();
        if (session) {
            return fetch(`${Config.BuildUrl('/session')}/${session.sessionId}`, { method: 'DELETE', headers: { 'token': session.sessionId } })
                .then<BaseResponse>(getResponse)
                .then((data: any) => {
                    authService.clearLocalSession();

                    if (data && data.type || data.traceId)
                        return errorHandle(data);

                    return data;
                })
                .catch(catchHandle);
        }
        return Promise.resolve();
    };

    static clearLocalSession(isException: boolean = false): Promise<any> {
        if (localStorage.removeItem && typeof (localStorage.removeItem) === 'function')
            localStorage.removeItem('session');
        return Promise.resolve('{}');
    };
}
