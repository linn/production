import { ReportActions } from '@linn-it/linn-form-components-library';
import { buildPlansReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.buildPlansReport.item,
    reportTypes.buildPlansReport.actionType,
    reportTypes.buildPlansReport.uri,
    actionTypes,
    config.appRoot
);
