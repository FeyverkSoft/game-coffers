import { combineReducers } from 'redux';
import { SessionActionsType } from '../_actions';
import { session } from './session/session.reducer.ts';
import { guild } from './guild/guild.reducer.ts';
import { alerts } from './alert/alert.reducer.ts';

const appReducer = combineReducers({
    session,
    alerts,
    guild
});

export const rootReducer = (state, action) => {
    if (action.type === SessionActionsType.CLOSED_SESSION) {
        state = undefined
    }
    return appReducer(state, action)
}