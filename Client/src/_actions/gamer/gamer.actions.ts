
import { gamerService, GamerInfo, IGamersListView, GamerStatus, GamerRank } from '../../_services';
import { GamerActionsType } from './GamerActionsType';
import { alertInstance, ICallback } from '..';

interface GetGuildGamersProps extends ICallback<any> {
    guildId: string;
    dateFrom?: Date | string;
    dateTo?: Date | string;
    gamerStatuses?: Array<GamerStatus>
}
interface SetRankProps extends ICallback<any> {
    gamerId: string;
    rank: GamerRank;
}
interface SetStatusProps extends ICallback<any> {
    gamerId: string;
    status: GamerStatus;
}

interface AddCharProps extends ICallback<any> {
    gamerId: string;
    name: string;
    className: string;
}

interface DeleteCharProps extends ICallback<any> {
    gamerId: string;
    name: string;
}
export class GamerActions {
    /**
     * Метод добавляет игроку нового персонажа
     */
    AddCharacters(props: AddCharProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.gamerId));
            gamerService.AddNewChar(props.gamerId, props.name, props.className)
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
        function request(gamerId: string) { return { type: GamerActionsType.PROC_ADD_NEW_CHARS, gamerId } }
        function success(gamerId: string, name: string) { return { type: GamerActionsType.SUCC_ADD_NEW_CHARS, gamerId, name } }
        function failure(gamerId: string) { return { type: GamerActionsType.FAILED_ADD_NEW_CHARS, gamerId } }
    }

    /**
     * Метод удаляет у игрока персонажа
     */
    DeleteCharacters(props: DeleteCharProps): Function {
        return (dispatch: Function) => {
            dispatch(request(props.gamerId));
            gamerService.DeleteChar(props.gamerId, props.name)
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
            gamerService.getCurrentGamer()
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

    /**
     * This method return gamer list
     */
    GetGamers(filter: GetGuildGamersProps): Function {
        return (dispatch: Function) => {
            dispatch(request());
            gamerService.GetGamers(filter.guildId)
                .then(
                    data => {
                        dispatch(success(data));
                        if (filter.onSuccess)
                            filter.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                        if (filter.onFailure)
                            filter.onFailure(ex);
                    });
        }
        function request() { return { type: GamerActionsType.PROC_GET_GUILD_GAMERS } }
        function success(GamersList: Array<IGamersListView>) { return { type: GamerActionsType.SUCC_GET_GUILD_GAMERS, GamersList } }
        function failure() { return { type: GamerActionsType.FAILED_GET_GUILD_GAMERS } }
    }

    /**
     * This method changed gamer status
     */
    SetStatus(status: SetStatusProps): Function {
        return (dispatch: Function) => {
            dispatch(request(status.gamerId));
            gamerService.SetStatus(status.gamerId, status.status)
                .then(
                    data => {
                        dispatch(success(status.gamerId, status.status));
                        if (status.onSuccess)
                            status.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(status.gamerId));
                        dispatch(alertInstance.error(ex));
                        if (status.onFailure)
                            status.onFailure(ex);
                    });
        }
        function request(gamerId: string) { return { type: GamerActionsType.PROC_SET_GAMER_STATUS, gamerId } }
        function success(gamerId: string, status: GamerStatus) { return { type: GamerActionsType.SUCC_SET_GAMER_STATUS, gamerId, status } }
        function failure(gamerId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_STATUS, gamerId } }
    }

    /**
     * This method changed gamer rank
     */
    SetRank(rank: SetRankProps): Function {
        return (dispatch: Function) => {
            dispatch(request(rank.gamerId));
            gamerService.SetRank(rank.gamerId, rank.rank)
                .then(
                    data => {
                        dispatch(success(rank.gamerId, rank.rank));
                        if (rank.onSuccess)
                            rank.onSuccess(data);
                    })
                .catch(
                    ex => {
                        dispatch(failure(rank.gamerId));
                        dispatch(alertInstance.error(ex));
                        if (rank.onFailure)
                            rank.onFailure(ex);
                    });
        }
        function request(gamerId: string) { return { type: GamerActionsType.PROC_SET_GAMER_RANK, gamerId } }
        function success(gamerId: string, rank: GamerRank) { return { type: GamerActionsType.SUCC_SET_GAMER_RANK, gamerId, rank } }
        function failure(gamerId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_RANK, gamerId } }
    }
}

export const gamerInstance = new GamerActions();