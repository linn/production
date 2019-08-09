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
export const departments = new ItemType('departments', 'DEPARTMENTS', '/production/departments');
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
