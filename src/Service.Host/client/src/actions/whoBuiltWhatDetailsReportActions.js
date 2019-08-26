import { ReportActions } from '@linn-it/linn-form-components-library';
import { whoBuiltWhatDetailsReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.whoBuiltWhatDetails.actionType,
    reportTypes.whoBuiltWhatDetails.uri,
    actionTypes,
    config.appRoot
);
