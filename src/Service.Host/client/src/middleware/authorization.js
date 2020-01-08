import { RSAA } from 'redux-api-middleware';
import { getAccessToken } from '../selectors/getAccessToken';

export default ({ getState }) => next => action => {
    if (action[RSAA]) {
        const authAction = action[RSAA];
        if (authAction.options && authAction.options.requiresAuth) {
            authAction.headers = {
                Authorization: `Bearer ${getAccessToken(getState())}`,
                ...action[RSAA].headers
            };
        }
    }

    return next(action);
};
