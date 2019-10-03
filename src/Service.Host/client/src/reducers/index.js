﻿import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer,
    ItemType
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
import worksOrder from './worksOrders/worksOrder';
import worksOrders from './worksOrders/worksOrders';
import productionTriggerLevels from './productionTriggerLevels';
import pcasRevisions from './pcasRevisions';
import employees from './employees';
import whoBuiltWhat from './whoBuiltWhat';
import whoBuiltWhatDetails from './whoBuiltWhatDetails';
import manufacturingRoute from './manufacturingRoutes/manufacturingRoute';
import assemblyFailsMeasures from './assemblyFailsMeasures';
import assemblyFailsDetails from './assemblyFailsDetails';
import worksOrderDetails from './worksOrders/worksOrderDetails';
import smtOutstandingWorkOrderParts from './smtOutstandingWorkOrderParts';
import parts from './parts';
import smtShifts from './smtShifts';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import ptlSettings from './ptlSettings';
import startTriggerRun from './startTriggerRun';

const errors = fetchErrorReducer({...itemTypes, ...reportTypes });

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    assemblyFails,
    assemblyFailsDetails,
    assemblyFailFaultCodes,
    assemblyFailsMeasures,
    assemblyFailsWaitingListReport,
    ateFaultCode,
    ateFaultCodes,
    boardFailType,
    boardFailTypes,
    buildsDetailReport,
    buildsSummaryReport,
    cits,
    departments,
    employees,
    errors,
    manufacturingSkills,
    manufacturingSkill,
    manufacturingResources,
    manufacturingResource,
    manufacturingRoute,
    outstandingWorksOrdersReport,
    parts,
    pcasRevisions,
    productionTriggerLevels,
    productionMeasures,
    productionTriggersReport,
    ptlSettings,
    smtShifts,
    smtOutstandingWorkOrderParts,
    startTriggerRun,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    worksOrder,
    worksOrders,
    worksOrderDetails,
    ...sharedLibraryReducers
});

export default rootReducer;
