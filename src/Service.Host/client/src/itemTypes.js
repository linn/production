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

export const assemblyFail = new ItemType(
    'assemblyFail',
    'ASSEMBLY_FAIL',
    '/production/quality/assembly-fails'
);

export const worksOrders = new ItemType(
    'worksOrders',
    'WORKS_ORDERS',
    '/production/maintenance/works-orders'
);
