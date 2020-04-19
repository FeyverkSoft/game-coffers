import { gamerService, GamerInfo, IGamersListView, GamerStatus, GamerRank, ILoanView, IPenaltyView } from '../../_services';
import { GamerActionsType } from './GamerActionsType';
import { alertInstance, ICallback } from '..';


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

    /**
     * Метод регистрирует нового пользователя в гильдии
     * костыльный метод
     * @param props 
     */
    addUser(props: { id: string, name: string, rank: GamerRank, status: GamerStatus, dateOfBirth: Date, login: string }): Function {
        return (dispatch: Function) => {
            gamerService.addUser(props.id, props.name, props.rank, props.status, props.dateOfBirth, props.login)
                .then(
                    data => {
                        dispatch(gamerInstance.getGamers({ dateMonth: new Date() }));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                    });
        }
    }


    /**
     * This method changed gamer status
     */
    setStatus(props: { userId: string; status: GamerStatus; }): Function {
        return (dispatch: Function) => {
            dispatch(request(props.userId));
            gamerService.setStatus(props.userId, props.status)
                .then(
                    data => {
                        dispatch(success(props.userId, props.status));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_SET_GAMER_STATUS, userId } }
        function success(userId: string, status: GamerStatus) { return { type: GamerActionsType.SUCC_SET_GAMER_STATUS, userId, status } }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_STATUS, userId } }
    }

    /**
     * This method changed gamer rank
     */
    setRank(props: { userId: string; rank: GamerRank; }): Function {
        return (dispatch: Function) => {
            dispatch(request(props.userId));
            gamerService.setRank(props.userId, props.rank)
                .then(
                    data => {
                        dispatch(success(props.userId, props.rank));
                        dispatch(gamerInstance.getGamers({ dateMonth: new Date() }));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(failure(props.userId));
                    });
        }
        function request(userId: string) { return { type: GamerActionsType.PROC_SET_GAMER_RANK, userId } }
        function success(userId: string, rank: GamerRank) { return { type: GamerActionsType.SUCC_SET_GAMER_RANK, userId, rank } }
        function failure(userId: string) { return { type: GamerActionsType.FAILED_SET_GAMER_RANK, userId } }
    }

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
    addPenalty(penalty: { userId: string; penaltyId: string, description: string, amount: number }): Function {
        return (dispatch: Function) => {
            dispatch(request(penalty.userId));
            gamerService.addPenalty(penalty.userId, penalty.penaltyId, penalty.amount, penalty.description)
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

    /**
     * отменяет займ если это возможно
     */
    cancelLoan(props: { loanId: string }): Function {
        return (dispatch: Function) => {
            dispatch(request(props.loanId));
            gamerService.cancelLoan(props.loanId)
                .then(
                    data => {
                        dispatch(success(props.loanId));
                    })
                .catch(
                    ex => {
                        dispatch(failure(props.loanId));
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request(loanId: string) { return { type: GamerActionsType.PROC_CANCEL_GAMER_LOAN, loanId } }
        function success(loanId: string) { return { type: GamerActionsType.SUCC_CANCEL_GAMER_LOAN, loanId } }
        function failure(loanId: string) { return { type: GamerActionsType.FAILED_CANCEL_GAMER_LOAN, loanId } }
    }


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