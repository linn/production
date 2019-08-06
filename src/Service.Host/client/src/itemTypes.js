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

export const departments = new ItemType('departments', 'DEPARTMENTS', '/production/departments');
