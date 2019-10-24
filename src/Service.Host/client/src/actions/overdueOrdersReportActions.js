import { ReportActions } from '@linn-it/linn-form-components-library';
import { overdueOrdersReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.overdueOrders.item,
    reportTypes.overdueOrders.actionType,
    reportTypes.overdueOrders.uri,
    actionTypes,
    config.appRoot
);
