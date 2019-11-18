import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { productionBackOrdersReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.productionBackOrdersReport.actionType,
    actionTypes,
    defaultState
);
