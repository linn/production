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

export const productionMeasuresInfoReport = new ItemType(
    'productionMeasuresInfo',
    'PRODUCTION_MEASURES_INFO',
    '/production/reports/measures/info'
);

export const productionMeasuresCitsReport = new ItemType(
    'productionMeasuresCits',
    'PRODUCTION_MEASURES_CITS',
    '/production/reports/measures/cits'
);

export const assemblyFailsWaitingList = new ItemType(
    'assemblyFailsWaitingListReport',
    'ASSEMBLY_FAILS_WAITING_LIST',
    '/production/reports/assembly-fails-waiting-list'
);

export const whoBuiltWhat = new ItemType(
    'whoBuiltWhat',
    'WHO_BUILT_WHAT',
    '/production/reports/who-built-what'
);

export const whoBuiltWhatDetails = new ItemType(
    'whoBuiltWhatDetails',
    'WHO_BUILT_WHAT_DETAILS',
    '/production/reports/who-built-what-details'
);

export const assemblyFailsMeasures = new ItemType(
    'assemblyFailsMeasures',
    'ASSEMBLY_FAILS_MEASURES',
    '/production/reports/assembly-fails-measures'
);

export const assemblyFailsDetails = new ItemType(
    'assemblyFailsDetails',
    'ASSEMBLY_FAILS_DETAILS',
    '/production/reports/assembly-fails-details'
);
