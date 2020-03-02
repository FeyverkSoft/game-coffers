import { createStore, applyMiddleware, Store } from 'redux';
import { rootReducer } from '../_reducers';
import thunkMiddleware from 'redux-thunk';
import { AlertState } from '../_reducers/alert/alert.reducer';
import { Dictionary } from '../core';
import logger from 'redux-logger';
import { IGuildStore } from '../_reducers/guild/guild.reducer';
import { IGamerStore } from '../_reducers/gamer/gamer.reducer';
import { IProfileStore } from '../_reducers/profile/profile.reducer';
import { SessionInfo } from '../_services';
import { IOperationsStore } from '../_reducers/operation/operations.reducer';

export interface IStore extends Dictionary<any> {
    alerts: AlertState;
    profile: IProfileStore;
    gamers: IGamerStore;
    guild: IGuildStore;
    session: SessionInfo;
    operations: IOperationsStore;
}
let _store;
if (process.env.NODE_ENV !== 'production') {
    _store = createStore<any, any, any, any>(
        rootReducer,
        applyMiddleware(
            thunkMiddleware,
            logger
        )
    ) as Store<IStore, any>
} else {
    _store = createStore<any, any, any, any>(
        rootReducer,
        applyMiddleware(
            thunkMiddleware,
        )
    ) as Store<IStore, any>
}

export const store = _store;