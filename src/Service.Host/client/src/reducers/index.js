﻿import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import salesArticle from './salesArticle';
import serialNumbers from './serialNumbers';
import serialNumberReissue from './serialNumberReissue';
import salesArticles from './salesArticles';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    departments,
    buildsDetailReport,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    salesArticle,
    serialNumbers,
    serialNumberReissue,
    salesArticles,
    ...sharedLibraryReducers
});

export default rootReducer;
