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
import productionTriggerLevels from './productionTriggerLevels/productionTriggerLevels';
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
import ptlSettings from './productionTriggerLevels/ptlSettings';
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
import partFailDetailsReport from './partFailDetailsReport';
import partFailSuppliers from './partFailSuppliers';
import printWorksOrderLabels from './printWorksOrderLabels';
import printWorksOrderAioLabels from './printWorksOrderAioLabels';
import productionBackOrdersReport from './productionBackOrdersReport';
import localStorage from './localStorage';
import labelType from './labelTypes/labelType';
import labelTypes from './labelTypes/labelTypes';
import productionTriggerLevel from './productionTriggerLevels/productionTriggerLevel';

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
    localStorage,
    manufacturingCommitDate,
    manufacturingResources,
    manufacturingResource,
    manufacturingRoute,
    manufacturingRoutes,
    manufacturingSkills,
    manufacturingSkill,
    outstandingWorksOrdersReport,
    overdueOrders,
    parts,
    partFail,
    partFails,
    partFailDetailsReport,
    partFailErrorType,
    partFailErrorTypes,
    partFailFaultCode,
    partFailFaultCodes,
    partFailSuppliers,
    pcasRevisions,
    printAllLabelsForProduct,
    printMACLabels,
    printWorksOrderLabels,
    printWorksOrderAioLabels,
    productionBackOrdersReport,
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
    storagePlaces,
    purchaseOrders,
    whoBuiltWhat,
    whoBuiltWhatDetails,
    worksOrder,
    worksOrders,
    worksOrderDetails,
    worksOrderLabel,
    worksOrderLabels,
    labelType,
    labelTypes,
    productionTriggerLevel,
    ...sharedLibraryReducers
});

export default rootReducer;
