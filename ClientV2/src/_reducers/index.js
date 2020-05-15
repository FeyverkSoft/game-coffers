import { combineReducers } from 'redux';
import { session } from './session/session.reducer.ts';
import { guild } from './guild/guild.reducer.ts';
import { alerts } from './alert/alert.reducer.ts';
import { profile } from './profile/profile.reducer.ts';
import { gamers } from './gamer/gamer.reducer.ts';
import { operations } from './operation/operations.reducer.ts';

const appReducer = combineReducers({
    session,
    profile,
    alerts,
    guild,
    gamers,
    operations
});

export const rootReducer = (state, action) => {
    if (action.type === 'CLOSED_SESSION') {
        state = undefined
    }
    return appReducer(state, action)
};