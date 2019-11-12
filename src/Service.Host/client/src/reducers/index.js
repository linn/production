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
import productionTriggerFacts from './productionTriggerFacts';
import wwdTriggerReport from './wwdTriggerReport';
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
import manufacturingRoutes from './manufacturingRoutes/manufacturingRoutes';
import assemblyFailsMeasures from './assemblyFailsMeasures';
import assemblyFailsDetails from './assemblyFailsDetails';
import worksOrderDetails from './worksOrders/worksOrderDetails';
import worksOrderLabel from './worksOrders/worksOrderLabel';
import worksOrderLabels from './worksOrders/worksOrderLabels';
import smtOutstandingWorkOrderParts from './smtOutstandingWorkOrderParts';
import parts from './parts';
import smtShifts from './smtShifts';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';
import ptlSettings from './ptlSettings';
import startTriggerRun from './startTriggerRun';
import partFail from './partFail';
import partFails from './partFails';
import storagePlaces from './storagePlaces';
import partFailErrorTypes from './partFailErrorTypes';
import partFailFaultCodes from './partFailFaultCodes';
import purchaseOrders from './purchaseOrders';
import serialNumbers from './serialNumbers';
import serialNumberReissue from './serialNumberReissue';
import salesArticles from './salesArticles';
import salesArticle from './salesArticle';
import assemblyFailFaultCode from './assemblyFails/assemblyFailFaultCode';
import manufacturingCommitDate from './manufacturingCommitDate';
import printAllLabelsForProduct from './printAllLabelsForProduct';
import printMACLabels from './printMACLabels';
import overdueOrders from './overdueOrdersReport';
import partFailErrorType from './partFailErrorType';
import partFailFaultCode from './partFailFaultCode';
import boardTestsReport from './boardTestsReport';
import boardTestDetailsReport from './boardTestDetailsReport';

const errors = fetchErrorReducer({
    ...itemTypes,
    ...reportTypes,
    ...processTypes
});

const rootReducer = combineReducers({
    oidc,
    assemblyFail,
    assemblyFails,
    assemblyFailsDetails,
    assemblyFailFaultCode,
    assemblyFailFaultCodes,
    assemblyFailsMeasures,
    assemblyFailsWaitingListReport,
    ateFaultCode,
    ateFaultCodes,
    boardFailType,
    boardFailTypes,
    boardTestDetailsReport,
    boardTestsReport,
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
    outstandingWorksOrdersReport,
    overdueOrders,
    parts,
    pcasRevisions,
    printAllLabelsForProduct,
    printMACLabels,
    productionTriggerLevels,
    productionMeasures,
    productionTriggersReport,
    productionTriggerFacts,
    wwdTriggerReport,
    ptlSettings,
    salesArticle,
    salesArticles,
    serialNumbers,
    serialNumberReissue,
    smtShifts,
    smtOutstandingWorkOrderParts,
    startTriggerRun,
    partFail,
    partFails,
    storagePlaces,
    partFailErrorTypes,
    partFailFaultCodes,
    purchaseOrders,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    worksOrder,
    worksOrders,
    worksOrderDetails,
    manufacturingCommitDate,
    partFailErrorType,
    partFailFaultCode,
    manufacturingRoute,
    manufacturingRoutes,
    worksOrderLabel,
    worksOrderLabels,
    ...sharedLibraryReducers
});

export default rootReducer;
