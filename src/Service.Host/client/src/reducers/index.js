﻿import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import buildsSummaryReport from './buildsSummaryReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import productionMeasures from './productionMeasures';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    productionMeasures,
    ...sharedLibraryReducers
});

export default rootReducer;
