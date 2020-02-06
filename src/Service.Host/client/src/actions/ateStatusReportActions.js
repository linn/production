import { ReportActions } from '@linn-it/linn-form-components-library';
import { ateStatusReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.ateStatusReport.item,
    reportTypes.ateStatusReport.actionType,
    reportTypes.ateStatusReport.uri,
    actionTypes,
    config.appRoot
);
