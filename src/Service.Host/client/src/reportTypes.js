import { ItemType } from '@linn-it/linn-form-components-library';

export const buildsSummaryReport = new ItemType(
    'buildsSummary',
    'BUILDS_SUMMARY',
    '/production/reports/builds-summary'
);

export const buildsDetailReport = new ItemType(
    'buildsDetail',
    'BUILDS_DETAIL',
    '/production/reports/builds-detail'
);

export const outstandingWorksOrdersReport = new ItemType(
    'outstandingWorksOrdersReport',
    'OUTSTANDING_WORKS_ORDERS',
    '/production/maintenance/works-orders/outstanding-works-orders-report'
);

export const assemblyFailsWaitingList = new ItemType(
    'assemblyFailsWaitingListReport',
    'ASSEMBLY_FAILS_WAITING_LIST',
    '/production/reports/assembly-fails-waiting-list'
);
