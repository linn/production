import { makeActionTypes, makeReportActionTypes } from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';

export const outstandingWorksOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.outstandingWorksOrdersReport.actionType
);

export const buildsDetailReportActionTypes = makeReportActionTypes(
    reportTypes.buildsDetailReport.actionType
);

export const FETCH_ERROR = 'FETCH_ERROR';

export const ateFaultCodeActionTypes = makeActionTypes(itemTypes.ateFaultCode.actionType);
export const ateFaultCodesActionTypes = makeActionTypes(itemTypes.ateFaultCodes.actionType, false);

export const assemblyFailActionTypes = makeActionTypes(itemTypes.assemblyFail.actionType, false);

export const salesArticleActionTypes = makeActionTypes(itemTypes.salesArticle.actionType);

export const salesArticles = makeActionTypes(itemTypes.salesArticles.actionType);

export const serialNumbersActionTypes = makeActionTypes(itemTypes.serialNumbers.actionType);

export const serialNumberReissueActionTypes = makeActionTypes(
    itemTypes.serialNumberReissue.actionType
);

export const departmentsActionTypes = makeActionTypes(itemTypes.departments.actionType, false);

export const manufacturingSkillActionTypes = makeActionTypes(
    itemTypes.manufacturingSkill.actionType
);

export const manufacturingSkillsActionTypes = makeActionTypes(
    itemTypes.manufacturingSkills.actionType,
    false
);

export const boardFailTypesActionTypes = makeActionTypes(
    itemTypes.boardFailTypes.actionType,
    false
);

export const boardFailTypeActionTypes = makeActionTypes(itemTypes.boardFailType.actionType);

export const buildsSummaryReportActionTypes = makeReportActionTypes(
    reportTypes.buildsSummaryReport.actionType
);

export const assemblyFailsWaitingListReportActionTypes = makeReportActionTypes(
    reportTypes.assemblyFailsWaitingList.actionType
);
