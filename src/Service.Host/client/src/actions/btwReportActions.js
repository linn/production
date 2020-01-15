import { ReportActions } from '@linn-it/linn-form-components-library';
import { btwReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.btwReport.item,
    reportTypes.btwReport.actionType,
    reportTypes.btwReport.uri,
    actionTypes,
    config.appRoot
);
