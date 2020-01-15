import { reportOptionsFactory } from '@linn-it/linn-form-components-library';
import { ateStatusReportActionTypes as actionTypes } from '../../actions';
import * as reportTypes from '../../reportTypes';

const defaultState = {};

export default reportOptionsFactory(
    reportTypes.ateStatusReport.actionType,
    actionTypes,
    defaultState
);
