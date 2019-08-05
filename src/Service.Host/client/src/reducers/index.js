﻿import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import buildsSummaryReport from './buildsSummaryReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import salesArticle from './salesArticle';
import serialNumbers from './serialNumbers';
import serialNumberReissue from './serialNumberReissue';
import serialNumberReissueSalesArticle from './serialNumberReissueSalesArticle';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    salesArticle,
    serialNumbers,
    serialNumberReissue,
    serialNumberReissueSalesArticle,
    ...sharedLibraryReducers
});

export default rootReducer;
