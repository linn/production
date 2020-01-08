import { ReportActions } from '@linn-it/linn-form-components-library';
import { ateDetailsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.ateDetailsReport.item,
    reportTypes.ateDetailsReport.actionType,
    reportTypes.ateDetailsReport.uri,
    actionTypes,
    config.appRoot
);
