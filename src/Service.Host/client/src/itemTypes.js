import { ItemType } from '@linn-it/linn-form-components-library';

export const ateFaultCode = new ItemType(
    'ateFaultCode',
    'ATE_FAULT_CODE',
    '/production/quality/ate/fault-codes'
);

export const ateFaultCodes = new ItemType(
    'ateFaultCodes',
    'ATE_FAULT_CODES',
    '/production/quality/ate/fault-codes'
);

export const salesArticle = new ItemType(
    'salesArticle',
    'SALES_ARTICLE',
    '/products/maint/sales-articles'
);

export const salesArticles = new ItemType(
    'salesArticles',
    'SALES_ARTICLES',
    '/products/maint/sales-articles'
);

export const serialNumbers = new ItemType(
    'serialNumbers',
    'SERIAL_NUMBERS',
    '/products/maint/serial-numbers'
);

export const serialNumberReissue = new ItemType(
    'serialNumberReissue',
    'SERIAL_NUMBER_REISSUE',
    '/production/maintenance/serial-number-reissue'
);

export const departments = new ItemType(
    'departments',
    'DEPARTMENTS',
    '/production/resources/departments'
);

export const manufacturingSkill = new ItemType(
    'manufacturingSkill',
    'MANUFACTURING_SKILL',
    '/production/resources/manufacturing-skills'
);

export const manufacturingSkills = new ItemType(
    'manufacturingSkills',
    'MANUFACTURING_SKILLS',
    '/production/resources/manufacturing-skills'
);

export const boardFailType = new ItemType(
    'boardFailType',
    'BOARD_FAIL_TYPE',
    '/production/resources/board-fail-types'
);

export const boardFailTypes = new ItemType(
    'boardFailTypes',
    'BOARD_FAIL_TYPES',
    '/production/resources/board-fail-types'
);
export const manufacturingResource = new ItemType(
    'manufacturingResource',
    'MANUFACTURING_RESOURCE',
    '/production/resources/manufacturing-resources'
);
export const manufacturingResources = new ItemType(
    'manufacturingResources',
    'MANUFACTURING_RESOURCES',
    '/production/resources/manufacturing-resources'
);

export const assemblyFail = new ItemType(
    'assemblyFail',
    'ASSEMBLY_FAIL',
    '/production/quality/assembly-fails'
);

export const assemblyFails = new ItemType(
    'assemblyFails',
    'ASSEMBLY_FAILS',
    '/production/quality/assembly-fails'
);

export const worksOrder = new ItemType('worksOrder', 'WORKS_ORDER', '/production/works-orders');

export const worksOrders = new ItemType('worksOrders', 'WORKS_ORDERS', '/production/works-orders');

export const worksOrdersBatchNotes = new ItemType(
    'worksOrders',
    'WORKS_ORDERS',
    '/production/works-orders-for-part'
);

export const worksOrderDetails = new ItemType(
    'worksOrderDetails',
    'WORKS_ORDER_DETAILS',
    '/production/works-orders/get-part-details'
);

export const productionTriggerLevel = new ItemType(
    'productionTriggerLevel',
    'PRODUCTION_TRIGGER_LEVEL',
    '/production/maintenance/production-trigger-levels'
);

export const productionTriggerLevels = new ItemType(
    'productionTriggerLevels',
    'PRODUCTION_TRIGGER_LEVELS',
    '/production/maintenance/production-trigger-levels'
);

export const pcasRevisions = new ItemType(
    'pcasRevisions',
    'PCAS_REVISIONS',
    '/production/maintenance/pcas-revisions'
);

export const employees = new ItemType(
    'employees',
    'EMPLOYEES',
    '/production/maintenance/employees'
);

export const cits = new ItemType('cits', 'CITS', '/production/maintenance/cits');

export const assemblyFailFaultCode = new ItemType(
    'assemblyFailFaultCode',
    'ASSEMBLY_FAIL_FAULT_CODE',
    '/production/quality/assembly-fail-fault-codes'
);

export const assemblyFailFaultCodes = new ItemType(
    'assemblyFailFaultCodes',
    'ASSEMBLY_FAIL_FAULT_CODES',
    '/production/quality/assembly-fail-fault-codes'
);

export const manufacturingRoute = new ItemType(
    'manufacturingRoute',
    'MANUFACTURING_ROUTE',
    '/production/resources/manufacturing-routes'
);

export const manufacturingRoutes = new ItemType(
    'manufacturingRoutes',
    'MANUFACTURING_ROUTES',
    '/production/resources/manufacturing-routes'
);

export const parts = new ItemType('parts', 'PARTS', '/production/maintenance/parts');

export const smtShifts = new ItemType(
    'smtShifts',
    'SMT_SHIFTS',
    '/production/maintenance/smt-shifts'
);

export const ptlSettings = new ItemType(
    'ptlSettings',
    'PTL_SETTINGS',
    '/production/maintenance/production-trigger-levels-settings'
);

export const partFail = new ItemType('partFail', 'PART_FAIL', '/production/quality/part-fails');

export const partFails = new ItemType('partFails', 'PART_FAILS', '/production/quality/part-fails');

export const storagePlaces = new ItemType(
    'storagePlaces',
    'STORAGE_PLACES',
    '/production/maintenance/storage-places'
);

export const partFailErrorTypes = new ItemType(
    'partFailErrorTypes',
    'PART_FAIL_ERROR_TYPES',
    '/production/quality/part-fail-error-types'
);

export const partFailFaultCodes = new ItemType(
    'partFailFaultCodes',
    'PART_FAIL_FAULT_CODES',
    '/production/quality/part-fail-fault-codes'
);

export const partFailErrorType = new ItemType(
    'partFailErrorType',
    'PART_FAIL_ERROR_TYPE',
    '/production/quality/part-fail-error-types'
);

export const partFailFaultCode = new ItemType(
    'partFailFaultCode',
    'PART_FAIL_FAULT_CODE',
    '/production/quality/part-fail-fault-codes'
);

export const purchaseOrders = new ItemType(
    'purchaseOrders',
    'PURCHASE_ORDERS',
    '/production/resources/purchase-orders'
);

export const partFailSuppliers = new ItemType(
    'partFailSuppliers',
    'PART_FAIL_SUPPLIERS',
    '/production/quality/part-fails/suppliers'
);

export const worksOrderLabel = new ItemType(
    'worksOrderLabel',
    'WORKS_ORDER_LABEL',
    '/production/works-orders/labels'
);

export const worksOrderLabels = new ItemType(
    'worksOrderLabels',
    'WORKS_ORDER_LABELS',
    '/production/works-orders/labels'
);

export const buildPlans = new ItemType(
    'buildPlans',
    'BUILD_PLANS',
    '/production/maintenance/build-plans'
);

export const labelType = new ItemType(
    'labelType',
    'LABEL_TYPE',
    '/production/resources/label-types'
);

export const labelTypes = new ItemType(
    'labelTypes',
    'LABEL_TYPES',
    '/production/resources/label-types'
);

export const labelReprint = new ItemType(
    'labelReprint',
    'LABEL_REPRINT',
    '/production/maintenance/labels/reprint-reasons'
);

export const workStations = new ItemType(
    'workStations',
    'WORK_STATIONS',
    '/production/maintenance/work-stations'
);

export const ateTest = new ItemType('ateTest', 'ATE_TEST', '/production/quality/ate-tests');

export const ateTests = new ItemType('ateTests', 'ATE_TESTS', '/production/quality/ate-tests');
