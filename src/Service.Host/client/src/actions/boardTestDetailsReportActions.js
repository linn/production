import { ReportActions } from '@linn-it/linn-form-components-library';
import { boardTestDetailsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.boardTestDetailsReport.item,
    reportTypes.boardTestDetailsReport.actionType,
    reportTypes.boardTestDetailsReport.uri,
    actionTypes,
    config.appRoot
);
