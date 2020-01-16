import { ReportActions } from '@linn-it/linn-form-components-library';
import { delPerfReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.delPerfReport.item,
    reportTypes.delPerfReport.actionType,
    reportTypes.delPerfReport.uri,
    actionTypes,
    config.appRoot
);