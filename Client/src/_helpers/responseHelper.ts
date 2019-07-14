import { Lang } from '../_services';
import { sessionInstance } from '../_actions';
import { store } from '.';


const HttpStatusDecode = (code: number | string): string => {
    switch (code) {
        case 400:
            return 'Bad Request';
        case 401:
            return 'Unauthorized';
        case 403:
            return 'Forbidden';
        case 404:
            return 'Not Found';
        case 500:
            return 'Internal Server Error';
        default:
            return code.toString();
    }
}
export function getResponse<T=any>(response: Response): Promise<T> {
    let status: number | string = response.statusText || response.status;
    try {
        return response.json();
    } catch (error) {
        return Promise.reject(HttpStatusDecode(status));
    }
}

export const catchHandle = (ex: any): Promise<any> => Promise.reject(ex.message || ex.code || ex);

export const errorHandle = (data: any): Promise<any> => {
    if (data && data.types || data.traceId) {
        if (data.type|| data.traceId) {
            let error: string = Lang(data.type).format(data.errors || data.title || '');
            if (data.type == 'unauthorized') {
                try {
                    store.dispatch(sessionInstance.clearLocalSession(true));
                    return Promise.reject(data.type);
                }
                catch (ex) {
                    console.debug(ex);
                    return Promise.reject('{}');
                }
            }
            return Promise.reject(error);
        }
        return Promise.reject(data.errors);
    }
    return Promise.resolve();
}