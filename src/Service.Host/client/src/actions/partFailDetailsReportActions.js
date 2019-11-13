import { ReportActions } from '@linn-it/linn-form-components-library';
import { partFailDetailsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.partFailDetailsReport.item,
    reportTypes.partFailDetailsReport.actionType,
    reportTypes.partFailDetailsReport.uri,
    actionTypes,
    config.appRoot
);
