import { ItemType } from '@linn-it/linn-form-components-library';

export const startTriggerRun = new ItemType(
    'startTriggerRun',
    'START_TRIGGER_RUN',
    '/production/maintenance/production-trigger-levels-settings/start-trigger-run'
);

export const printMACLabels = new ItemType(
    'printMACLabels',
    'PRINT_MAC_LABELS',
    '/production/maintenance/labels/reprint-mac-label'
);

export const printAllLabelsForProduct = new ItemType(
    'printAllLabelsForProduct',
    'PRINT_ALL_LABELS_FOR_PRODUCT',
    '/production/maintenance/labels/reprint-all'
);
