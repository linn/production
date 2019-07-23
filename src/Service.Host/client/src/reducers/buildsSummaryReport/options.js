import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { buildsSummaryReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.buildsSummaryReport.actionType,
    actionTypes,
    defaultState
);
