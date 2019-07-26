import { ReportActions } from '@linn-it/linn-form-components-library';
import { buildsSummaryReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.buildsSummaryReport.actionType,
    reportTypes.buildsSummaryReport.uri,
    actionTypes,
    config.appRoot
);
