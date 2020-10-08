import { ReportActions } from '@linn-it/linn-form-components-library';
import { shortageSummaryActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.shortageSummary.item,
    reportTypes.shortageSummary.actionType,
    reportTypes.shortageSummary.uri,
    actionTypes,
    config.appRoot
);
