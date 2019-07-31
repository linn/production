import { ItemType } from '@linn-it/linn-form-components-library';

export const buildsSummaryReport = new ItemType(
    'buildsSummary',
    'BUILDS_SUMMARY',
    '/production/reports/builds-summary'
);

export const outstandingWorksOrdersReport = new ItemType(
    'outstandingWorksOrdersReport',
    'OUTSTANDING_WORKS_ORDERS',
    '/production/maintenance/works-orders/outstanding-works-orders-report'
);

export const productionMeasuresCitsReport = new ItemType(
    'productionMeasures',
    'PRODUCTION_MEASURES',
    '/production/reports/measures/cits'
);
