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

export const serialNumberReissue = new ItemType(
    'serialNumberReissue',
    'SERIAL_NUMBER_REISSUE',
    '/production/maintenance/serial-number-reissue'
);

// Proxy items

export const serialNumbers = new ItemType(
    'serialNumbers',
    'SERIAL_NUMBERS',
    '/products/maint/serial-numbers'
);
