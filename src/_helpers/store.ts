import { createStore, applyMiddleware, Store } from 'redux';
import { rootReducer } from '../_reducers';
import thunkMiddleware from 'redux-thunk';
import { SessionInfo } from '../_services/entity';
import { AlertState } from '../_reducers/alert/alert.reducer';
import { Dictionary } from '../core';
import logger from 'redux-logger';

interface IUserSettings {
    [id: string]: any;
}

export interface IStore {
    alerts: AlertState;
    user: IUserSettings;
    [id: string]: any;
}

export const store = createStore<any, any, any, any>(
    rootReducer,
    applyMiddleware(
        thunkMiddleware,
        logger
    )
) as Store<IStore, any>