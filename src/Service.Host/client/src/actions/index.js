import { makeActionTypes, makeReportActionTypes } from '@linn-it/linn-form-components-library';
import * as reportTypes from '../reportTypes';

export const outstandingWorksOrdersReportActionTypes = makeReportActionTypes(
    reportTypes.outstandingWorksOrdersReport.actionType
);

export const FETCH_ERROR = 'FETCH_ERROR';
