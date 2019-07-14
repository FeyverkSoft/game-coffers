import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, IGamerInfo, GamerInfo } from '..';
import { Config } from '../../core';
import { authService } from '..';

export class gamerService {

    /**
     * Получить информацию об игроке текущем владельце сессии
     */
    static async getCurrentGamer(): Promise<IGamerInfo> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Gamers/current`), requestOptions)
            .then<BaseResponse & IGamerInfo>(getResponse)
            .then(data => {
                if (data && data.type || data.traceId) {
                    return errorHandle(data);
                }
                return new GamerInfo(data.userId, data.name, data.balance || 0, data.activeLoanAmount || 0,
                    data.activePenaltyAmount || 0, data.rank, data.charCount || 0);
            })
            .catch(catchHandle);
    }
}
