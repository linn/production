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
import boardFailTypes from './boardFailTypes/boardFailTypes';
import boardFailType from './boardFailTypes/boardFailType';
import assemblyFailsWaitingListReport from './assemblyFailsWaitingListReport';

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    ateFaultCode,
    ateFaultCodes,
    departments,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    productionMeasures,
    buildsDetailReport,
    manufacturingSkills,
    manufacturingSkill,
    boardFailType,
    boardFailTypes,
    assemblyFailsWaitingListReport,
    ...sharedLibraryReducers
});

export default rootReducer;
