import { ItemType } from '@linn-it/linn-form-components-library';

export const buildsSummaryReport = new ItemType(
    'buildsSummaryReport',
    'BUILDS_SUMMARY_REPORT',
    '/production/reports/builds-summary'
);

export const buildsDetailReport = new ItemType(
    'buildsDetailReport',
    'BUILDS_DETAIL_REPORT',
    '/production/reports/builds-detail'
);

export const outstandingWorksOrdersReport = new ItemType(
    'outstandingWorksOrdersReport',
    'OUTSTANDING_WORKS_ORDERS_REPORT',
    '/production/works-orders/outstanding-works-orders-report'
);

export const productionMeasuresInfoReport = new ItemType(
    'productionMeasuresInfo',
    'PRODUCTION_MEASURES_INFO_REPORT',
    '/production/reports/measures/info'
);

export const productionMeasuresCitsReport = new ItemType(
    'productionMeasuresCits',
    'PRODUCTION_MEASURES_CITS_REPORT',
    '/production/reports/measures/cits'
);

export const productionTriggersReport = new ItemType(
    'productionTriggersReport',
    'PRODUCTION_TRIGGERS_REPORT',
    '/production/reports/triggers'
);

export const productionTriggerFacts = new ItemType(
    'productionTriggerFacts',
    'PRODUCTION_TRIGGER_FACTS_REPORT',
    '/production/reports/triggers/facts'
);

export const assemblyFailsWaitingList = new ItemType(
    'assemblyFailsWaitingListReport',
    'ASSEMBLY_FAILS_WAITING_LIST_REPORT',
    '/production/reports/assembly-fails-waiting-list'
);

export const whoBuiltWhat = new ItemType(
    'whoBuiltWhat',
    'WHO_BUILT_WHAT_REPORT',
    '/production/reports/who-built-what/report'
);

export const whoBuiltWhatDetails = new ItemType(
    'whoBuiltWhatDetails',
    'WHO_BUILT_WHAT_DETAILS_REPORT',
    '/production/reports/who-built-what-details'
);

export const assemblyFailsMeasures = new ItemType(
    'assemblyFailsMeasures',
    'ASSEMBLY_FAILS_MEASURES_REPORT',
    '/production/reports/assembly-fails-measures/report'
);

export const assemblyFailsDetails = new ItemType(
    'assemblyFailsDetails',
    'ASSEMBLY_FAILS_DETAILS_REPORT',
    '/production/reports/assembly-fails-details/report'
);

export const smtOutstandingWorkOrderParts = new ItemType(
    'smtOutstandingWorkOrderParts',
    'SMT_OUTSTANDING_WO_PARTS_REPORT',
    '/production/reports/smt/outstanding-works-order-parts/report'
);

export const manufacturingCommitDate = new ItemType(
    'manufacturingCommitDate',
    'MANUFACTURING_COMMIT_DATE_REPORT',
    '/production/reports/manufacturing-commit-date/report'
);

export const overdueOrders = new ItemType(
    'overdueOrders',
    'OVERDUE_ORDERS_REPORT',
    '/production/reports/overdue-orders/report'
);

export const boardTestsReport = new ItemType(
    'boardTestsReport',
    'BOARD_TESTS_REPORT',
    '/production/reports/board-tests-report'
);

export const boardTestDetailsReport = new ItemType(
    'boardTestDetailsReport',
    'BOARD_TEST_DETAILS_REPORT',
    '/production/reports/board-test-details-report'
);

export const partFailDetailsReport = new ItemType(
    'partFailDetailsReport',
    'PART_FAIL_DETAILS_REPORT',
    '/production/quality/part-fails/detail-report/report'
);

export const productionBackOrdersReport = new ItemType(
    'productionBackOrdersReport',
    'PRODUCTION_BACK_ORDERS_REPORT',
    '/production/reports/production-back-orders'
);
