import { ReportActions } from '@linn-it/linn-form-components-library';
import { outstandingWorksOrdersReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.outstandingWorksOrdersReport,
    reportTypes.outstandingWorksOrdersReport.actionType,
    reportTypes.outstandingWorksOrdersReport.uri,
    actionTypes,
    config.appRoot
);
