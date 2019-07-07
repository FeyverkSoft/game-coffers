import { createStore, applyMiddleware, Store } from 'redux';
import { rootReducer } from '../_reducers';
import thunkMiddleware from 'redux-thunk';
import { AlertState } from '../_reducers/alert/alert.reducer';
import { Dictionary } from '../core';
import logger from 'redux-logger';
import { IGuildStore } from '../_reducers/guild/guild.reducer';
import { SessionInfo } from '../_services';

interface IGamerStore {
    [id: string]: any;
}

export interface IStore extends Dictionary<any> {
    alerts: AlertState;
    user: IGamerStore;
    guild: IGuildStore;
    session: SessionInfo;
}

export const store = createStore<any, any, any, any>(
    rootReducer,
    applyMiddleware(
        thunkMiddleware,
        logger
    )
) as Store<IStore, any>