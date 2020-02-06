import {
    makeActionTypes,
    makeReportActionTypes,
    makeProcessActionTypes
} from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';
import * as processTypes from '../processTypes';

export const outstandingWorksOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.outstandingWorksOrdersReport.actionType
);

export const buildsDetailReportActionTypes = makeReportActionTypes(
    reportTypes.buildsDetailReport.actionType
);

export const FETCH_ERROR = 'FETCH_ERROR';

export const ateFaultCodeActionTypes = makeActionTypes(itemTypes.ateFaultCode.actionType);
export const ateFaultCodesActionTypes = makeActionTypes(itemTypes.ateFaultCodes.actionType, false);

export const assemblyFailActionTypes = makeActionTypes(itemTypes.assemblyFail.actionType, true);
export const assemblyFailsActionTypes = makeActionTypes(itemTypes.assemblyFails.actionType, true);

export const salesArticleActionTypes = makeActionTypes(itemTypes.salesArticle.actionType);

export const worksOrderActionTypes = makeActionTypes(itemTypes.worksOrder.actionType);
export const worksOrderDetailsActionTypes = makeActionTypes(itemTypes.worksOrderDetails.actionType);
export const worksOrdersActionTypes = makeActionTypes(itemTypes.worksOrders.actionType, true);

export const salesArticles = makeActionTypes(itemTypes.salesArticles.actionType);

export const serialNumbersActionTypes = makeActionTypes(itemTypes.serialNumbers.actionType);

export const serialNumberReissueActionTypes = makeActionTypes(
    itemTypes.serialNumberReissue.actionType
);

export const departmentsActionTypes = makeActionTypes(itemTypes.departments.actionType, false);

export const manufacturingSkillActionTypes = makeActionTypes(
    itemTypes.manufacturingSkill.actionType
);

export const manufacturingSkillsActionTypes = makeActionTypes(
    itemTypes.manufacturingSkills.actionType,
    false
);

export const boardFailTypesActionTypes = makeActionTypes(
    itemTypes.boardFailTypes.actionType,
    false
);

export const manufacturingResourceActionTypes = makeActionTypes(
    itemTypes.manufacturingResource.actionType
);
export const manufacturingResourcesActionTypes = makeActionTypes(
    itemTypes.manufacturingResources.actionType,
    false
);

export const boardFailTypeActionTypes = makeActionTypes(itemTypes.boardFailType.actionType);

export const buildsSummaryReportActionTypes = makeReportActionTypes(
    reportTypes.buildsSummaryReport.actionType
);

export const citsActionTypes = makeActionTypes(itemTypes.cits.actionType);

export const productionMeasuresInfoActionTypes = makeReportActionTypes(
    reportTypes.productionMeasuresInfoReport.actionType
);

export const productionMeasuresCitsActionTypes = makeReportActionTypes(
    reportTypes.productionMeasuresCitsReport.actionType
);

export const productionTriggersReportTypes = makeReportActionTypes(
    reportTypes.productionTriggersReport.actionType
);

export const productionTriggerFactsTypes = makeReportActionTypes(
    reportTypes.productionTriggerFacts.actionType
);

export const wwdTriggerReportTypes = makeReportActionTypes(reportTypes.wwdTriggerReport.actionType);

export const assemblyFailsWaitingListReportActionTypes = makeReportActionTypes(
    reportTypes.assemblyFailsWaitingList.actionType
);

export const productionTriggerLevel = makeActionTypes(
    itemTypes.productionTriggerLevel.actionType,
    true,
    true
);

export const productionTriggerLevels = makeActionTypes(
    itemTypes.productionTriggerLevels.actionType,
    false
);

export const pcasRevisionsActionTypes = makeActionTypes(itemTypes.pcasRevisions.actionType, false);

export const citsActionsTypes = makeActionTypes(itemTypes.cits.actionType, false);

export const employeesActionTypes = makeActionTypes(itemTypes.employees.actionType, false);

export const assemblyFailFaultCodeActionTypes = makeActionTypes(
    itemTypes.assemblyFailFaultCode.actionType
);

export const assemblyFailFaultCodesActionTypes = makeActionTypes(
    itemTypes.assemblyFailFaultCodes.actionType,
    false
);

export const whoBuiltWhatReportActionTypes = makeReportActionTypes(
    reportTypes.whoBuiltWhat.actionType
);
export const whoBuiltWhatDetailsReportActionTypes = makeReportActionTypes(
    reportTypes.whoBuiltWhatDetails.actionType
);

export const manufacturingRouteActionTypes = makeActionTypes(
    itemTypes.manufacturingRoute.actionType
);

export const manufacturingRoutesActionTypes = makeActionTypes(
    itemTypes.manufacturingRoutes.actionType,
    false
);

export const assemblyFailsMeasuresActionTypes = makeReportActionTypes(
    reportTypes.assemblyFailsMeasures.actionType
);

export const assemblyFailsDetailsActionTypes = makeReportActionTypes(
    reportTypes.assemblyFailsDetails.actionType
);

export const smtOutstandingWorkOrderPartsActionTypes = makeReportActionTypes(
    reportTypes.smtOutstandingWorkOrderParts.actionType
);

export const partsActionsTypes = makeActionTypes(itemTypes.parts.actionType, false);

export const smtShiftsActionTypes = makeActionTypes(itemTypes.smtShifts.actionType);

export const ptlSettingsActionTypes = makeActionTypes(itemTypes.ptlSettings.actionType);

export const startTriggerRunActionTypes = makeProcessActionTypes(
    processTypes.startTriggerRun.actionType
);

export const manufacturingCommitDateActionTypes = makeReportActionTypes(
    reportTypes.manufacturingCommitDate.actionType
);

export const partFailActionTypes = makeActionTypes(itemTypes.partFail.actionType);

export const partFailsActionTypes = makeActionTypes(itemTypes.partFails.actionType);

export const partFailErrorTypesActionTypes = makeActionTypes(
    itemTypes.partFailErrorTypes.actionType
);

export const partFailErrorTypeActionTypes = makeActionTypes(itemTypes.partFailErrorType.actionType);

export const partFailFaultCodesActionTypes = makeActionTypes(
    itemTypes.partFailFaultCodes.actionType
);

export const partFailFaultCodeActionTypes = makeActionTypes(itemTypes.partFailFaultCode.actionType);

export const storagePlacesActionTypes = makeActionTypes(itemTypes.storagePlaces.actionType);

export const purchaseOrdersActionTypes = makeActionTypes(itemTypes.purchaseOrders.actionType, true);

export const overdueOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.overdueOrders.actionType
);

export const printMACLabelsActionTypes = makeProcessActionTypes(
    processTypes.printMACLabels.actionType
);

export const printAllLabelsForProductActionTypes = makeProcessActionTypes(
    processTypes.printAllLabelsForProduct.actionType
);

export const boardTestsReportActionTypes = makeReportActionTypes(
    reportTypes.boardTestsReport.actionType
);

export const worksOrderLabelActionTypes = makeActionTypes(
    itemTypes.worksOrderLabel.actionType,
    true
);

export const worksOrderLabelsActionTypes = makeActionTypes(itemTypes.worksOrderLabels.actionType);

export const boardTestDetailsReportActionTypes = makeReportActionTypes(
    reportTypes.boardTestDetailsReport.actionType
);

export const partFailDetailsReportActionTypes = makeReportActionTypes(
    reportTypes.partFailDetailsReport.actionType
);

export const partFailSuppliersActionTypes = makeActionTypes(
    itemTypes.partFailSuppliers.actionType,
    true
);

export const printWorksOrderLabelsActionTypes = makeProcessActionTypes(
    processTypes.printWorksOrderLabels.actionType
);

export const printWorksOrderAioLabelsActionTypes = makeProcessActionTypes(
    processTypes.printWorksOrderAioLabels.actionType
);

export const productionBackOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.productionBackOrdersReport.actionType
);

export const buildPlansReportActionTypes = makeReportActionTypes(
    reportTypes.buildPlansReport.actionType
);

export const btwReportActionTypes = makeReportActionTypes(reportTypes.btwReport.actionType);

export const delPerfReportActionTypes = makeReportActionTypes(reportTypes.delPerfReport.actionType);

export const shortageSummaryActionTypes = makeReportActionTypes(
    reportTypes.shortageSummary.actionType
);

export const delPerfDetailsActionTypes = makeReportActionTypes(
    reportTypes.delPerfDetails.actionType
);

export const buildPlansActionTypes = makeActionTypes(itemTypes.buildPlans.actionType);

export const SELECT_DEFAULT_WORKS_ORDER_PRINTER = 'SELECT_DEFAULT_WORKS_ORDER_PRINTER';

export const SET_DEFAULT_WORKS_ORDER_PRINTER = 'SET_DEFAULT_WORKS_ORDER_PRINTER';

export const labelTypeActionTypes = makeActionTypes(itemTypes.labelType.actionType);

export const labelTypesActionTypes = makeActionTypes(itemTypes.labelTypes.actionType, false);

export const labelReprintActionTypes = makeActionTypes(itemTypes.labelReprint.actionType);

export const labelPrintActionTypes = makeActionTypes(itemTypes.labelPrint.actionType);

export const labelPrinterActionTypes = makeActionTypes(itemTypes.labelPrinters.actionType);

export const labelPrintTypeActionTypes = makeActionTypes(itemTypes.labelPrintTypes.actionType);

export const workStationsActionTypes = makeActionTypes(itemTypes.workStations.actionType, false);

export const ateStatusReportActionTypes = makeReportActionTypes(
    reportTypes.ateStatusReport.actionType
);

export const ateDetailsReportActionTypes = makeReportActionTypes(
    reportTypes.ateDetailsReport.actionType
);

export const ateTestActionTypes = makeActionTypes(itemTypes.ateTest.actionType);

export const ateTestsActionTypes = makeActionTypes(itemTypes.ateTests.actionType);

export const componentCountsActionTypes = makeActionTypes(itemTypes.componentCounts.actionType);

export const addressesActionTypes = makeActionTypes(itemTypes.addresses.actionType, false);

export const addressActionTypes = makeActionTypes(itemTypes.address.actionType);

export const suppliersActionTypes = makeActionTypes(itemTypes.suppliers.actionType, false);

export const failedPartsReportActionTypes = makeReportActionTypes(
    reportTypes.failedPartsReport.actionType
);

export const issueSernosActionTypes = makeProcessActionTypes(processTypes.issueSernos.actionType);

export const buildSernosActionTypes = makeProcessActionTypes(processTypes.buildSernos.actionType);

export const purchaseOrderActionTypes = makeActionTypes(itemTypes.purchaseOrder.actionType);
