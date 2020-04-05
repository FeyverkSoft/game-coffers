import { gamerService, GamerInfo, IGamersListView, GamerStatus, GamerRank, ILoanView, IPenaltyView } from '../../_services';
import { GamerActionsType } from './GamerActionsType';
import { alertInstance, ICallback } from '..';

interface SetRankProps extends ICallback<any> {
    gamerId: string;
    rank: GamerRank;
}
interface SetStatusProps extends ICallback<any> {
    gamerId: string;
    status: GamerStatus;
}

interface CancelLoanProps extends ICallback<any> {
    gamerId: string;
    id: string,
}
interface CancelPenaltyProps extends ICallback<any> {
    gamerId: string;
    id: string,
}

export class GamerActions {

    /**
     * Метод добавляет игроку нового персонажа
     */
    addCharacter(props: { userId: string, characterId: string, name: string, className: string, isMain: boolean }): Function {
        return (dispatch: Function) => {
            dispatch(request(props.userId));
            gamerService.addNewChar(props.userId, props.characterId, props.name, props.className, props.isMain)
                .then(
                    data => {
                        dispatch(success(props.userId, props.characterId, props.name, props.className, props.isMain));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_ADD_NEW_CHARS, userId } }
        function success(userId: string, characterId: string, name: string, className: string, isMain: boolean) {
            return { type: GamerActionsType.SUCC_ADD_NEW_CHARS, userId, name, className, characterId, isMain }
        }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_ADD_NEW_CHARS, userId } }
    }

    /**
     * Метод удаляет у игрока персонажа
     */
    deleteCharacter(props: { userId: string, characterId: string }): any {
        return (dispatch: Function) => {
            dispatch(request(props.userId));
            gamerService.DeleteChar(props.userId, props.characterId)
                .then(
                    data => {
                        dispatch(success(props.userId, props.characterId));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_DELETE_CHARS, userId } }
        function success(userId: string, characterId: string) { return { type: GamerActionsType.SUCC_DELETE_CHARS, userId, characterId } }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_DELETE_CHARS, userId } }
    }

    /**
     * This method return gamer list
     */
    getGamers(filter: { dateMonth: Date; gamerStatuses?: Array<GamerStatus> }): Function {
        return (dispatch: Function) => {
            dispatch(request(filter.dateMonth));
            gamerService.GetGamers(filter.dateMonth, filter.gamerStatuses)
                .then(
                    data => {
                        dispatch(success(data, filter.dateMonth));
                    })
                .catch(
                    ex => {
                        dispatch(failure(filter.dateMonth));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(date: Date) { return { type: GamerActionsType.PROC_GET_GUILD_GAMERS, date } }
        function success(gamersList: Array<IGamersListView>, date: Date) { return { type: GamerActionsType.SUCC_GET_GUILD_GAMERS, gamersList, date } }
        function failure(date: Date) { return { type: GamerActionsType.FAILED_GET_GUILD_GAMERS, date } }
    }

    // /**
    //  * Метод регистрирует нового пользователя в гильдии
    //  * костыльный метод
    //  * @param props 
    //  */
    // AddUser(props: AddUserProps): Function {
    //     return (dispatch: Function) => {
    //         gamerService.AddUser(props.guildId, props.id, props.name,
    //             props.rank, props.status, props.dateOfBirth, props.login)
    //             .then(
    //                 data => {
    //                     if (props.onSuccess)
    //                         props.onSuccess(data);
    //                     dispatch(gamerInstance.GetGamers({ dateMonth: new Date() }));
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(alertInstance.error(ex));
    //                     if (props.onFailure)
    //                         props.onFailure(ex);
    //                 });
    //     }
    // }


    // /**
    //  * This method changed gamer status
    //  */
    // SetStatus(status: SetStatusProps): Function {
    //     return (dispatch: Function) => {
    //         dispatch(request(status.gamerId));
    //         gamerService.SetStatus(status.gamerId, status.status)
    //             .then(
    //                 data => {
    //                     dispatch(success(status.gamerId, status.status));
    //                     if (status.onSuccess)
    //                         status.onSuccess(data);
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(failure(status.gamerId));
    //                     dispatch(alertInstance.error(ex));
    //                     if (status.onFailure)
    //                         status.onFailure(ex);
    //                 });
    //     }
    //     function request(gamerId: string) { return { type: GamerActionsType.PROC_SET_GAMER_STATUS, gamerId } }
    //     function success(gamerId: string, status: GamerStatus) { return { type: GamerActionsType.SUCC_SET_GAMER_STATUS, gamerId, status } }
    //     function failure(gamerId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_STATUS, gamerId } }
    // }

    // /**
    //  * This method changed gamer rank
    //  */
    // SetRank(rank: SetRankProps): Function {
    //     return (dispatch: Function) => {
    //         dispatch(request(rank.gamerId));
    //         gamerService.SetRank(rank.gamerId, rank.rank)
    //             .then(
    //                 data => {
    //                     dispatch(success(rank.gamerId, rank.rank));
    //                     if (rank.onSuccess)
    //                         rank.onSuccess(data);
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(failure(rank.gamerId));
    //                     dispatch(alertInstance.error(ex));
    //                     if (rank.onFailure)
    //                         rank.onFailure(ex);
    //                 });
    //     }
    //     function request(gamerId: string) { return { type: GamerActionsType.PROC_SET_GAMER_RANK, gamerId } }
    //     function success(gamerId: string, rank: GamerRank) { return { type: GamerActionsType.SUCC_SET_GAMER_RANK, gamerId, rank } }
    //     function failure(gamerId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_RANK, gamerId } }
    // }

    /**
     * This method add gamers loan
     */
    addLoan(loan: { userId: string; loanId: string, description: string, amount: number }): Function {
        return (dispatch: Function) => {
            dispatch(request(loan.userId));
            gamerService.addLoan(loan.userId, loan.loanId, loan.amount, loan.description)
                .then(
                    data => {
                        dispatch(success(loan.userId, {
                            id: String(data.id),
                            amount: Number(data.amount),
                            balance: Number(data.balance),
                            createDate: new Date(data.createDate),
                            expiredDate: new Date(data.expiredDate),
                            description: String(data.description),
                            loanStatus: data.loanStatus
                        }));
                    })
                .catch(
                    ex => {
                        dispatch(failure(loan.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_ADD_GAMER_LOAN, userId } }
        function success(userId: string, loan: ILoanView) { return { type: GamerActionsType.SUCC_ADD_GAMER_LOAN, userId, loan } }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_ADD_GAMER_LOAN, userId } }
    }

    /**
     * This method add gamers Penalty
     */
    addPenalty(penalty: { userId: string; id: string, description: string, amount: number }): Function {
        return (dispatch: Function) => {
            dispatch(request(penalty.userId));
            gamerService.addPenalty(penalty.userId, penalty.id, penalty.amount, penalty.description)
                .then(
                    data => {
                        dispatch(success(penalty.userId, {
                            id: data.id,
                            createDate: data.createDate,
                            amount: data.amount,
                            description: data.description,
                            penaltyStatus: data.penaltyStatus
                        }));
                    })
                .catch(
                    ex => {
                        dispatch(failure(penalty.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_ADD_GAMER_PENALTY, userId } }
        function success(userId: string, penalty: IPenaltyView) { return { type: GamerActionsType.SUCC_ADD_GAMER_PENALTY, userId, penalty } }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_ADD_GAMER_PENALTY, userId } }
    }

    // /**
    //  * отменяет займ если это возможно
    //  */
    // CancelLoan(props: CancelLoanProps): Function {
    //     return (dispatch: Function) => {
    //         dispatch(request(props.gamerId));
    //         gamerService.CancelLoan(props.gamerId, props.id)
    //             .then(
    //                 data => {
    //                     dispatch(success(props.gamerId, props.id));
    //                     if (props.onSuccess)
    //                         props.onSuccess(data);
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(failure(props.gamerId));
    //                     dispatch(alertInstance.error(ex));
    //                     if (props.onFailure)
    //                         props.onFailure(ex);
    //                 });
    //     }
    //     function request(gamerId: string) { return { type: GamerActionsType.PROC_CANCEL_GAMER_LOAN, gamerId } }
    //     function success(gamerId: string, loanId: string) { return { type: GamerActionsType.SUCC_CANCEL_GAMER_LOAN, gamerId, loanId } }
    //     function failure(gamerId: string) { return { type: GamerActionsType.FAILED_CANCEL_GAMER_LOAN, gamerId } }
    // }

    // /**
    //  * Красное сторно займа, если это возожно
    //  */
    // ReverseLoan(props: CancelLoanProps): Function {
    //     return (dispatch: Function) => {
    //         dispatch(request(props.gamerId));
    //         gamerService.ReverseLoan(props.gamerId, props.id)
    //             .then(
    //                 data => {
    //                     dispatch(success(props.gamerId, props.id));
    //                     if (props.onSuccess)
    //                         props.onSuccess(data);
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(failure(props.gamerId));
    //                     dispatch(alertInstance.error(ex));
    //                     if (props.onFailure)
    //                         props.onFailure(ex);
    //                 });
    //     }
    //     function request(gamerId: string) { return { type: GamerActionsType.PROC_CANCEL_GAMER_LOAN, gamerId } }
    //     function success(gamerId: string, loanId: string) { return { type: GamerActionsType.SUCC_CANCEL_GAMER_LOAN, gamerId, loanId } }
    //     function failure(gamerId: string) { return { type: GamerActionsType.FAILED_CANCEL_GAMER_LOAN, gamerId } }
    // }

    // /**
    //  * отменяет штраф если это возможно
    //  */
    // CancelPenalty(props: CancelPenaltyProps): Function {
    //     return (dispatch: Function) => {
    //         dispatch(request(props.gamerId));
    //         gamerService.CancelPenalty(props.gamerId, props.id)
    //             .then(
    //                 data => {
    //                     dispatch(success(props.gamerId, props.id));
    //                     if (props.onSuccess)
    //                         props.onSuccess(data);
    //                 })
    //             .catch(
    //                 ex => {
    //                     dispatch(failure(props.gamerId));
    //                     dispatch(alertInstance.error(ex));
    //                     if (props.onFailure)
    //                         props.onFailure(ex);
    //                 });
    //     }
    //     function request(gamerId: string) { return { type: GamerActionsType.PROC_CANCEL_GAMER_PENALTY, gamerId } }
    //     function success(gamerId: string, penaltyId: string) { return { type: GamerActionsType.SUCC_CANCEL_GAMER_PENALTY, gamerId, penaltyId } }
    //     function failure(gamerId: string) { return { type: GamerActionsType.FAILED_CANCEL_GAMER_PENALTY, gamerId } }
    // }
}

export const gamerInstance = new GamerActions();