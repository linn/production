import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer
} from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyFail from './assemblyFails/assemblyFail';
import assemblyFails from './assemblyFails/assemblyFails';
import assemblyFailFaultCodes from './assemblyFails/assemblyFailFaultCodes';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import productionMeasures from './productionMeasures';
import productionTriggersReport from './productionTriggersReport';
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
import whoBuiltWhat from './whoBuiltWhat';
import whoBuiltWhatDetails from './whoBuiltWhatDetails';
import manufacturingRoute from './manufacturingRoutes/manufacturingRoute';
import assemblyFailsMeasures from './assemblyFailsMeasures';
import assemblyFailsDetails from './assemblyFailsDetails';
import smtOutstandingWorkOrderParts from './smtOutstandingWorkOrderParts';
import parts from './parts';
import smtShifts from './smtShifts';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import ptlSettings from './ptlSettings';
import startTriggerRun from './startTriggerRun';
import partFail from './partFail';
import partFails from './partFails';
import storagePlaces from './storagePlaces';
import partFailErrorTypes from './partFailErrorTypes';
import partFailFaultCodes from './partFailFaultCodes';

const errors = fetchErrorReducer({ ...itemTypes, ...reportTypes });

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    assemblyFails,
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
    productionTriggersReport,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    manufacturingRoute,
    assemblyFailsMeasures,
    assemblyFailsDetails,
    errors,
    smtOutstandingWorkOrderParts,
    parts,
    smtShifts,
    ptlSettings,
    startTriggerRun,
    partFail,
    partFails,
    storagePlaces,
    partFailErrorTypes,
    partFailFaultCodes,
    ...sharedLibraryReducers
});

export default rootReducer;
