import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyFail from './assemblyFails/assemblyFail';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import productionMeasures from './productionMeasures';
import manufacturingSkills from './manufacturingSkills/manufacturingSkills';
import manufacturingSkill from './manufacturingSkills/manufacturingSkill';
import cits from './cits';
import manufacturingResources from './manufacturingResources/manufacturingResources';
import manufacturingResource from './manufacturingResources/manufacturingResource';
import boardFailTypes from './boardFailTypes/boardFailTypes';
import boardFailType from './boardFailTypes/boardFailType';
import assemblyFailsWaitingListReport from './assemblyFailsWaitingListReport';
import whoBuiltWhat from './whoBuiltWhat';
import whoBuiltWhatDetails from './whoBuiltWhatDetails';
import manufacturingRoute from './manufacturingRoutes/manufacturingRoute';

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    assemblyFailsWaitingListReport,
    ateFaultCode,
    ateFaultCodes,
    buildsDetailReport,
    buildsSummaryReport,
    boardFailType,
    boardFailTypes,
    cits,
    departments,
    manufacturingSkills,
    manufacturingSkill,
    manufacturingResources,
    manufacturingResource,
    outstandingWorksOrdersReport,
    productionMeasures,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    manufacturingRoute,
    ...sharedLibraryReducers
});

export default rootReducer;
