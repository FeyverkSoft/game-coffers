import { gamerService, GamerStatus, GamerRank } from '../../_services';
import { GamerActionsType } from './GamerActionsType';
import { alertInstance } from '../alert/alert.actions';


export class GamerActions {
    /**
     * Метод добавляет игроку нового персонажа
     */
    addCharacter(props: { userId: string, characterId: string, name: string, className: string, isMain: boolean }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_ADD_NEW_CHARS(props.userId));
            gamerService.addNewChar(props.userId, props.characterId, props.name, props.className, props.isMain)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_ADD_NEW_CHARS(props.userId, props.characterId, props.name, props.className, props.isMain));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.PROC_ADD_NEW_CHARS(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * Метод удаляет у игрока персонажа
     */
    deleteCharacter(props: { userId: string, characterId: string }): any {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_DELETE_CHARS(props.userId));
            gamerService.DeleteChar(props.userId, props.characterId)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_DELETE_CHARS(props.userId, props.characterId));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_DELETE_CHARS(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * This method return gamer list
     */
    getGamers(filter: { dateMonth: Date; gamerStatuses?: Array<GamerStatus> }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_GET_GUILD_GAMERS(filter.dateMonth));
            gamerService.GetGamers(filter.dateMonth, filter.gamerStatuses)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_GET_GUILD_GAMERS(filter.dateMonth, data));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_GET_GUILD_GAMERS(filter.dateMonth));
                        dispatch(alertInstance.error(ex));
                    });
        }
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
            dispatch(GamerActionsType.PROC_SET_GAMER_STATUS(props.userId));
            gamerService.setStatus(props.userId, props.status)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_SET_GAMER_STATUS(props.userId, props.status));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_SET_GAMER_STATUS(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * This method changed gamer rank
     */
    setRank(props: { userId: string; rank: GamerRank; }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_SET_GAMER_RANK(props.userId));
            gamerService.setRank(props.userId, props.rank)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_SET_GAMER_RANK(props.userId, props.rank));
                        dispatch(gamerInstance.getGamers({ dateMonth: new Date() }));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_SET_GAMER_RANK(props.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * This method add gamers loan
     */
    addLoan(loan: { userId: string; loanId: string, description: string, amount: number }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_ADD_GAMER_LOAN(loan.userId));
            gamerService.addLoan(loan.userId, loan.loanId, loan.amount, loan.description)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_ADD_GAMER_LOAN(loan.userId, {
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
                        dispatch(GamerActionsType.FAILED_ADD_GAMER_LOAN(loan.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * This method add gamers Penalty
     */
    addPenalty(penalty: { userId: string; penaltyId: string, description: string, amount: number }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_ADD_GAMER_PENALTY(penalty.userId));
            gamerService.addPenalty(penalty.userId, penalty.penaltyId, penalty.amount, penalty.description)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_ADD_GAMER_PENALTY(penalty.userId, {
                            id: data.id,
                            createDate: data.createDate,
                            amount: data.amount,
                            description: data.description,
                            penaltyStatus: data.penaltyStatus
                        }));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_ADD_GAMER_PENALTY(penalty.userId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    /**
     * отменяет займ если это возможно
     */
    cancelLoan(props: { loanId: string }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_CANCEL_GAMER_LOAN(props.loanId));
            gamerService.cancelLoan(props.loanId)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_CANCEL_GAMER_LOAN(props.loanId));
                    })
                .catch(
                    ex => {
                        dispatch(GamerActionsType.FAILED_CANCEL_GAMER_LOAN(props.loanId));
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    
    /**
     * Продляет займ если это возможно
     */
    prolongLoan(props: { loanId: string; }): Function {
        return (dispatch: Function) => {
            dispatch(GamerActionsType.PROC_PROLONG_GAMER_LOAN(props.loanId));
            gamerService.prolongLoan(props.loanId)
                .then(
                    data => {
                        dispatch(GamerActionsType.SUCC_PROLONG_GAMER_LOAN(props.loanId, {
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
                        dispatch(GamerActionsType.FAILED_PROLONG_GAMER_LOAN(props.loanId));
                        dispatch(alertInstance.error(ex));
                    });
        }
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