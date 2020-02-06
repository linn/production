import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer
} from '@linn-it/linn-form-components-library';
import { connectRouter } from 'connected-react-router';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import assemblyFail from './assemblyFails/assemblyFail';
import assemblyFails from './assemblyFails/assemblyFails';
import assemblyFailFaultCodes from './assemblyFails/assemblyFailFaultCodes';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import ateTest from './ateTests/ateTest';
import ateTests from './ateTests/ateTests';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import btwReport from './btwReport';
import delPerfReport from './delPerfReport';
import delPerfDetails from './delPerfDetails';
import shortageSummary from './shortageSummary';
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
import buildPlansReport from './buildPlansReport';
import buildPlans from './buildPlans';
import labelType from './labelTypes/labelType';
import labelTypes from './labelTypes/labelTypes';
import labelReprint from './labelReprint';
import productionTriggerLevel from './productionTriggerLevels/productionTriggerLevel';
import workStations from './workStations';
import historyStore from './history';
import ateStatusReport from './ateStatusReport';
import ateDetailsReport from './ateDetailsReport';
import componentCounts from './componentCounts';
import labelPrint from './labelPrint';
import labelPrintTypes from './labelPrintTypes';
import labelPrinters from './labelPrinters';
import suppliers from './suppliers';
import addresses from './addresses';
import address from './address';
import failedPartsReport from './failedPartsReport';
import daysRequiredReport from './daysRequiredReport';

const errors = fetchErrorReducer({
    ...itemTypes,
    ...reportTypes,
    ...processTypes
});

const reducer = history =>
    combineReducers({
        oidc,
        historyStore,
        router: connectRouter(history),
        address,
        addresses,
        assemblyFail,
        assemblyFails,
        assemblyFailsDetails,
        assemblyFailFaultCode,
        assemblyFailFaultCodes,
        assemblyFailsMeasures,
        assemblyFailsWaitingListReport,
        ateDetailsReport,
        ateFaultCode,
        ateFaultCodes,
        ateStatusReport,
        ateTest,
        ateTests,
        boardFailType,
        boardFailTypes,
        boardTestDetailsReport,
        boardTestsReport,
        btwReport,
        buildsDetailReport,
        buildPlans,
        buildPlansReport,
        buildsSummaryReport,
        cits,
        componentCounts,
        daysRequiredReport,
        delPerfReport,
        delPerfDetails,
        departments,
        employees,
        errors,
        failedPartsReport,
        labelPrint,
        labelPrinters,
        labelPrintTypes,
        labelReprint,
        labelType,
        labelTypes,
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
        productionTriggerLevel,
        productionTriggersReport,
        productionTriggerFacts,
        ptlSettings,
        purchaseOrders,
        salesArticle,
        salesArticles,
        serialNumbers,
        serialNumberReissue,
        shortageSummary,
        smtShifts,
        smtOutstandingWorkOrderParts,
        startTriggerRun,
        storagePlaces,
        suppliers,
        whoBuiltWhat,
        whoBuiltWhatDetails,
        worksOrder,
        worksOrders,
        worksOrderDetails,
        worksOrderLabel,
        worksOrderLabels,
        workStations,
        wwdTriggerReport,
        ...sharedLibraryReducers
    });

export default reducer;
