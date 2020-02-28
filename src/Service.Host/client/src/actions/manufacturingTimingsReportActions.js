import { ReportActions } from '@linn-it/linn-form-components-library';
import { manufacturingTimingsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.manufacturingTimingsReport.item,
    reportTypes.manufacturingTimingsReport.actionType,
    reportTypes.manufacturingTimingsReport.uri,
    actionTypes,
    config.appRoot
);
