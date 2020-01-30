import { ReportActions } from '@linn-it/linn-form-components-library';
import { failedPartsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.failedPartsReport.item,
    reportTypes.failedPartsReport.actionType,
    reportTypes.failedPartsReport.uri,
    actionTypes,
    config.appRoot
);
