import { ReportActions } from '@linn-it/linn-form-components-library';
import { boardTestsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.boardTestsReport.item,
    reportTypes.boardTestsReport.actionType,
    reportTypes.boardTestsReport.uri,
    actionTypes,
    config.appRoot
);
