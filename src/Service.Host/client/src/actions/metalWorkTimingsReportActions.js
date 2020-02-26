import { ReportActions } from '@linn-it/linn-form-components-library';
import { metalWorkTimingsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.metalWorkTimingsReport.item,
    reportTypes.metalWorkTimingsReport.actionType,
    reportTypes.metalWorkTimingsReport.uri,
    actionTypes,
    config.appRoot
);
