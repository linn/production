import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyFail from './assemblyFails/assemblyFail';
import assemblyFailFaultCodes from './assemblyFails/assemblyFailFaultCodes';
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
import worksOrders from './worksOrders/worksOrders';
import productionTriggerLevels from './productionTriggerLevels';
import pcasRevisions from './pcasRevisions';
import employees from './employees';
import cits from './cits';
import whoBuiltWhat from './whoBuiltWhat';
import whoBuiltWhatDetails from './whoBuiltWhatDetails';

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    assemblyFailFaultCodes,
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
    worksOrders,
    productionTriggerLevels,
    pcasRevisions,
    employees,
    outstandingWorksOrdersReport,
    productionMeasures,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    ...sharedLibraryReducers
});

export default rootReducer;
