import { RSAA } from 'redux-api-middleware';
import { getAccessToken } from '../selectors/getAccessToken';
import userManager from '../helpers/userManager';

export default ({ getState }) => next => action => {
    if (action[RSAA]) {
        if (action[RSAA].options && action[RSAA].options.requiresAuth) {
            action[RSAA].headers = {
                Authorization: `Bearer ${getAccessToken(getState())}`,
                ...action[RSAA].headers
            };
        }
    }

    if (action.type === 'redux-oidc/USER_SIGNED_OUT') {
        userManager.signinRedirect({
            data: { redirect: window.location.pathname + window.location.search }
        });
    }

    return next(action);
};
