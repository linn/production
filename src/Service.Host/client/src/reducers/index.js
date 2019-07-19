import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    ...sharedLibraryReducers
});

export default rootReducer;
