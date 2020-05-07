import { getResponse, catchHandle, errorHandle } from '../../_helpers';
import { BaseResponse, GuildInfo, GuildBalanceReport, Tariff, ITariff } from '..';
import { Config } from '../../core';
import { authService } from '..';

export class guildService {
    /**
     * Получить информацию о гильдии 
     */
    static async getGuild(): Promise<GuildInfo> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json',
                'accept': 'application/json',
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/current`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return new GuildInfo(data.id, data.name, data.status, data.recruitmentStatus,
                    Number(data.charactersCount),
                    Number(data.gamersCount));
            })
            .catch(catchHandle);
    }

    /**
     * Возвращает информацию о балансе гильдии
     */
    static async GetGuildBalanceReport(): Promise<GuildBalanceReport> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/current/balance`), requestOptions)
            .then<BaseResponse & any>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return new GuildBalanceReport(
                    Number(data.balance),
                    Number(data.expectedTaxAmount),
                    Number(data.taxAmount),
                    Number(data.activeLoansAmount),
                    Number(data.repaymentLoansAmount),
                    Number(data.gamersBalance));
            })
            .catch(catchHandle);
    }

    static async GetGuildTariffs(): Promise<Array<ITariff>> {
        let session = authService.getCurrentSession();
        const requestOptions: RequestInit = {
            method: 'GET',
            cache: 'no-cache',
            headers: {
                'Authorization': 'Bearer ' + session.sessionId
            }
        };
        return await fetch(Config.BuildUrl(`/Guilds/current/roles`), requestOptions)
            .then<BaseResponse & Array<any>>(getResponse)
            .then(data => {
                if ((data && data.type) || data.traceId) {
                    return errorHandle(data);
                }
                return data.map(_ => new Tariff(
                    _.guildId,
                    _.userRole,
                    _.loanTax,
                    _.expiredLoanTax,
                    _.tax
                ));
            })
            .catch(catchHandle);
    }
}
