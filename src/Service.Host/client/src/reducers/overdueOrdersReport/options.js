import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { overdueOrdersReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.overdueOrders.actionType,
    actionTypes,
    defaultState
);
