import { combineReducers } from 'redux';
import { SessionActionsType } from '../_actions';
import { session } from './session/session.reducer.ts';
import { guild } from './guild/guild.reducer.ts';
import { alerts } from './alert/alert.reducer.ts';
import { gamers } from './gamer/gamer.reducer.ts';

const appReducer = combineReducers({
    session,
    alerts,
    guild,
    gamers
});

export const rootReducer = (state, action) => {
    if (action.type === SessionActionsType.CLOSED_SESSION) {
        state = undefined
    }
    return appReducer(state, action)
}