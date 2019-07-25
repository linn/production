import { makeActionTypes, makeReportActionTypes } from '@linn-it/linn-form-components-library';
import * as itemTypes from '../itemTypes';
import * as reportTypes from '../reportTypes';

export const outstandingWorksOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.outstandingWorksOrdersReport.actionType
);

export const FETCH_ERROR = 'FETCH_ERROR';

export const ateFaultCodeActionTypes = makeActionTypes(itemTypes.ateFaultCode.actionType);
export const ateFaultCodesActionTypes = makeActionTypes(itemTypes.ateFaultCodes.actionType, false);

export const buildsSummaryReportActionTypes = makeReportActionTypes(
    reportTypes.buildsSummaryReport.actionType
);
