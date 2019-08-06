import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import productionMeasures from './productionMeasures';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    departments,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    productionMeasures,
    buildsDetailReport,
    ...sharedLibraryReducers
});

export default rootReducer;
