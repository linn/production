import { ReportActions } from '@linn-it/linn-form-components-library';
import { daysRequiredReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.daysRequiredReport.item,
    reportTypes.daysRequiredReport.actionType,
    reportTypes.daysRequiredReport.uri,
    actionTypes,
    config.appRoot
);
