import { combineReducers } from 'redux';
import { session } from './session/session.reducer.ts';
import { guild } from './guild/guild.reducer.ts';
import { alerts } from './alert/alert.reducer.ts';
import { profile } from './profile/profile.reducer.ts';
import { gamers } from './gamer/gamer.reducer.ts';
import { operations } from './operation/operations.reducer.ts';
import { nests } from './nests/nest.reducer.ts';

const appReducer = combineReducers({
    session,
    profile,
    alerts,
    guild,
    gamers,
    operations,
    nests
});

export const rootReducer = (state, action) => {
    if (action.type === 'CLOSED_SESSION') {
        state = undefined
    }
    return appReducer(state, action)
};