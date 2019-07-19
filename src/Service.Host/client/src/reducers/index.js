import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';

const rootReducer = combineReducers({
    oidc,
    outstandingWorksOrdersReport,
    ...sharedLibraryReducers
});

export default rootReducer;
